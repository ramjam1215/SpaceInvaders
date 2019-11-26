using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pColSpriteBatch;
        private Composite pTree;


        public AlienFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name colSpriteBatchName, Composite pTree)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pColSpriteBatch = SpriteBatchMan.Find(colSpriteBatchName);
            Debug.Assert(this.pColSpriteBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }

        ~AlienFactory()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~AlienFactory(): ");
#endif

            this.pSpriteBatch = null;
            this.pColSpriteBatch = null;
            this.pTree = null;
        }

        public void SetParent(GameObject pParentNode)
        {
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }

        public GameObject Create(GameObject.Name name,  AlienCategory.Type type, float posX = 0.0f, float posY = 0.0f, float ColIndex = 0.0f)
        {
            GameObject pGameObject = null;

            switch (type)
            {
                case AlienCategory.Type.Squid:
                    pGameObject = new SquidGO(name, GameSprite.Name.Squid, posX, posY);
                    break;

                case AlienCategory.Type.Crab:
                    pGameObject = new CrabGO(name, GameSprite.Name.Crab, posX, posY);
                    break;

                case AlienCategory.Type.Octopus:
                    pGameObject = new OctoGO(name, GameSprite.Name.Octopus, posX, posY);
                    break;

                case AlienCategory.Type.Group:
                    pGameObject = new AlienGroup(name, GameSprite.Name.NullObject, 0.0f, 0.0f);
                    break;

                case AlienCategory.Type.Column:
                    pGameObject = new AlienColumn(name, GameSprite.Name.NullObject, 0.0f, 0.0f, ColIndex);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            this.pTree.Add(pGameObject);


            //attach to Group to Draw/Render it
            pGameObject.ActivateGameSprite(this.pSpriteBatch);
            pGameObject.ActivateCollisionSprite(this.pColSpriteBatch);

            return pGameObject;
        }
    }
}
