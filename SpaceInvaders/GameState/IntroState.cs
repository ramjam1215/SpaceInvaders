using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class IntroState : GameState
    {
        private static GameMan.State pName = GameMan.State.Intro;

        public override GameMan.State GetStateName()
        {
            return pName;
        }

        public override void Handle()
        {
            //when the person makes the choice we change states
            // then call the handle()
            SpaceInvaders pGame = GameMan.GetGame();

            pGame.SetGameState(GameMan.State.InGame);
            pGame.GetCurrentState().Handle();

        }

        //This loadContent is called in the actual Game.cs
        //So its done before the player even presses input
        // and only called once
        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Create Managers
            //---------------------------------------------------------------------------------------------------------

            TextureMan.Create(6, 1);
            ImageMan.Create(25, 2);
            GameSpriteMan.Create(20, 1);

            //i'm grossly inefficient at this point
            //------------------------------------------------
            BoxSpriteMan.Create(200, 100);
            ProxySpriteMan.Create(200, 100);
            //-------------------------------------------------
            SpriteBatchMan.Create(8, 1);
            GlyphMan.Create();
            FontMan.Create(12, 1);

            GONodeMan.Create(10, 3);
            TimerMan.Create(7, 3);
            ColPairMan.Create(16, 1);

            //Experimental managers
            AnimMan.Create(3, 1);
            Simulation.Create();
            PlayerMan.Create();

            SpaceInvaders pGame = GameMan.GetGame();
            IrrKlang.ISoundEngine pSndEngine = pGame.GetSndEngine();


            //---------------------------------------------------------------------------------------------------------
            // Load the Textures and Font {Consolas20pt}
            //---------------------------------------------------------------------------------------------------------

            TextureMan.Add(Texture.Name.Aliens14x14, "aliens14x14.tga");
            TextureMan.Add(Texture.Name.Aliens, "SpaceInvadersSprites.tga");

            TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            TextureMan.Add(Texture.Name.Consolas20pt, "Consolas20pt.tga");

            FontMan.AddXml(Glyph.Name.Consolas20pt, "Consolas20pt.xml", Texture.Name.Consolas20pt);
            FontMan.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            
            
            //---------------------------------------------------------------------------------------------------------
            // Load Images
            //---------------------------------------------------------------------------------------------------------

            ImageMan.Add(Image.Name.CrabU, Texture.Name.Aliens14x14, 318, 180, 160, 116);
            ImageMan.Add(Image.Name.CrabD, Texture.Name.Aliens14x14, 318, 24, 160, 116);
            ImageMan.Add(Image.Name.OctopusU, Texture.Name.Aliens14x14, 610, 25, 122, 115);
            ImageMan.Add(Image.Name.OctopusD, Texture.Name.Aliens14x14, 610, 180, 119, 112);
            ImageMan.Add(Image.Name.SquidU, Texture.Name.Aliens14x14, 51, 24, 175, 116);
            ImageMan.Add(Image.Name.SquidD, Texture.Name.Aliens14x14, 51, 180, 175, 110);

            ImageMan.Add(Image.Name.Ship, Texture.Name.Aliens14x14, 52, 336, 194, 114);
            ImageMan.Add(Image.Name.UFO, Texture.Name.Aliens14x14, 81, 502, 229, 98);
            ImageMan.Add(Image.Name.Missile, Texture.Name.Aliens14x14, 378, 798, 14, 98);

            ImageMan.Add(Image.Name.AlienSplat, Texture.Name.Aliens14x14, 573, 490, 183, 110);
            ImageMan.Add(Image.Name.ShipSplat, Texture.Name.Aliens, 651, 942, 117, 72);
            ImageMan.Add(Image.Name.BombSplat, Texture.Name.Aliens, 350, 90, 49, 72);
            ImageMan.Add(Image.Name.UFOSplat, Texture.Name.Aliens, 224, 230, 187, 77);

            ImageMan.Add(Image.Name.BombStraight, Texture.Name.Aliens, 216, 94, 16, 56);
            ImageMan.Add(Image.Name.BombZigZag, Texture.Name.Aliens, 349, 161, 23, 59);
            ImageMan.Add(Image.Name.BombCross, Texture.Name.Aliens, 210, 163, 30, 48);

            ImageMan.Add(Image.Name.Brick, Texture.Name.Aliens, 50, 120, 20, 10);
            ImageMan.Add(Image.Name.BrickLeft_Top1, Texture.Name.Aliens, 40, 100, 20, 10);
            ImageMan.Add(Image.Name.BrickLeft_Top2, Texture.Name.Aliens, 40, 110, 20, 10);
            ImageMan.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Aliens, 75, 190, 15, 10);
            ImageMan.Add(Image.Name.BrickRight_Top1, Texture.Name.Aliens, 189, 125, 20, 10);
            ImageMan.Add(Image.Name.BrickRight_Top2, Texture.Name.Aliens, 186, 126, 20, 10);
            ImageMan.Add(Image.Name.BrickRight_Bottom, Texture.Name.Aliens, 130, 190, 20, 10);


            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------

            GameSpriteMan.Add(GameSprite.Name.UFO, Image.Name.UFO, 400, 550, 50.0f, 25.0f);
            GameSpriteMan.Add(GameSprite.Name.Ship, Image.Name.Ship, 400, 25, 40.0f, 25.0f);

            GameSpriteMan.Add(GameSprite.Name.Crab, Image.Name.CrabU, 700, 480, 35, 20);
            GameSpriteMan.Add(GameSprite.Name.Squid, Image.Name.SquidU, 325, 350, 35, 20);
            GameSpriteMan.Add(GameSprite.Name.Octopus, Image.Name.OctopusU, 260, 350, 25, 20);

            GameSpriteMan.Add(GameSprite.Name.AlienSplat, Image.Name.AlienSplat, 50, 50, 30, 20);
            GameSpriteMan.Add(GameSprite.Name.ShipSplat, Image.Name.ShipSplat, 50, 50, 40, 25);
            GameSpriteMan.Add(GameSprite.Name.UFOsplat, Image.Name.UFOSplat, 50, 50, 50, 25);
            GameSpriteMan.Add(GameSprite.Name.BombSplat, Image.Name.BombSplat, 50, 50, 10, 15);

            GameSpriteMan.Add(GameSprite.Name.Missile, Image.Name.Missile, 100, 100, 5.0f, 15.0f);
            GameSpriteMan.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, 200, 200, 10, 20);
            GameSpriteMan.Add(GameSprite.Name.BombStraight, Image.Name.BombStraight, 100, 100, 5, 20);
            GameSpriteMan.Add(GameSprite.Name.BombDagger, Image.Name.BombCross, 100, 100, 10, 20);

            GameSpriteMan.Add(GameSprite.Name.Brick, Image.Name.Brick, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftTop2, Image.Name.BrickLeft_Top2, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightTop2, Image.Name.BrickRight_Top2, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, 10, 5);


            //------------------------------------------------------------------------------------------
            //Attaching the sprites/batches
            //------------------------------------------------------------------------------------------

            SpriteBatch pSB_Intro = SpriteBatchMan.Add(SpriteBatch.Name.IntroScreen);


            SpriteBatch pSB_Aliens = SpriteBatchMan.Add(SpriteBatch.Name.Aliens);
            pSB_Aliens.bToggle = false;

            SpriteBatch pSB_Boxes = SpriteBatchMan.Add(SpriteBatch.Name.Boxes);

            SpriteBatch pSB_Shields = SpriteBatchMan.Add(SpriteBatch.Name.Shields);
            pSB_Shields.bToggle = false;

            SpriteBatch pSB_InGame = SpriteBatchMan.Add(SpriteBatch.Name.InGameScreen);
            pSB_InGame.bToggle = false;

            SpriteBatch pSB_Projectiles = SpriteBatchMan.Add(SpriteBatch.Name.Projectiles);
            pSB_Projectiles.bToggle = false;

            SpriteBatch pSB_GameOver = SpriteBatchMan.Add(SpriteBatch.Name.GameOver);
            pSB_GameOver.bToggle = false;

            //SpriteBatch pSB_Splats = SpriteBatchMan.Add(SpriteBatch.Name.Splats);
            //pSB_Splats.bToggle = false;

            //---------------------------------------------------------------------------------------------------------
            // Font
            //---------------------------------------------------------------------------------------------------------
            
            FontMan.Add(Font.Name.Title, SpriteBatch.Name.IntroScreen, "Space Invaders", Glyph.Name.Consolas36pt, 200, 700);
            FontMan.Add(Font.Name.NumOfPlayers, SpriteBatch.Name.IntroScreen, "Press 1 for Single Player", Glyph.Name.Consolas36pt, 120, 650);
            FontMan.Add(Font.Name.ShootButton, SpriteBatch.Name.IntroScreen, "Space <Bar> To Shoot", Glyph.Name.Consolas36pt, 140, 580);
            FontMan.Add(Font.Name.MoveButtons, SpriteBatch.Name.IntroScreen, "Left and Right Arrows To Move", Glyph.Name.Consolas36pt, 60, 540);
            FontMan.Add(Font.Name.SquidScore, SpriteBatch.Name.IntroScreen, "Squid = 30 points", Glyph.Name.Consolas36pt, 200, 480);
            FontMan.Add(Font.Name.CrabScore, SpriteBatch.Name.IntroScreen, "Crab = 40 points", Glyph.Name.Consolas36pt, 200, 440);
            FontMan.Add(Font.Name.OctoScore, SpriteBatch.Name.IntroScreen, "Octo = 50 points", Glyph.Name.Consolas36pt, 200, 400);
            FontMan.Add(Font.Name.UFOScore, SpriteBatch.Name.IntroScreen, "UFO = 100 points", Glyph.Name.Consolas36pt, 200, 360);
            FontMan.Add(Font.Name.TBD, SpriteBatch.Name.IntroScreen, "2 Player Coming Soon!", Glyph.Name.Consolas36pt, 160, 300);


            FontMan.Add(Font.Name.ScoreP1, SpriteBatch.Name.InGameScreen, "<P1 Score>", Glyph.Name.Consolas36pt, 20, 720);
            FontMan.Add(Font.Name.LivesP1, SpriteBatch.Name.InGameScreen, "P1 Lives:", Glyph.Name.Consolas36pt, 20, 20);
            FontMan.Add(Font.Name.P1Points, SpriteBatch.Name.InGameScreen, "0", Glyph.Name.Consolas36pt, 20, 680);

            FontMan.Add(Font.Name.HiScore, SpriteBatch.Name.InGameScreen, "<HiScore>", Glyph.Name.Consolas36pt, 230, 720);
            FontMan.Add(Font.Name.HiPoints, SpriteBatch.Name.InGameScreen, "0", Glyph.Name.Consolas36pt, 250, 680);

            FontMan.Add(Font.Name.ScoreP2, SpriteBatch.Name.InGameScreen, "<P2 Score>", Glyph.Name.Consolas36pt, 425, 720);
            FontMan.Add(Font.Name.LivesP2, SpriteBatch.Name.InGameScreen, "P2 Lives:", Glyph.Name.Consolas36pt, 400, 20);
            FontMan.Add(Font.Name.P2Points, SpriteBatch.Name.InGameScreen, "0", Glyph.Name.Consolas36pt, 400, 680);

            FontMan.Add(Font.Name.GameOver, SpriteBatch.Name.GameOver, "Game, Set and Match", Glyph.Name.Consolas36pt, 100, 450);
            FontMan.Add(Font.Name.Credits, SpriteBatch.Name.GameOver, "Created By: James Corcoran", Glyph.Name.Consolas36pt, 100, 350);
            FontMan.Add(Font.Name.Thankyou, SpriteBatch.Name.GameOver, "Thanks for Playing!!!", Glyph.Name.Consolas36pt, 100, 250);

            //----------------------------------------------------------------------
            //Missile
            //----------------------------------------------------------------------

            MissileGroup pMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, GameSprite.Name.NullObject, 0, 0.0f, 0.0f);
            pMissileGroup.ActivateGameSprite(pSB_Projectiles);
            pMissileGroup.ActivateCollisionSprite(pSB_Boxes);

            GONodeMan.Attach(pMissileGroup);


            Debug.WriteLine("-------------------");
            pMissileGroup.Print();

            //----------------------------------------------------------------------
            //Ship
            //----------------------------------------------------------------------

            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);

            GONodeMan.Attach(pShipRoot);

            Debug.WriteLine("-------------------");
            pShipRoot.Print();

            //----------------------------------------------------------------------
            //UFO
            //----------------------------------------------------------------------

            UFORoot pUFORoot = new UFORoot(GameObject.Name.UFORoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GONodeMan.Attach(pUFORoot);

            Debug.WriteLine("-------------------");
            pUFORoot.Print();

            //---------------------------------------------------------------------------------------------------------
            // Bomb
            //---------------------------------------------------------------------------------------------------------

            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, GameSprite.Name.NullObject, -100.0f, -100.0f);
            GONodeMan.Attach(pBombRoot);

            Debug.WriteLine("-------------------");
            pBombRoot.Print();

            //---------------------------------------------------------------------
            //Wall Creation
            //---------------------------------------------------------------------

            WallRoot pTWallRoot = new WallRoot(GameObject.Name.WallRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GONodeMan.Attach(pTWallRoot);

            TopWall pTWall = new TopWall(GameObject.Name.TopWall, GameSprite.Name.NullObject, 336, 748, 600, 40);
            pTWall.ActivateCollisionSprite(pSB_Boxes);
            pTWallRoot.Add(pTWall);

            WallRoot pLWallRoot = new WallRoot(GameObject.Name.WallRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GONodeMan.Attach(pLWallRoot);

            LeftWall pLWall = new LeftWall(GameObject.Name.LeftWall, GameSprite.Name.NullObject, 20, 384, 40, 700);
            pLWall.ActivateCollisionSprite(pSB_Boxes);
            pLWallRoot.Add(pLWall);

            WallRoot pRWallRoot = new WallRoot(GameObject.Name.WallRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GONodeMan.Attach(pRWallRoot);

            RightWall pRWall = new RightWall(GameObject.Name.RightWall, GameSprite.Name.NullObject, 653, 384, 40, 700);
            pRWall.ActivateCollisionSprite(pSB_Boxes);
            pRWallRoot.Add(pRWall);

            WallRoot pBWallRoot = new WallRoot(GameObject.Name.WallRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GONodeMan.Attach(pBWallRoot);

            BottomWall pBWall = new BottomWall(GameObject.Name.BottomWall, GameSprite.Name.NullObject, 336, 20, 700, 40);
            pBWall.ActivateCollisionSprite(pSB_Boxes);
            pBWallRoot.Add(pBWall);

            Debug.WriteLine("-------------------");

            //---------------------------------------------------------------------
            //AlienRoot(Group) and ShieldRoot
            //---------------------------------------------------------------------

            Composite pAlienGroup = new AlienGroup(GameObject.Name.AlienGrid, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pAlienGroup.ActivateCollisionSprite(pSB_Boxes);
            GONodeMan.Attach(pAlienGroup);


            Composite pShieldRoot = new ShieldRoot(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GONodeMan.Attach(pShieldRoot);


            //Composite pSplatRoot = new SplatRoot(GameObject.Name.SplatRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            //GONodeMan.Attach(pSplatRoot);

            //--------------------------------------------------------------------------
            // Collision Pair
            //--------------------------------------------------------------------------

            ColPair pGrid_V_LWall = ColPairMan.Add(ColPair.Name.Aliens_V_LWall, pAlienGroup, pLWallRoot);
            Debug.Assert(pGrid_V_LWall != null);
            pGrid_V_LWall.Attach(new GridObserver());

            ColPair pGrid_V_RWall = ColPairMan.Add(ColPair.Name.Aliens_V_RWall, pAlienGroup, pRWallRoot);
            Debug.Assert(pGrid_V_RWall != null);
            pGrid_V_RWall.Attach(new GridObserver());

            ColPair pGrid_V_BWall = ColPairMan.Add(ColPair.Name.Aliens_V_BWall, pAlienGroup, pBWallRoot);
            Debug.Assert(pGrid_V_BWall != null);
            pGrid_V_BWall.Attach(new GridObserver());

            ColPair pMissile_V_Alien = ColPairMan.Add(ColPair.Name.Missile_V_Alien, pMissileGroup, pAlienGroup);
            Debug.Assert(pMissile_V_Alien != null);
            pMissile_V_Alien.Attach(new SplatObserver());
            pMissile_V_Alien.Attach(new RemoveAlienObserver());
            pMissile_V_Alien.Attach(new RemoveMissileObserver());
            pMissile_V_Alien.Attach(new ShipReadyObserver());
            pMissile_V_Alien.Attach(new SndObserver(pSndEngine, "invaderkilled.wav"));
            

            ColPair pAlien_V_Shield = ColPairMan.Add(ColPair.Name.Aliens_V_Shield, pAlienGroup, pShieldRoot);
            Debug.Assert(pAlien_V_Shield != null);
            pAlien_V_Shield.Attach(new RemoveBrickObserver());
            //pAlien_V_Shield.Attach(new SndObserver(pSndEngine, "explosion.wav"));

            ColPair pAlien_V_Ship = ColPairMan.Add(ColPair.Name.Aliens_V_Ship, pAlienGroup, pShipRoot);
            Debug.Assert(pAlien_V_Ship != null);
            pAlien_V_Ship.Attach(new SplatShipObserver());
            pAlien_V_Ship.Attach(new HitShipObserver());
            pAlien_V_Ship.Attach(new SndObserver(pSndEngine, "explosion.wav"));
            pAlien_V_Ship.Attach(new P1GameOverObs());

            //--------------------------------------------------------------------------------------------------------------

            ColPair pCollide_UFO_RWall = ColPairMan.Add(ColPair.Name.UFO_V_RWall, pUFORoot, pRWallRoot);
            Debug.Assert(pCollide_UFO_RWall != null);
            pCollide_UFO_RWall.Attach(new MissedUFOobserver());
            pCollide_UFO_RWall.Attach(new RemoveSndUFO());

            ColPair pCollide_UFO_LWall = ColPairMan.Add(ColPair.Name.UFO_V_LWall, pUFORoot, pLWallRoot);
            Debug.Assert(pCollide_UFO_LWall != null);
            pCollide_UFO_LWall.Attach(new MissedUFOobserver());
            pCollide_UFO_LWall.Attach(new RemoveSndUFO());

            ColPair pCollide_Missile_V_UFO = ColPairMan.Add(ColPair.Name.Missile_V_UFO, pMissileGroup, pUFORoot);
            Debug.Assert(pCollide_Missile_V_UFO != null);
            pCollide_Missile_V_UFO.Attach(new RemoveMissileObserver());
            pCollide_Missile_V_UFO.Attach(new RemoveUFOobserver());
            pCollide_Missile_V_UFO.Attach(new ShipReadyObserver());
            pCollide_Missile_V_UFO.Attach(new UFOSplatObs());
            pCollide_Missile_V_UFO.Attach(new SndObserver(pSndEngine, "invaderkilled.wav"));
            pCollide_Missile_V_UFO.Attach(new RemoveSndUFO());

            //-----------------------------------------------------------------------------------------------------------------
            ColPair pMissile_V_TWall = ColPairMan.Add(ColPair.Name.Missile_V_TWall, pMissileGroup, pTWallRoot);
            Debug.Assert(pMissile_V_TWall != null);
            pMissile_V_TWall.Attach(new ShipReadyObserver());
            pMissile_V_TWall.Attach(new RemoveMissileObserver());

            ColPair pBomb_V_Wall = ColPairMan.Add(ColPair.Name.Bomb_V_Wall, pBombRoot, pBWallRoot);
            Debug.Assert(pBomb_V_Wall != null);
            pBomb_V_Wall.Attach(new BombObserver());

            ColPair pMissile_V_Shield = ColPairMan.Add(ColPair.Name.Missile_V_Shield, pMissileGroup, pShieldRoot);
            Debug.Assert(pMissile_V_Shield != null);
            pMissile_V_Shield.Attach(new RemoveBrickObserver());
            pMissile_V_Shield.Attach(new RemoveMissileObserver());
            pMissile_V_Shield.Attach(new ShipReadyObserver());
            pMissile_V_Shield.Attach(new SndObserver(pSndEngine, "explosion.wav"));

            ColPair pBomb_V_Shield = ColPairMan.Add(ColPair.Name.Bomb_V_Shield, pBombRoot, pShieldRoot);
            Debug.Assert(pBomb_V_Shield != null);
            pBomb_V_Shield.Attach(new RemoveBrickObserver());
            pBomb_V_Shield.Attach(new BombObserver());
            pBomb_V_Shield.Attach(new SndObserver(pSndEngine, "explosion.wav"));

            ColPair pBomb_V_Ship = ColPairMan.Add(ColPair.Name.Ship_V_Bomb, pBombRoot, pShipRoot);
            Debug.Assert(pBomb_V_Ship != null);
            pBomb_V_Ship.Attach(new SplatShipObserver());
            pBomb_V_Ship.Attach(new BombObserver());
            pBomb_V_Ship.Attach(new HitShipObserver());
            pBomb_V_Ship.Attach(new SndObserver(pSndEngine, "explosion.wav"));
            pBomb_V_Ship.Attach(new P1GameOverObs());
            

            ColPair pBomb_V_Missile = ColPairMan.Add(ColPair.Name.Bomb_V_Missile, pBombRoot, pMissileGroup);
            Debug.Assert(pBomb_V_Missile != null);
            pBomb_V_Missile.Attach(new BombSplatObs());
            pBomb_V_Missile.Attach(new BombMissileObserver());
            pBomb_V_Missile.Attach(new SndObserver(pSndEngine, "explosion.wav"));
            pBomb_V_Missile.Attach(new ShipReadyObserver());
            //------------------------------------------------------------------------------------------------------------------

            ColPair pShip_V_LWall = ColPairMan.Add(ColPair.Name.Ship_V_LWall, pShipRoot, pLWallRoot);
            Debug.Assert(pShip_V_LWall != null);
            pShip_V_LWall.Attach(new ShipMovementObserver());

            ColPair pShip_V_RWall = ColPairMan.Add(ColPair.Name.Ship_V_RWall, pShipRoot, pRWallRoot);
            Debug.Assert(pShip_V_RWall != null);
            pShip_V_RWall.Attach(new ShipMovementObserver());

            //----------------------------------------------------------------------------------------------------------
            //Animate Sprites and Movement Commands
            //----------------------------------------------------------------------------------------------------------

            //Tests on my Animation Manager
            AnimateCrab pAnimS1 = new AnimateCrab(Animation.Name.AnimateCrab, GameSprite.Name.Crab);
            pAnimS1.Attach(Image.Name.CrabD);
            pAnimS1.Attach(Image.Name.CrabU);

            AnimateSquid pAnimS2 = new AnimateSquid(Animation.Name.AnimateSquid, GameSprite.Name.Squid);
            pAnimS2.Attach(Image.Name.SquidD);
            pAnimS2.Attach(Image.Name.SquidU);

            AnimateOcto pAnimS3 = new AnimateOcto(Animation.Name.AnimateOcto, GameSprite.Name.Octopus);
            pAnimS3.Attach(Image.Name.OctopusD);
            pAnimS3.Attach(Image.Name.OctopusU);

            AnimMan.Attach(pAnimS1);
            AnimMan.Attach(pAnimS2);
            AnimMan.Attach(pAnimS3);

            //---------------------------------------------------------------------------------------------------------
            //Add Input
            //---------------------------------------------------------------------------------------------------------
            InputSubject pInputSubject;
            pInputSubject = InputManager.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputManager.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputManager.GetSpaceBarSubject();
            pInputSubject.Attach(new ShootObserver());

            //first player
            pInputSubject = InputManager.Get1keySubject();
            pInputSubject.Attach(new Player1Observer());

            //add second player observer
            pInputSubject = InputManager.Get2keySubject();
            pInputSubject.Attach(new Player2Observer());


            pInputSubject = InputManager.GetCkeySubject();
            pInputSubject.Attach(new ToggleColObserver());


            //Simulator
            //I am testing because i think we will need to pause the game
            //for the timer manager and the lives for the ship
            Simulation.SetState(Simulation.State.RealTime);

        }

        public override void Update()
        {
            //We are waiting for the player to choose 1 or 2 players
            InputManager.Update();

        }

        public override void Draw()
        {
            //Draw/Render the Sprites
            SpriteBatchMan.Draw();
        }
    }
}
