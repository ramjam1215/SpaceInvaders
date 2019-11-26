using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerMan
    {
        //umm...... mimicking ShipMan for now
        //not sure what i'm really doing
        //is it too easy, and thats why i'm hesistent?
        //or just tired
        public enum Player
        {
            Player1,
            Player2,
        }

        private static PlayerMan instance = null;

        private float player1Score;
        private float player1Lives;



        private float player2Score;
        private float player2Lives;

        private float fHiScore;
        private Player CurrentPlayer;

        private PlayerMan()
        {
            this.player1Score = 0;
            this.player1Lives = 3;

            this.player2Score = 0;
            this.player2Lives = 3;

            this.fHiScore = 0;

            //Always start with player1
            this.CurrentPlayer = Player.Player1;
        }

        
        public static void Create()
        {
            Debug.Assert(instance == null);

            if(instance == null)
            {
                instance = new PlayerMan();
            }

            Debug.Assert(instance != null);

        }

        public static Player GetCurrentPlayer()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            return pPMan.CurrentPlayer;
        }

        public static void SetCurrentPlayer(Player player)
        {
            PlayerMan pPlayerMan = PlayerMan.PrivInstance();
            Debug.Assert(pPlayerMan != null);

            

            switch (player)
            {
                case PlayerMan.Player.Player1:
                    pPlayerMan.CurrentPlayer = Player.Player1;
                    break;

                case PlayerMan.Player.Player2:
                    pPlayerMan.CurrentPlayer = Player.Player2;
                    break;

                default:
                    Debug.Assert(false);
                    break;
                    
            }

        }

        public static void UpdateHiScore()
        {

            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            //thinking...........
            //get player 1 score
            //get player 2 score
            
            float fP1Score = pPMan.player1Score;
            float fP2Score = pPMan.player2Score;

            float fHiScore = pPMan.fHiScore;

            //check if either Score is higher than hiScore
            if (fP1Score >= fHiScore)
            {
                //if one of them is then update the new hiScore
                pPMan.fHiScore = fP1Score;
            }

            else if(fP2Score >= fHiScore)
            {
                //if one of them is then update the new hiScore
                pPMan.fHiScore = fP2Score;
            }

            else
            {
                // no one got better than highScore
            }

        }

        public static float GetHiScore()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            return pPMan.fHiScore;
        }

        //--------------------------------------------------
        //Player 1
        //--------------------------------------------------

        public static void SetP1Score(float points)
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            pPMan.player1Score += points;
        }

        public static float GetP1Score()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            return pPMan.player1Score;
        }

        public static void P1TakeLife()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            pPMan.player1Lives -= 1;
        }

        public static float GetP1Lives()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            return pPMan.player1Lives;
        }

        //--------------------------------------------------
        //Player 2
        //--------------------------------------------------

        public static void SetP2Score(float points)
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            pPMan.player1Score += points;
        }

        public static float GetP2Score()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            return pPMan.player1Score;
        }

        public static void P2TakeLife()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            pPMan.player2Score -= 1;
        }

        public static float GetP2Lives()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            return pPMan.player2Lives;
        }

        public static void ClearStuff()
        {
            PlayerMan pPMan = PlayerMan.PrivInstance();
            Debug.Assert(pPMan != null);

            pPMan.player1Lives = 3;
            pPMan.player1Score = 0;

            pPMan.player2Lives = 3;
            pPMan.player2Score = 0;
        }

        private static PlayerMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }
    }
}
