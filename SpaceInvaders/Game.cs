using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpaceInvaders : Azul.Game
    {

        public IrrKlang.ISoundEngine sndEngine = new IrrKlang.ISoundEngine();
        private GameState pGameState = null;

        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("V.0.76");
            this.SetWidthHeight(672, 768);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------

        public override void LoadContent()
        {
            this.pGameState.LoadContent();
            //---------------------------------------------------------------------------------------------------------
            // Demo variables
            //---------------------------------------------------------------------------------------------------------

            Debug.WriteLine("(Width,Height): {0}, {1}", this.GetScreenWidth(), this.GetScreenHeight());
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update()
        {
            // Add your update below this line: ----------------------------
            this.pGameState.Update();
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            //Draw/Render the Sprites
            this.pGameState.Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
            TextureMan.Destroy();
            ImageMan.Destroy();
            GameSpriteMan.Destroy();
            BoxSpriteMan.Destroy();
            ProxySpriteMan.Destroy();
            SpriteBatchMan.Destroy();
            GONodeMan.Destroy();
            TimerMan.Destroy();
            ColPairMan.Destroy();
            FontMan.Destroy();

            Simulation.Destroy();
            AnimMan.Destroy();
            InputManager.Destroy();
            GlyphMan.Destroy();
            //ShipMan.Destroy();
            //GraveyardMan.Destroy();
        }

        public void SetGameState(GameMan.State inState)
        {
            this.pGameState = GameMan.GetGameState(inState);
        }

        public GameState GetCurrentState()
        {
            return this.pGameState;
        }

        public IrrKlang.ISoundEngine GetSndEngine()
        {
            return this.sndEngine;
        }

    }
}
