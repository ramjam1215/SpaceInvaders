using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Glyph : DLink
    {
        public enum Name
        {
            Consolas36pt,
            Consolas20pt,

            NullObject,
            Uninitialized
        }

        public Name name;
        public int key;

        private Azul.Rect pSubRect;
        private Texture pTexture;

        public Glyph() 
            : base()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.pSubRect = new Azul.Rect();
            this.key = 0;
        }

        ~Glyph()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.pSubRect = null;
        }

        public void Set(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            Debug.Assert(this.pSubRect != null);

            this.name = name;

            this.key = key;

            this.pTexture = TextureMan.Find(textName);
            Debug.Assert(this.pTexture != null);

            this.pSubRect.Set(x, y, width, height);

        }

        public void Wash()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.pSubRect.Set(0, 0, 1, 1);
            this.key = 0;
        }

        public Azul.Rect GetAzulSubRect()
        {
            Debug.Assert(this.pSubRect != null);
            return this.pSubRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.pTexture != null);
            return this.pTexture.GetAzulTexture();
        }

        public void DumpGlyph()
        {

        }
    }
}
