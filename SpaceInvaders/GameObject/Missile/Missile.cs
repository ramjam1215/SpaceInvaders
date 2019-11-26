using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : MissileCategory
    {
        public bool enable;
        public float deltaMove;

        public Missile(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY) 
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.enable = false;

            this.deltaMove = 10.0f;
        }

        public override void Update()
        {
            
            base.Update();

            this.y += this.deltaMove;


            //if (this.enable == true)
            //{
            //    this.y += this.deltaMove;
            //}

            //else
            //{
            //    Ship pShip = ShipMan.GetShip();
            //    this.x = pShip.x;
            //}
        }

        public override void Remove()
        {
            this.GetColObject().poColRect.Set(0, 0, 0, 0);
            base.Update();

            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            base.Remove();
        }

        //public override void Reset()
        //{
        //    Ship pShip = ShipMan.GetShip();

        //    this.GetColObject().poColRect.Set(0, 0, 0, 0);

        //    //SetPos(pShip.x, pShip.y);
        //    //SetActive(false);

        //    base.Update();

        //    GameObject pParent = (GameObject)this.pParent;
        //    pParent.Update();


        //    base.Reset();

        //}


        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public void SetActive(bool state)
        {
            this.enable = state;
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitMissile(this);
        }

        public override void VisitBomb(Bomb b)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", b.GetName(), this.GetName());

            //Debug.WriteLine("-------> Bomb V Missile!  <--------");

            ColPair pColPair = ColPairMan.GetActiveColPair();

            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

    }
}
