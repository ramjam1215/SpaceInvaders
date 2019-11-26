using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameState
    {

        public abstract GameMan.State GetStateName();

        //state
        public abstract void Handle();


        //strategy?
        public abstract void LoadContent();

        public abstract void Update();

        public abstract void Draw();

    }
}
