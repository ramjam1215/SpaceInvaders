using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class NoLeftState : MoveShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMoveState(ShipMan.MoveState.FreeMove);
        }

        public override void MoveLeft(Ship pShip)
        {
            
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;

            this.Handle(pShip);
            
        }
    }
}
