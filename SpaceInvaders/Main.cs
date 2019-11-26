using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            GameMan.Create();
            // Create the instance
            SpaceInvaders game = GameMan.GetGame();
            Debug.Assert(game != null);

            // Start the game
            game.Run();
        }
    }
}
