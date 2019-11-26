using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpeedUpCheck : Command
    {
        private AlienGroup pGrid;

        public SpeedUpCheck(AlienGroup pGrid)
        {
            Debug.Assert(pGrid != null);
            this.pGrid = pGrid;
        }
        public override void Execute(float deltaTime)
        {
            //1 Grid
            //11 columns
            //55 aliens
            //--------
            //67 total

            TimeEvent pE1 = TimerMan.Find(TimeEvent.Name.AnimateSquid);
            TimeEvent pE2 = TimerMan.Find(TimeEvent.Name.AnimateOcto);
            TimeEvent pE3 = TimerMan.Find(TimeEvent.Name.AnimateCrab);

            TimeEvent pE4 = TimerMan.Find(TimeEvent.Name.MoveGrid);


            ForwardIterator pForIter = new ForwardIterator(pGrid);

            Component pNode = pForIter.First();
            int count = 0;
            while (!pForIter.IsDone())
            {
                count++;
                pNode = pForIter.Next();
            }

            // 50 - 35
            if(count < 50 && count >= 35)
            {
                pE1.SetTriggerTime(0.4f);
                pE2.SetTriggerTime(0.4f);
                pE3.SetTriggerTime(0.4f);
                pE4.SetTriggerTime(0.4f);
            }

            //34-15
            else if(count < 35 && count >= 15)
            {
                pE1.SetTriggerTime(0.20f);
                pE2.SetTriggerTime(0.20f);
                pE3.SetTriggerTime(0.20f);
                pE4.SetTriggerTime(0.20f);
            }

            //15-0
            else if(count < 15)
            {
                pE1.SetTriggerTime(0.10f);
                pE2.SetTriggerTime(0.10f);
                pE3.SetTriggerTime(0.10f);
                pE4.SetTriggerTime(0.10f);
            }


            TimerMan.Add(TimeEvent.Name.SpeedCheck, this, deltaTime);

        }
    }
}
