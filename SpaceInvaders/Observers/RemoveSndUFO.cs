using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveSndUFO : ColObserver
    {
        public override void Notify()
        {
            //might need to specify if i add more sound effects
            //but testing here
            TimeEvent pTE = TimerMan.Find(TimeEvent.Name.PlaySound);

            if(pTE != null)
            {
                TimerMan.Remove(pTE);
            }

            else
            {
                // edge case of ufo hit wall and hit by missile
                //event was attempted to be removed twice
            }
            
            //TimerMan.DumpTimeEvents();
        }
    }
}
