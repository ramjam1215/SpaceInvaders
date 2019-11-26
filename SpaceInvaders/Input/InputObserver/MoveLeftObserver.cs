using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveLeftObserver : InputObserver
    {
        public override void Notify()
        {
            //Testing purposes
            //Debug.WriteLine("Move Left");
            Ship pShip = ShipMan.GetShip();
            pShip.MoveLeft();
        }
    }
}
