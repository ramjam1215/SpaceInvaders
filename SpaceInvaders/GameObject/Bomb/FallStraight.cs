using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FallStraight : FallStrategy
    {
        private float oldPosY;

        public FallStraight()
        {
            this.oldPosY = 0.0f;
        }

        //Falls straight down
        public override void Fall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);
        }

        //maybe give an aliens position to send a missile later?
        public override void Reset(float posY)
        {
            this.oldPosY = posY;
        }
    }
}
