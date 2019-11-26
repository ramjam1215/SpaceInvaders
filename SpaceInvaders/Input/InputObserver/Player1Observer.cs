using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Player1Observer : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("1 key pressed");
            //this is for the Intro
            //might be a bug here, if people press it again
            SpaceInvaders pGame = GameMan.GetGame();

            pGame.GetCurrentState().Handle();
        }
    }
}
