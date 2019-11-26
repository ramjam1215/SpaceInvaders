using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Bomb : BombCategory
    {
        public float deltaMove;
        private FallStrategy pStrategy;
        private GameObject pOwner;
       

        public Bomb(GameObject.Name name, GameSprite.Name spriteName, FallStrategy strategy, GameObject owner, float posX, float posY)
            : base(name, spriteName, BombCategory.Type.Bomb)
        {
            this.x = posX;
            this.y = posY;
            this.deltaMove = 4.0f;

            Debug.Assert(strategy != null);
            this.pStrategy = strategy;

            this.pOwner = owner;

            this.pStrategy.Reset(this.y);

            this.GetColObject().pColSprite.SetLineColor(1, 1, 0);
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitBomb(this);
        }

        public override void VisitMissile(Missile m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", this.GetName(), m.GetName());

            //Debug.WriteLine("-------> Bomb V Missile!  <--------");

            ColPair pColPair = ColPairMan.GetActiveColPair();

            pColPair.SetCollision(this, m);
            pColPair.NotifyListeners();
        }

        //public void Reset()
        //{
        //    this.x = this.pColumn.GetColObject().poColRect.minX;
        //    this.y = this.pColumn.GetColObject().poColRect.maxY;
        //    this.pStrategy.Reset(this.y);
        //}

        public override void Remove()
        {
            this.GetColObject().poColRect.Set(0, 0, 0, 0);
            base.Update();


            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            base.Remove();

        }

        public override void Update()
        {
            base.Update();
            this.y -= deltaMove;

            this.pStrategy.Fall(this);
        }

        public GameObject GetOwner()
        {
            return this.pOwner;
        }


        public float GetBoundingBoxHeight()
        {
            return this.GetColObject().poColRect.height;
        }

        public void SetPos(float xPos,float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public void MultiplyScale(float sx, float sy)
        {
            Debug.Assert(this.GetProxy() != null);

            this.GetProxy().sx *= sx;
            this.GetProxy().sy *= sy;
        }


    }
}
