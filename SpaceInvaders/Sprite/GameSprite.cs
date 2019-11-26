using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameSprite_Link : SpriteBase
    {

    }
    public class GameSprite : GameSprite_Link
    {
        public enum Name
        {
            UFO,
            Ship,

            Crab,
            Octopus,
            Squid,

            Missile,
            BombZigZag,
            BombStraight,
            BombDagger,

            Brick,
            Brick_LeftTop1,
            Brick_LeftTop2,
            Brick_LeftBottom,
            Brick_RightTop1,
            Brick_RightTop2,
            Brick_RightBottom,

            AlienSplat,
            ShipSplat,
            UFOsplat,
            BombSplat,

            NullObject,

            Unitialized
        }

        private Name name;
        private Image pImage;
        private Azul.Sprite poAzulSprite;
        private Azul.Rect poScreenRect;
        private Azul.Color poAzulColor;

        private float x;
        private float y;
        public float sx;
        public float sy;
        private float angle;

        static private Azul.Color psTempColor = new Azul.Color(1, 1, 1);
        //static Azul.Rect poRect = new Azul.Rect();


        public GameSprite()
            : base()
        {
            this.name = GameSprite.Name.Unitialized;

            //use the default for now and replace later in the Set
            this.pImage = ImageMan.Find(Image.Name.Default);
            Debug.Assert(this.pImage != null);

            this.poScreenRect = new Azul.Rect();
            Debug.Assert(this.pImage != null);

            //make sure nothing is there already?
            this.poScreenRect.Clear();

            Debug.Assert(GameSprite.psTempColor != null);
            GameSprite.psTempColor.Set(1, 1, 1);

            this.poAzulSprite = new Azul.Sprite(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poScreenRect, psTempColor);
            Debug.Assert(this.poAzulSprite != null);

            this.poAzulColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.poAzulColor != null);

            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            this.sx = poAzulSprite.sx;
            this.sy = poAzulSprite.sy;
            this.angle = poAzulSprite.angle;
        }

        ~GameSprite()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~GameSprite(): {0} ", this.GetHashCode());
#endif
            this.name = Name.Unitialized;
            this.pImage = null;
            this.poAzulSprite = null;
            this.poAzulColor = null;
        }

        public void Set(GameSprite.Name name, Image pImage, float x, float y, float width, float height, Azul.Color Color)
        {
            Debug.Assert(pImage != null);
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(this.poScreenRect != null);

            this.name = name;
            this.pImage = pImage;
            this.poScreenRect.Set(x, y, width, height);

            if (Color == null)
            {
                Color = new Azul.Color(1.0f, 1.0f, 1.0f);
            }

            this.poAzulSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poScreenRect, this.poAzulColor);

            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            this.sx = poAzulSprite.sx;
            this.sy = poAzulSprite.sy;
            this.angle = poAzulSprite.angle;
        }

        public new void Clear()
        {
            this.name = GameSprite.Name.Unitialized;

            this.pImage = null;
            this.poAzulColor.Set(1, 1, 1);

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;

        }

        public void Wash()
        {
            base.Clear();
            this.Clear();

        }

        public Name GetName()
        {
            return this.name;
        }

        public void SetName(Name name)
        {
            this.name = name;
        }

        public Image GetImage()
        {
            return this.pImage;
        }

        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.poScreenRect != null);
            return this.poScreenRect;
        }

        public Azul.Sprite GetAzulSprite()
        {
            return this.poAzulSprite;
        }

        public void PosX(float x)
        {
            this.x = x;
        }

        public void PosY(float y)
        {
            this.y = y;
        }
        public void DumpSprite()
        {
            Debug.WriteLine("Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("X: {0}, Y:{1}, SX:{2}, SY:{3}, Angle:{4}", this.x, this.y, this.sx, this.sy, this.angle);

            if (this.pPrev == null)
            {
                Debug.WriteLine("previous: null");
            }

            else
            {
                GameSprite pTemp = (GameSprite)this.pPrev;
                Debug.WriteLine("previous: {0}, {1}", pTemp.name, pTemp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("next: null");
            }

            else
            {
                GameSprite pTemp = (GameSprite)this.pNext;
                Debug.WriteLine("next: {0}, {1}", pTemp.name, pTemp.GetHashCode());

            }
        }

        public override void Update()
        {
            this.poAzulSprite.x = this.x;
            this.poAzulSprite.y = this.y;
            this.poAzulSprite.sx = this.sx;
            this.poAzulSprite.sy = this.sy;
            this.poAzulSprite.angle = this.angle;

            this.poAzulSprite.Update();
        }

        public override void Render()
        {
            this.poAzulSprite.Render();
        }

        public void SwapTexture(Texture.Name textureName)
        {
            Texture pTexture = TextureMan.Find(textureName);

            this.poAzulSprite.SwapTexture(pTexture.GetAzulTexture());
        }

        public void SwapTextureRect(float x, float y, float width, float height)
        {
            this.poAzulSprite.SwapTextureRect(new Azul.Rect(x, y, width, height));
        }

        public void SwapImage(Image pNewImage)
        {
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(pNewImage != null);

            this.pImage = pNewImage;
            this.poAzulSprite.SwapTexture(this.pImage.GetAzulTexture());
            this.poAzulSprite.SwapTextureRect(this.pImage.GetAzulRect());
        }


       
    }
}

