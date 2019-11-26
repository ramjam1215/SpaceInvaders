using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveRightObserver : InputObserver
    {
        public override void Notify()
        {
            //testing purposes
            //Debug.WriteLine("Move Right");
            Ship pShip = ShipMan.GetShip();
            pShip.MoveRight();
        }
    }
}
