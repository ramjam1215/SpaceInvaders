using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShootObserver : InputObserver
    {
        public override void Notify()
        {
            //Testing purposes
            //Debug.WriteLine("Shoot");

            //get the ship
            Ship pShip = ShipMan.GetShip();

            //tell the ship to shoot/create a missile
            pShip.ShootMissile();
        }
    }
}
