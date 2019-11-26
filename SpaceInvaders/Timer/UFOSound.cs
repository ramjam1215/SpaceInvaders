using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOSound : Command
    {
        //this is hopefully going to sound of every 1 second not sure if thats what its supposed to do
        public override void Execute(float deltaTime)
        {
            SpaceInvaders pGame = GameMan.GetGame();

            IrrKlang.ISoundEngine pSndEngine = pGame.GetSndEngine();

            pSndEngine.SoundVolume = 0.1f;
            pSndEngine.Play2D("ufo_lowpitch.wav");


            TimerMan.Add(TimeEvent.Name.PlaySound, this, deltaTime);

            

        }
    }
}
