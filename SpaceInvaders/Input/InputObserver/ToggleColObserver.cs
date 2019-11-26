using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ToggleColObserver : InputObserver
    {
        private bool bswitch = false;
        public override void Notify()
        {
            //Debug.WriteLine("C key pressed");

            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            if (bswitch)
            {
                pSB_Boxes.bToggle = false;
                bswitch = false;

            }

            else
            {
                pSB_Boxes.bToggle = true;
                bswitch = true;
            }
            

        }
    }
}
