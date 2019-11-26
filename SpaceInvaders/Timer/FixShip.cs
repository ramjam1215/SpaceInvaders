using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FixShip : Command
    {

        public FixShip()
        {
            
        }

        public override void Execute(float deltaTime)
        {
            Ship pShip = ShipMan.ActivateShip();

            pShip.SetMoveState(ShipMan.MoveState.FreeMove);
            pShip.SetShootState(ShipMan.ShootState.Ready);

        }
    }
}
