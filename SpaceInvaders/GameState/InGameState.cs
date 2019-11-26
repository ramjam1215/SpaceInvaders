using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InGameState : GameState
    {
        // maybe make strategies instead of a toggle
        //these will tell us what to draw/render via toggle variables
        private static GameMan.State pName = GameMan.State.InGame;
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
            if (bWaveFinished == false)
            {
                Debug.WriteLine("Wave 1");
                Debug.WriteLine("-----------------------------------------------------------------");
                // De-activate sprite batch
                SpriteBatch pIntroScreen = SpriteBatchMan.Find(SpriteBatch.Name.IntroScreen);
                pIntroScreen.bToggle = false;

                this.bWaveFinished = true;
                LoadContent();
                //Update();
            }

            // wave is finished go to the next one
            else
            {
                Debug.WriteLine("Wave 2");
                Debug.WriteLine("-----------------------------------------------------------------");

                this.bWaveFinished = false;

                SpaceInvaders pGame = GameMan.GetGame();

                pGame.SetGameState(GameMan.State.LVL2);
                pGame.GetCurrentState().Handle();
                
            }


        }

        public override void LoadContent()
        {
            
            ShipMan.Create();
            GraveyardMan.Create();

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            pSB_Aliens.bToggle = true;

            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            pSB_Boxes.bToggle = false;

            SpriteBatch pSB_Shields = SpriteBatchMan.Find(SpriteBatch.Name.Shields);
            pSB_Shields.bToggle = true;

            SpriteBatch pSB_InGame = SpriteBatchMan.Find(SpriteBatch.Name.InGameScreen);
            pSB_InGame.bToggle = true;

            SpriteBatch pSB_Projectiles = SpriteBatchMan.Find(SpriteBatch.Name.Projectiles);
            pSB_Projectiles.bToggle = true;

            //SpriteBatch pSB_Splats = SpriteBatchMan.Find(SpriteBatch.Name.Splats);
            //pSB_Splats.bToggle = true;

            SpaceInvaders pGame = GameMan.GetGame();
            IrrKlang.ISoundEngine pSndEngine = pGame.GetSndEngine();

            //-------------------------------------------------------------------------------------------------------
            //create factory
            //--------------------------------------------------------------------------------------------------------

            Composite pAlienGroup = (Composite)GONodeMan.Find(GameObject.Name.AlienGrid);

            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes, pAlienGroup);

            GameObject pGameObj;

            //AF.SetParent(pAlienGroup);
            //GameObject pCol = AF.Create(GameObject.Name.Column_1, AlienCategory.Type.Column, 0.0f, 0.0f);

            //AF.SetParent(pCol);

            //pGameObj = AF.Create(GameObject.Name.Octopus, AlienCategory.Type.Octopus, 70.0f, 600);

            //Column Creation 1 - 11
            for (int i = 0; i < 11; i++)
            {
                AF.SetParent(pAlienGroup);
                GameObject pCol = AF.Create(GameObject.Name.Column_1 + i, AlienCategory.Type.Column, 0.0f, 0.0f, i);

                AF.SetParent(pCol);

                pGameObj = AF.Create(GameObject.Name.Octopus, AlienCategory.Type.Octopus, 70.0f + i * 43.0f, 600);
                pGameObj = AF.Create(GameObject.Name.Crab, AlienCategory.Type.Crab, 70.0f + i * 43.0f, 560);
                pGameObj = AF.Create(GameObject.Name.Crab, AlienCategory.Type.Crab, 70.0f + i * 43.0f, 520);
                pGameObj = AF.Create(GameObject.Name.Squid, AlienCategory.Type.Squid, 70.0f + i * 43.0f, 480);
                pGameObj = AF.Create(GameObject.Name.Squid, AlienCategory.Type.Squid, 70.0f + i * 43.0f, 440);
            }

            //Debug.WriteLine("-------------------");
            //pAlienGroup.Print();

            //---------------------------------------------------------------------------------------------------------
            // Shields
            //---------------------------------------------------------------------------------------------------------

            float posX = 80;
            float posY = 120;

            for (int i = 0; i < 4; i++)
            {
                ShieldFactory.ShieldCreator(posX, posY, GameObject.Name.ShieldGrid_1 + i);
                posX += 130;
            }

            //Debug.WriteLine("-------------------");
            //GameObject pShieldRoot = GONodeMan.Find(GameObject.Name.ShieldRoot);
            //pShieldRoot.Print();

            //--------------------------------------------------------------------------------------------------------------
            //Time Events
            //--------------------------------------------------------------------------------------------------------------

            UFORoot pUFORoot = (UFORoot)GONodeMan.Find(GameObject.Name.UFORoot);

            MoveGrid pMove = new MoveGrid(pAlienGroup, pSndEngine);

            SendUFO pSendIt = new SendUFO(pUFORoot);

            //BombDrop pBombDrop0 = new BombDrop((AlienGroup)pAlienGroup);
            BombDrop pBombDrop1 = new BombDrop((AlienGroup)pAlienGroup);
            BombDrop pBombDrop2 = new BombDrop((AlienGroup)pAlienGroup);

            SpeedUpCheck pSpeedCheck = new SpeedUpCheck((AlienGroup)pAlienGroup);

            //---------------------------------------------------------------------------------------------------------
            //Add Events
            //---------------------------------------------------------------------------------------------------------

            AnimateCrab pAnimCrab = (AnimateCrab)AnimMan.Find(Animation.Name.AnimateCrab);
            AnimateSquid pAnimSquid = (AnimateSquid)AnimMan.Find(Animation.Name.AnimateSquid);
            AnimateOcto pAnimOcto = (AnimateOcto)AnimMan.Find(Animation.Name.AnimateOcto);


            TimerMan.Add(TimeEvent.Name.AnimateOcto, pAnimOcto, 0.60f);
            TimerMan.Add(TimeEvent.Name.AnimateSquid, pAnimSquid, 0.60f);
            TimerMan.Add(TimeEvent.Name.AnimateCrab, pAnimCrab, 0.60f);
            TimerMan.Add(TimeEvent.Name.MoveGrid, pMove, 0.60f);

            //TimerMan.Add(TimeEvent.Name.ColumnShoot, pBombDrop0, 1.0f);
            TimerMan.Add(TimeEvent.Name.ColumnShoot, pBombDrop1, 2.0f);
            TimerMan.Add(TimeEvent.Name.ColumnShoot, pBombDrop2, 4.0f);

            TimerMan.Add(TimeEvent.Name.SpeedCheck, pSpeedCheck, 12.0f);
            TimerMan.Add(TimeEvent.Name.SendUFO, pSendIt, 25.0f);
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
