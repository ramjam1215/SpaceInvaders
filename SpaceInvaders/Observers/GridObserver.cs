using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridObserver : ColObserver
    {
        public GridObserver()
        {

        }

        //Grid Observer only worryies about moving the grid back and forth and down
        public override void Notify()
        {
            float movement;
            //Debug.WriteLine("Grid_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            AlienGroup pGrid = (AlienGroup)this.pSubject.pObjA;

            WallCategory pWall = (WallCategory)this.pSubject.pObjB;

            if(pWall.GetCategoryType() == WallCategory.Type.Right)
            {
                //BAD HACK FIX LATER
                movement = pGrid.GetDeltaMove() * -1.0f;
                pGrid.SetDeltaMove(movement);
                pGrid.MoveAcross();
                pGrid.MoveDown();
            }

            else if (pWall.GetCategoryType() == WallCategory.Type.Left)
            {
                movement = pGrid.GetDeltaMove() * -1.0f;
                pGrid.SetDeltaMove(movement);
                pGrid.MoveAcross();
                pGrid.MoveDown();

            }

            else
            {
                //Bottom Wall hit, move aliens up and reset Waves before Game Over
                SpaceInvaders pGame = GameMan.GetGame();
                GameState pState = pGame.GetCurrentState();

                if (pState.GetStateName() == GameMan.State.InGame)
                {
                    InGameState pLvl1 = (InGameState)pState;
                    pLvl1.SetStateToggle(false);

                    TimerMan.Add(TimeEvent.Name.GameStateChange, new GameStateChange(false), 2.0f);
                }

                else if (pState.GetStateName() == GameMan.State.LVL2)
                {
                    InGameStateLV2 pLvl2 = (InGameStateLV2)pState;
                    pLvl2.SetStateToggle(false);

                    TimerMan.Add(TimeEvent.Name.GameStateChange, new GameStateChange(false), 2.0f);

                }
            }
        }
    }
}
