using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class NoRightState : MoveShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMoveState(ShipMan.MoveState.FreeMove);
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;

            this.Handle(pShip);
            
        }

        public override void MoveRight(Ship pShip)
        {
            
        }
    }
}
