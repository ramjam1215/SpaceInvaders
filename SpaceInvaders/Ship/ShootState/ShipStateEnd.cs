using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipStateEnd : ShipShootState
    {

        //commented out everything, just ideas
        //Dead state?
        //private float lives = 3.0f;

        public override void Handle(Ship pShip)
        {
            //SpaceInvaders pGame = GameMan.GetGame();

            //if (lives == 0)
            //{
            //    pGame.SetGameState(GameMan.State.GameOver);
            //}

            //this.lives -= 1;
            //pShip.SetMoveState(ShipMan.MoveState.FreeMove);
            //pShip.SetShootState(ShipMan.ShootState.Ready);
        }

        public override void ShootMissile(Ship pShip)
        {
            
        }
    }
}
