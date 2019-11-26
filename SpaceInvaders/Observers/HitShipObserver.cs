using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class HitShipObserver : ColObserver
    {
        private Ship pShip;

        public HitShipObserver()
        {
            this.pShip = null;
        }

        public HitShipObserver(HitShipObserver s)
        {
            Debug.Assert(s != null);
            this.pShip = s.pShip;
        }
        public override void Notify()
        {
            GameObject pObject = this.pSubject.pObjA;
            this.pShip = (Ship)this.pSubject.pObjB;
            Debug.Assert(pShip != null);

            if(pShip.bMarkForDeath == false)
            {
                this.pShip.bMarkForDeath = true;

                //had initially tried to use the reference "this" ship
                //did not work FYI, needed to get the ship from manager
                Ship pShip = ShipMan.GetShip();

                pShip.SetShootState(ShipMan.ShootState.End);
                pShip.SetMoveState(ShipMan.MoveState.NoMove);


                HitShipObserver pObserver = new HitShipObserver(this);
                DelayedObjectMan.Attach(pObserver);

                

            }

            //very wierd not sure how i feel about it
            if(pObject.GetName() == GameObject.Name.Octopus|| pObject.GetName() == GameObject.Name.Squid || pObject.GetName() == GameObject.Name.Crab)
            {
                //we want to reset the grids location
                AlienGroup pGrid = (AlienGroup)GONodeMan.Find(GameObject.Name.AlienGrid);
                pGrid.MoveUp();

            }
            
            
        }

        public override void Execute()
        {

            //GameObject pShipRoot = GONodeMan.Find(GameObject.Name.ShipRoot);

            GameObject pShipObject = (GameObject)this.pShip;

            this.pShip.Remove();

            //pShipRoot.Remove(this.pShip);


             float repairs = PlayerMan.GetP1Lives();
            if(repairs != 0)
            {
                TimerMan.Add(TimeEvent.Name.FixShip, new FixShip(), 1.0f);
            }

        }
    }
}
