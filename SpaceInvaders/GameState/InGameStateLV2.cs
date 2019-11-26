using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InGameStateLV2 : GameState
    {
        private static GameMan.State pName = GameMan.State.LVL2;
        private bool bWaveFinished = false;

        public override GameMan.State GetStateName()
        {
            return pName;
        }

        public void SetStateToggle(bool bToggle)
        {
            this.bWaveFinished = bToggle;
        }

        public override void Handle()
        {
            //handle is called before entering gamestate loops
            //fight the wave
            if(bWaveFinished == false)
            {

                this.bWaveFinished = true;
                LoadContent();
                
            }

            //wave complete and go to game over screen
            else
            {

                this.bWaveFinished = false;
                SpaceInvaders pGame = GameMan.GetGame();

                pGame.SetGameState(GameMan.State.GameOver);
                pGame.GetCurrentState().Handle();
            }

            
        }

        public override void LoadContent()
        {
            //make the next wave more difficult
            //change grid movement speed and add more bombs
            GraveyardMan.RaiseDead();

            //can be finicky might need to replace
            //GraveyardMan.RebuildShields();

            //-------------------------------------------------------------------------------------------------------
            //create factory
            //--------------------------------------------------------------------------------------------------------

            Composite pAlienGroup = (Composite)GONodeMan.Find(GameObject.Name.AlienGrid);

            //AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes, pAlienGroup);

            //GameObject pGameObj;

            //AF.SetParent(pAlienGroup);
            //GameObject pCol = AF.Create(GameObject.Name.Column_1, AlienCategory.Type.Column, 0.0f, 0.0f);

            //AF.SetParent(pCol);

            //pGameObj = AF.Create(GameObject.Name.Octopus, AlienCategory.Type.Octopus, 70.0f, 600);

            //Column Creation 1 - 11
            //for (int i = 0; i < 1; i++)
            //{
            //    AF.SetParent(pAlienGroup);
            //    GameObject pCol = AF.Create(GameObject.Name.Column_1 + i, AlienCategory.Type.Column, 0.0f, 0.0f, i);

            //    AF.SetParent(pCol);

            //    pGameObj = AF.Create(GameObject.Name.Octopus, AlienCategory.Type.Octopus, 70.0f + i * 43.0f, 660);
            //    pGameObj = AF.Create(GameObject.Name.Crab, AlienCategory.Type.Crab, 70.0f + i * 43.0f, 620);
            //    pGameObj = AF.Create(GameObject.Name.Crab, AlienCategory.Type.Crab, 70.0f + i * 43.0f, 580);
            //    pGameObj = AF.Create(GameObject.Name.Squid, AlienCategory.Type.Squid, 70.0f + i * 43.0f, 540);
            //    pGameObj = AF.Create(GameObject.Name.Squid, AlienCategory.Type.Squid, 70.0f + i * 43.0f, 500);
            //}

            //Debug.WriteLine("-------------------");
            //pAlienGroup.Print();

            //---------------------------------------------------------------------------------------------------------
            // Shields
            //---------------------------------------------------------------------------------------------------------

            //float posX = 80;
            //float posY = 120;

            //for (int i = 0; i < 1; i++)
            //{
            //    ShieldFactory.ShieldCreator(posX, posY, GameObject.Name.ShieldGrid_1 + i);
            //    posX += 130;
            //}

            //Debug.WriteLine("-------------------");

            //GameObject pShieldRoot = GONodeMan.Find(GameObject.Name.ShieldRoot);
            //pShieldRoot.Print();


            //---------------------------------------------------------------------------------
            //Event/Difficulty Modifiers
            //----------------------------------------------------------------------------------
            TimeEvent pE1 = TimerMan.Find(TimeEvent.Name.AnimateSquid);
            TimeEvent pE2 = TimerMan.Find(TimeEvent.Name.AnimateOcto);
            TimeEvent pE3 = TimerMan.Find(TimeEvent.Name.AnimateCrab);

            TimeEvent pE4 = TimerMan.Find(TimeEvent.Name.MoveGrid);

            pE1.SetTriggerTime(0.50f);
            pE2.SetTriggerTime(0.50f);
            pE3.SetTriggerTime(0.50f);
            pE4.SetTriggerTime(0.50f);

            //pAlienGroup.SetDeltaMove(18.0f);
            BombDrop pBombDrop1 = new BombDrop((AlienGroup)pAlienGroup);
            BombDrop pBombDrop2 = new BombDrop((AlienGroup)pAlienGroup);
            BombDrop pBombDrop3 = new BombDrop((AlienGroup)pAlienGroup);

            TimerMan.Add(TimeEvent.Name.ColumnShoot, pBombDrop1, 1.0f);
            TimerMan.Add(TimeEvent.Name.ColumnShoot, pBombDrop2, 3.0f);
            TimerMan.Add(TimeEvent.Name.ColumnShoot, pBombDrop3, 5.0f);
        }

        public override void Update()
        {
            SpaceInvaders pGame = GameMan.GetGame();

            pGame.sndEngine.Update();

            InputManager.Update();

            Simulation.Update(pGame.GetTime());

            if (Simulation.GetTimeStep() > 0.0f)
            {
                //start timer
                TimerMan.Update(Simulation.GetTotalTime());

                //Update all the game objects(nodes)
                GONodeMan.Update();

                //check for collisions
                ColPairMan.Process();

                //process observers
                DelayedObjectMan.Process();
            }


            //---------------------------------------------------------------------------------------------------------
            // Font Practice
            //---------------------------------------------------------------------------------------------------------

            Font pScoreMessage = FontMan.Find(Font.Name.P1Points);
            Debug.Assert(pScoreMessage != null);
            pScoreMessage.UpdateMessage("" + (PlayerMan.GetP1Score()));

            Font pHiScore = FontMan.Find(Font.Name.HiPoints);
            Debug.Assert(pHiScore != null);
            pHiScore.UpdateMessage("" + PlayerMan.GetHiScore());

            Font pP1LivesLeft = FontMan.Find(Font.Name.LivesP1);
            Debug.Assert(pP1LivesLeft != null);
            pP1LivesLeft.UpdateMessage("P1 Lives: " + (PlayerMan.GetP1Lives()));

        }

        public override void Draw()
        {
            //Draw/Render the Sprites
            SpriteBatchMan.Draw();
        }


    }
}
