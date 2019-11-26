using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameStateChange : Command
    {
        private bool wonOrLost;
        public GameStateChange(bool wonOrLost)
        {
            this.wonOrLost = wonOrLost;
        }
        //this will be called if a wave is completed for better or worse
        public override void Execute(float deltaTime)
        {
            SpaceInvaders pGame = GameMan.GetGame();

            if (wonOrLost == true)
            {
                pGame.GetCurrentState().Handle();
            }

            else
            {
                pGame.SetGameState(GameMan.State.GameOver);
                pGame.GetCurrentState().Handle();
            }
            
        }
    }
}
