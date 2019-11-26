using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FontSprite : SpriteBase
    {

        public Font.Name name;
        private Azul.Sprite pAzulSprite;
        private Azul.Rect pScreenRect;
        private Azul.Color pColor;

        private String pMessage;
        public Glyph.Name glyphName;

        public float x;
        public float y;

        public FontSprite()
            : base()
        {
            this.name = Font.Name.Uninitialized;
            
            this.pAzulSprite = new Azul.Sprite();
            this.pScreenRect = new Azul.Rect();
            this.pColor = new Azul.Color(1, 1, 1);

            this.pMessage = null;
            this.glyphName = Glyph.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
        }

        ~FontSprite()
        {
            this.pAzulSprite = null;
            this.pScreenRect = null;
            this.pColor = null;
            this.pMessage = null;
        }

        public void Set(Font.Name name, String pMessage, Glyph.Name glyphName, float x, float y)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;

            this.x = x;
            this.y = y;

            this.name = name;
            this.glyphName = glyphName;

            //set color to white
            Debug.Assert(this.pColor != null);
            this.pColor.Set(1.0f, 1.0f, 1.0f);

        }

        public void SetColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.pColor != null);
            this.pColor.Set(red, green, blue, alpha);
        }

        public void UpdateMessage( String pMessage)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;
        }

        public override void Render()
        {
            Debug.Assert(this.pAzulSprite != null);
            Debug.Assert(this.pColor != null);
            Debug.Assert(this.pScreenRect != null);
            Debug.Assert(this.pMessage != null);
            Debug.Assert(this.pMessage.Length > 0);

            float xTemp = this.x;
            float yTemp = this.y;

            float xEnd = this.x;

            for(int i = 0; i < this.pMessage.Length; i++)
            {
                int key = Convert.ToByte(pMessage[i]);

                Glyph pGlyph = GlyphMan.Find(this.glyphName, key);
                Debug.Assert(pGlyph != null);

                xTemp = xEnd + (pGlyph.GetAzulSubRect().width / 2);
                this.pScreenRect.Set(xTemp, yTemp, pGlyph.GetAzulSubRect().width, pGlyph.GetAzulSubRect().height);

                pAzulSprite.Swap(pGlyph.GetAzulTexture(), pGlyph.GetAzulSubRect(), this.pScreenRect, this.pColor);

                pAzulSprite.Update();
                pAzulSprite.Render();

                xEnd = (pGlyph.GetAzulSubRect().width / 2) + xTemp;
            }
        }

        public override void Update()
        {
            Debug.Assert(this.pAzulSprite != null);
        }

        public void Dump()
        {

        }
    }
}
