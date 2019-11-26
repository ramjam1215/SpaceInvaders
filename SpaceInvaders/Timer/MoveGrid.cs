using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveGrid : Command
    {

        private Composite pGrid;
        IrrKlang.ISoundEngine pSndEngine;
        public MoveGrid(Composite pGrid, IrrKlang.ISoundEngine pEng)
        {
            Debug.Assert(pGrid != null);
            this.pGrid = pGrid;

            Debug.Assert(pEng != null);
            this.pSndEngine = pEng;
        }

        private AlienGroup GetComposite()
        {
            return (AlienGroup)this.pGrid;
        }

        public override void Execute(float deltaTime)
        {
            AlienGroup pGrid = GetComposite();
            pGrid.MoveAcross();

            //sound 1 at 500+
            if(pGrid.y >= 500)
            {
                pSndEngine.SoundVolume = 0.2f;
                pSndEngine.Play2D("fastinvader1.wav");
            }

            //sound 2 at 499 - 400
            else if(pGrid.y < 500 && pGrid.y >= 400)
            {
                pSndEngine.SoundVolume = 0.2f;
                pSndEngine.Play2D("fastinvader2.wav");
            }

            //sound 3 at 399 - 300
            else if(pGrid.y < 400 && pGrid.y >= 300)
            {
                pSndEngine.SoundVolume = 0.2f;
                pSndEngine.Play2D("fastinvader3.wav");
            }

            //sound 4 at 299-0
            else if (pGrid.y < 300)
            {
                pSndEngine.SoundVolume = 0.2f;
                pSndEngine.Play2D("fastinvader4.wav");
            }
            

            TimerMan.Add(TimeEvent.Name.MoveGrid, this, deltaTime);
        }
    }
}