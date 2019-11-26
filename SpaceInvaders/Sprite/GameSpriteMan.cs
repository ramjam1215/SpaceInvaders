using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GSMan_MLink : Manager
    {
        public GameSprite_Link poActive;
        public GameSprite_Link poReserve;
    }
    class GameSpriteMan : GSMan_MLink
    {
        private static GameSpriteMan pInstance = null;
        private GameSprite poNodeCompare;

        public GameSpriteMan(int reserveNum = 5, int growth = 2)
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.poNodeCompare = new GameSprite();
        }

        public static void Create(int reserveNum, int growth)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
                pInstance = new GameSpriteMan(reserveNum, growth);

            GameSprite pGSprite = GameSpriteMan.Add(GameSprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
            Debug.Assert(pGSprite != null);
        }

        public static void Destroy()
        {
            GameSpriteMan pSMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pSMan != null);

            pSMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("GameSpriteMan.Destroy()");
#endif
            pSMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("{0} ({1})", pSMan.poNodeCompare, pSMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("{0} ({1})", GameSpriteMan.pInstance, GameSpriteMan.pInstance.GetHashCode());
#endif

            pSMan.poNodeCompare = null;
            GameSpriteMan.pInstance = null;
        }

        public static GameSprite Add(GameSprite.Name name, Image.Name ImageName, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            GameSpriteMan pSMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pSMan != null);

            GameSprite pSNode = (GameSprite)pSMan.BaseAdd();
            Debug.Assert(pSNode != null);

            Image pImage = ImageMan.Find(ImageName);
            Debug.Assert(pSNode != null);

            pSNode.Set(name, pImage, x, y, width, height, pColor);
            return pSNode;
        }

        //optional add method to add color
        public static GameSprite Add(GameSprite.Name name, Image.Name ImageName, float x, float y, float width, float height, float red, float green, float blue)
        {
            GameSpriteMan pSMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pSMan != null);

            GameSprite pSNode = (GameSprite)pSMan.BaseAdd();
            Debug.Assert(pSNode != null);

            Image pImage = ImageMan.Find(ImageName);
            Debug.Assert(pSNode != null);

            pSNode.Set(name, pImage, x, y, width, height, new Azul.Color(red, green, blue));
            return pSNode;
        }

        public static GameSprite Find(GameSprite.Name name)
        {
            GameSpriteMan pSMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pSMan != null);

            pSMan.poNodeCompare.SetName(name);

            GameSprite pSNode = (GameSprite)pSMan.BaseFind(pSMan.poNodeCompare);
            return pSNode;
        }

        public static void Remove(GameSprite pSNode)
        {
            GameSpriteMan pSMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pSMan != null);

            Debug.Assert(pSNode != null);
            pSMan.BaseRemove(pSNode);
        }

        public static void DumpSprites()
        {
            GameSpriteMan pSMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pSMan != null);

            pSMan.BaseDumpNodes();
        }

        private void PrivStatDump()
        {
            GameSpriteMan pSMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pSMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("GameSprite Manager Stats------------------------------");
            pSMan.BaseStatDump();
        }

        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            GameSprite pSNode1 = (GameSprite)pDLink1;
            GameSprite pSNode2 = (GameSprite)pDLink2;

            Boolean status = false;

            if (pSNode1.GetName() == pSNode2.GetName())
                status = true;

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new GameSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            GameSprite pSNode = (GameSprite)pDLink;
            pSNode.DumpSprite();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            GameSprite pSNode = (GameSprite)pDLink;
            pSNode.Wash();
        }

        private static GameSpriteMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
