using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //cant shoot state, happens once shipstate ready activates missile
    public class ShipStateMissileFlying : ShipShootState
    {
        public override void Handle(Ship pShip)
        {
            
        }


        public override void ShootMissile(Ship pShip)
        {
            
        }
    }
}
