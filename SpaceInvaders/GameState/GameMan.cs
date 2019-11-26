using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameMan
    {

        public enum State
        {
            Intro,
            InGame,
            LVL2,
            GameOver
        }

        private static GameMan instance = null;

        private SpaceInvaders poSpaceInvaders;

        private IntroState pIntro;
        private InGameState pInGame;
        private InGameStateLV2 pLVL2;
        private EndGameState pEnd;

        //test for two player


        private GameMan()
        {
            this.poSpaceInvaders = null;
            this.pIntro = new IntroState();
            this.pInGame = new InGameState();
            this.pLVL2 = new InGameStateLV2();
            this.pEnd = new EndGameState();
        }

        public static void Create()
        {
            Debug.Assert(instance == null);

            if(instance == null)
            {
                instance = new GameMan();
            }

            Debug.Assert(instance != null);

            instance.poSpaceInvaders = new SpaceInvaders();
            instance.poSpaceInvaders.SetGameState(GameMan.State.Intro);
        }

        public static SpaceInvaders GetGame()
        {
            GameMan pGameMan = GameMan.PrivInstance();

            Debug.Assert(pGameMan != null);
            Debug.Assert(pGameMan.poSpaceInvaders != null);

            return pGameMan.poSpaceInvaders;
        }

        public static GameState GetGameState(State state)
        {
            GameMan pGameMan = GameMan.PrivInstance();
            Debug.Assert(pGameMan != null);

            GameState pGameState = null;

            switch (state)
            {
                case GameMan.State.Intro:
                    pGameState = pGameMan.pIntro;
                    break;

                case GameMan.State.InGame:
                    pGameState = pGameMan.pInGame;
                    break;

                case GameMan.State.GameOver:
                    pGameState = pGameMan.pEnd;
                    break;

                case GameMan.State.LVL2:
                    pGameState = pGameMan.pLVL2;
                    break;

                default:
                    Debug.Assert(false);
                    break;  
            }

            return pGameState;
        }


        private static GameMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }
    }
}
