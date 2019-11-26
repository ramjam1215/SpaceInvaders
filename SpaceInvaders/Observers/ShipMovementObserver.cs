using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMovementObserver : ColObserver
    {
        private Ship pShip;
        private GameObject pWall;
        public override void Notify()
        {
            this.pShip = (Ship)this.pSubject.pObjA;
            Debug.Assert(this.pShip != null);


            this.pWall = (WallCategory)this.pSubject.pObjB;
            Debug.Assert(this.pWall != null);

            if(pWall.GetName() == GameObject.Name.LeftWall)
            {
                this.pShip.SetMoveState(ShipMan.MoveState.NoLeft);
            }
            
            if(pWall.GetName() == GameObject.Name.RightWall)
            {
                this.pShip.SetMoveState(ShipMan.MoveState.NoRight);
            }

        }
    }
}
