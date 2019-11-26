using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Player2Observer : InputObserver
    {
        public override void Notify()
        {
            Debug.WriteLine("2 key pressed");

            Debug.Assert(false);
        }
    }
}
