using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class P1GameOverObs : ColObserver
    {
        public override void Notify()
        {
            // we need to revert lvl1 or lvl2 to original forms
            // because we lost and will need to re-load content

            //ship was hit take a life and subtract from score
            
            PlayerMan.P1TakeLife();
            PlayerMan.SetP1Score(-250);

            float P1lives = PlayerMan.GetP1Lives();

            SpaceInvaders pGame = GameMan.GetGame();
            
            //check if they have more lives
            if (P1lives == 0)
            {
                // last life i was stil able to shoot
                Ship pShip = ShipMan.GetShip();

                pShip.SetShootState(ShipMan.ShootState.End);
                pShip.SetMoveState(ShipMan.MoveState.NoMove);

                GameState pState = pGame.GetCurrentState();

                if (pState.GetStateName() == GameMan.State.InGame)
                {
                    InGameState pLvl1 = (InGameState)pState;
                    pLvl1.SetStateToggle(false);

                    TimerMan.Add(TimeEvent.Name.GameStateChange, new GameStateChange(false), 5.0f);
                }

                else if (pState.GetStateName() == GameMan.State.LVL2)
                {
                    InGameStateLV2 pLvl2 = (InGameStateLV2)pState;
                    pLvl2.SetStateToggle(false);

                    TimerMan.Add(TimeEvent.Name.GameStateChange, new GameStateChange(false), 5.0f);

                }
            }

            else
            {
                //do nothing
            }
        }
    }
}
