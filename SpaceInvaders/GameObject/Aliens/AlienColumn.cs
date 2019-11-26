using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienColumn : Composite
    {
        private float ColIndex;
        public Bomb pBomb;

        public AlienColumn(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float _ColIndex) 
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pBomb = null;

            this.ColIndex = _ColIndex;

            this.GetColObject().pColSprite.SetLineColor(0, 0, 1);

        }

        public override void Update()
        {

            //go to children and get new rectangle bounds
            base.BaseUpdateBoundingBox(this);
            
            //then update yourself
            base.Update();
        }

        public float GetColIndex()
        {
            return this.ColIndex;
        }

        public void ActivateBomb(float minX, float minY)
        {
            GameObject pBombRoot = GONodeMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            Random rNum = new Random();
            int choice = rNum.Next(1, 4);

            switch (choice)
            {
                case 1:
                    this.pBomb = new  Bomb(GameObject.Name.Bomb, GameSprite.Name.BombStraight, new FallStraight(), this, minX, minY);
                    break;

                case 2:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombDagger, new FallDagger(), this, minX, minY);
                    break;

                case 3:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombZigZag, new FallZigZag(), this, minX, minY);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }



            pBombRoot.Add(this.pBomb);

            SpriteBatch pSB_Projectiles = SpriteBatchMan.Find(SpriteBatch.Name.Projectiles);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            this.pBomb.ActivateGameSprite(pSB_Projectiles);
            this.pBomb.ActivateCollisionSprite(pSB_Boxes);


        }

        public override void Accept(ColVisitor other)
        {
            //what is the reaction of the "other" object
            // with the Alien Column
            other.VisitColumn(this);
        }

        public override void VisitMissile(Missile m)
        {
            //Alien Column vs MissileGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            
            //now check the Aliens
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObject);
        }

        
    }
}
