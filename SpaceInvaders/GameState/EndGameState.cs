using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class EndGameState : GameState
    {
        private static GameMan.State pName = GameMan.State.GameOver;
        private float ChangeStateTime;
        private bool bStateToggle = true;

        public override GameMan.State GetStateName()
        {
            return pName;
        }
        public override void Handle()
        {

            SpriteBatch pGameOverScreen = SpriteBatchMan.Find(SpriteBatch.Name.GameOver);
            pGameOverScreen.bToggle = true;

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            pSB_Aliens.bToggle = false;

            SpriteBatch pSB_Shields = SpriteBatchMan.Find(SpriteBatch.Name.Shields);
            pSB_Shields.bToggle = false;

            SpriteBatch pSB_InGame = SpriteBatchMan.Find(SpriteBatch.Name.InGameScreen);
            pSB_InGame.bToggle = false;

            SpriteBatch pSB_Projectiles = SpriteBatchMan.Find(SpriteBatch.Name.Projectiles);
            pSB_Projectiles.bToggle = false;

            PlayerMan.UpdateHiScore();
            PlayerMan.ClearStuff();
        }

        public override void LoadContent()
        {
            Debug.Assert(false);
        }

        public override void Update()
        {
            //first time through set a time to change states
            if (this.bStateToggle == true)
            {
                //inititally used the game to get time
                //Testing timer man
                //could use Timer Man for current time
                float curTime = TimerMan.GetCurrentTime();
                this.ChangeStateTime = curTime + 10;
                this.bStateToggle = false;
            }


            else
            {
                SpaceInvaders pGame = GameMan.GetGame();

                //time has elapsed start over
                if (this.ChangeStateTime <= pGame.GetTime() )
                {
                    this.bStateToggle = true;

                    SpriteBatch pSB_Intro = SpriteBatchMan.Find(SpriteBatch.Name.IntroScreen);
                    pSB_Intro.bToggle = true;

                    SpriteBatch pGameOverScreen = SpriteBatchMan.Find(SpriteBatch.Name.GameOver);
                    pGameOverScreen.bToggle = false;

                    //SpriteBatch pSB_Splats = SpriteBatchMan.Find(SpriteBatch.Name.Splats);
                    //pSB_Splats.bToggle = false;

                    //----------------------------------------------------------------------------------------------------
                    //Things we need to reset for the Next game?
                    //----------------------------------------------------------------------------------------------------

                    // need to remove ship from its root
                    GameObject pShipRoot = GONodeMan.Find(GameObject.Name.ShipRoot);
                    GameObject pShip = (GameObject)pShipRoot.GetFirstChild();

                    if (pShip != null)
                    {
                        pShip.Remove();
                    }

                    ShipMan.Destroy();

                    GraveyardMan.KillAll();
                    //GraveyardMan.DumpNodes();
                    GraveyardMan.Destroy();


                    TimerMan.ClearActiveList();
                    TimerMan.DumpTimeEvents();

                    AlienGroup pAlienGroup = (AlienGroup)GONodeMan.Find(GameObject.Name.AlienGrid);
                    pAlienGroup.SetDeltaMove(15.0f);
                    
                    SpriteBatch pSB_Projectiles = SpriteBatchMan.Find(SpriteBatch.Name.Projectiles);
                    pSB_Projectiles.bToggle = false;

                    //----------------------------------------------------------------------------------------------------

                    pGame.SetGameState(GameMan.State.Intro);
                    pGame.GetCurrentState().Update();


                }
            }

        }

        public override void Draw()
        {
            SpriteBatchMan.Draw();
        }
    }
}
