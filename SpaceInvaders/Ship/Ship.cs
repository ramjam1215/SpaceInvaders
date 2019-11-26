using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Ship : ShipCategory
    {
        public float shipSpeed;
        private ShipShootState state;
        private MoveShipState moveState;

        public Ship(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY) 
            : base(name, spriteName, ShipCategory.Type.Ship)
        {
            this.x = posX;
            this.y = posY;

            this.shipSpeed = 3.0f;
            this.state = null;
            this.moveState = null;
        }

        ~Ship()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Ship(): {0} ", this.GetHashCode());
#endif
            this.state = null;
            this.moveState = null;
        }

        // tells ship what state it should be in.... 
        //can it Shoot? can it move L or R?
        public void SetShootState(ShipMan.ShootState inState)
        {
            this.state = ShipMan.GetShootState(inState);
        }

        public void SetMoveState(ShipMan.MoveState inState)
        {
            this.moveState = ShipMan.GetMoveState(inState);
        }

        public override void Accept(ColVisitor other)
        {
            //Q: what hits the ship? 
            //A: mostly Bombs, but also aliens/AlienGroup(Get to later)....
            other.VisitShip(this);
        }

        public override void VisitBomb(Bomb b)
        {
            Debug.WriteLine("         collide:  {0} <-> {1}", b.GetName(), this.GetName());

            Debug.WriteLine("-------> BOOM!  <--------");

            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitAlien(AlienCategory a)
        {
            Debug.WriteLine("         collide:  {0} <-> {1}", a.GetName(), this.GetName());

            Debug.WriteLine("-------> BOOM!  <--------");

            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }


        public void MoveLeft()
        {
            this.moveState.MoveLeft(this);
        }

        public void MoveRight()
        {
            this.moveState.MoveRight(this);
        }

        public void ShootMissile()
        {
            this.state.ShootMissile(this);
        }

        public override void Update()
        {
            base.Update();
        }


    }
}
