using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Image_Link : DLink
    {

    }
    public class Image : Image_Link
    {
        public enum Name
        {
            UFO,
            Ship,

            CrabU,
            CrabD,

            SquidU,
            SquidD,

            OctopusU,
            OctopusD,

            AlienSplat,
            ShipSplat,
            UFOSplat,
            BombSplat,

            Missile,

            BombStraight,
            BombZigZag,
            BombCross,

            Brick,
            BrickLeft_Top1,
            BrickLeft_Top2,
            BrickLeft_Bottom,
            BrickRight_Top1,
            BrickRight_Top2,
            BrickRight_Bottom,

            Default,
            NullObject,
            Unitialized
        }

        private Name name;
        private Texture pTexture;
        private Azul.Rect poRect;

        

        public Image()
            : base()
        {
            this.poRect = new Azul.Rect();
            Debug.Assert(this.poRect != null);

            this.Clear();

        }

        ~Image()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Image(): {0} ", this.GetHashCode());
#endif
            this.name = Name.Unitialized;
            this.pTexture = null;
            this.poRect = null;
        }

        public void Set(Name name, Texture pTexture, float x, float y, float width, float height)
        {
            this.name = name;

            Debug.Assert(pTexture != null);
            this.pTexture = pTexture;

            this.poRect.Set(x, y, width, height);
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }

        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.poRect != null);
            return this.poRect;
        }

        public Name GetName()
        {
            return this.name;
        }

        public void SetName(Name name)
        {
            this.name = name;
        }

        public new void Clear()
        {
            this.name = Name.Unitialized;
            this.pTexture = null;

            //checked in the meta data
            this.poRect.Clear();
        }

        public void Wash()
        {

            base.Clear();
            this.Clear();
        }

        public void DumpImage()
        {
            Debug.WriteLine("Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("Image Dimensions");
            Debug.WriteLine("X:{0}, Y:{1}, W:{2}, H:{3}", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            if (this.pTexture != null)
            {
                Debug.WriteLine("Texture: {0}", this.pTexture.GetName());
            }

            else
            {
                Debug.WriteLine("Texture: null");
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("previous: null");
            }
            else
            {
                Image pTemp = (Image)this.pPrev;
                Debug.WriteLine("previous: {0}, {1}", pTemp.name, pTemp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("next: null");
            }
            else
            {
                Image pTemp = (Image)this.pNext;
                Debug.WriteLine("next: {0}, {1}", pTemp.name, pTemp.GetHashCode());

            }
        }



    }
}

