using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Texture_Link : DLink
    {

    }
    public class Texture : Texture_Link
    {
        public enum Name
        {
            Aliens,
            Aliens14x14,

            Consolas20pt,
            Consolas36pt,

            Shields_n_Bombs,

            Default,
            NullObject,
            Uninitialized
        }

        private Name name;

        //storing Azul texture in the texture class
        private Azul.Texture poAzulTexture;

        //static texture
        static private Azul.Texture psDefaultAzulTexture = new Azul.Texture("HotPink.tga");

        public Texture()
            : base()
        {
            Debug.Assert(Texture.psDefaultAzulTexture != null);

            this.name = Name.Default;

            this.poAzulTexture = psDefaultAzulTexture;
            Debug.Assert(this.poAzulTexture != null);

        }

        ~Texture()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Texture(): {0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.poAzulTexture = null;
        }
        public void Set(Name name, String pTexName)
        {
            Debug.Assert(pTexName != null);

            this.name = name;

            //Texture Swap
            this.poAzulTexture = new Azul.Texture(pTexName);
            Debug.Assert(this.poAzulTexture != null);
        }

        public new void Clear()
        {
            this.name = Name.Uninitialized;
            this.poAzulTexture = null;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.poAzulTexture != null);
            return this.poAzulTexture;
        }

        public Name GetName()
        {
            return this.name;
        }

        public void SetName(Name name)
        {
            this.name = name;
        }


        public void DumpTexture()
        {
            Debug.WriteLine("");
            Debug.WriteLine("Name: {0} ({1})", this.name, this.GetHashCode());

            if (this.poAzulTexture == null)
            {
                Debug.WriteLine("Texture: null \n");
            }
            else
            {
                Debug.WriteLine("Texture: {0} \n", this.poAzulTexture.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("previous: null");
            }
            else
            {
                Texture pTemp = (Texture)this.pPrev;
                Debug.WriteLine("previous: {0}, {1}", pTemp.name, pTemp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("next: null");
            }
            else
            {
                Texture pTemp = (Texture)this.pNext;
                Debug.WriteLine("next: {0}, {1}", pTemp.name, pTemp.GetHashCode());

            }
        }

    }
}
