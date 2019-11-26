using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SendUFO : Command
    {
        private UFORoot pUFOroot;
        private UFO pUFO;

        public SendUFO(UFORoot pUFOroot)
        {
            this.pUFOroot = pUFOroot;
            this.pUFO = null;
        }
        public void ActivateUFO()
        {
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);

            if(pUFOroot.GetDeltaMove() > 0)
                this.pUFO = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 140, 700);

            else
            {
                this.pUFO = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 600, 700);
            }

            pUFO.ActivateGameSprite(pSB_Aliens);
            pUFO.ActivateCollisionSprite(pSB_Boxes);
            
            pUFOroot.Add(pUFO);
        }



        public override void Execute(float deltaTime)
        {
            ActivateUFO();
            IrrKlang.ISoundEngine pSndEngine = GameMan.GetGame().GetSndEngine();
            pSndEngine.Play2D("ufo_lowpitch.wav");

            TimerMan.Add(TimeEvent.Name.SendUFO, this, deltaTime);
            TimerMan.Add(TimeEvent.Name.UFOBomb, new UFOBombDrop(this.pUFO), 1.0f);
            TimerMan.Add(TimeEvent.Name.PlaySound, new UFOSound(), 1.5f);
            
        }
    }
}
