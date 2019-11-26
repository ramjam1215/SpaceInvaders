using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //this is a one-time deal event every time the UFO is spawned
    class UFOBombDrop : Command
    {
        private UFO pUFO;
        public UFOBombDrop(UFO ufo)
        {

            this.pUFO = ufo;

        }
        public override void Execute(float deltaTime)
        {
            float posX = pUFO.x;
            float posY = pUFO.y;

            pUFO.ActivateBomb(posX, posY);
        }
    }
}
