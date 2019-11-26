using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Font : DLink
    {

        public enum Name
        {
            //Intro Info
            Title,
            NumOfPlayers,
            UFOScore,
            OctoScore,
            CrabScore,
            SquidScore,
            ShootButton,
            MoveButtons,
            TBD,
            

            //InGame
            HiScore,
            HiPoints,


            P1Points,
            P2Points
                ,
            ScoreP1,
            ScoreP2,

            LivesP1,
            LivesP2,

            //EndGame
            GameOver,
            Credits,
            Thankyou,

            NullObject,
            Uninitialized
        }

        public Name name;
        public FontSprite pFontSprite;
        static private String pNullString = "null";

        public Font()
            : base()
        {
            this.name = Name.Uninitialized;
            this.pFontSprite = new FontSprite();
        }

        ~Font()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Font:{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.pFontSprite = null;
        }

        public void Set(Font.Name name, String pMessage, Glyph.Name glypName, float x, float y)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            this.pFontSprite.Set(name, pMessage, glypName, x, y);
        }
        

        public void UpdateMessage(String pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(this.pFontSprite != null);
            this.pFontSprite.UpdateMessage(pMessage);
        }

        public void Wash()
        {
            this.name = Name.Uninitialized;
            this.pFontSprite.Set(Font.Name.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        public void DumpFont()
        {

        }
        
    }
}
