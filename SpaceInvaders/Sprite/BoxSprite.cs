using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BoxSprite_Link : SpriteBase
    {

    }
    public class BoxSprite : BoxSprite_Link
    {
        public enum Name
        {
            BoxSprite1,
            BoxSprite2,
            BoxSprite3,
            BoxSprite4,

            Unitialized
        }

        private Name name;
        private Azul.SpriteBox poAzulSpriteBox;
        private Azul.Color poLineColor;

        static private Azul.Rect psTempRect = new Azul.Rect();
        static private Azul.Color psTempColor = new Azul.Color(1, 1, 1);

        //new stuff public should be private and width and height are new
        private float x;
        private float y;
        private float sx;
        private float sy;
        private float angle;

        public BoxSprite()
            : base()
        {
            this.name = BoxSprite.Name.Unitialized;

            Debug.Assert(BoxSprite.psTempRect != null);
            BoxSprite.psTempRect.Set(0, 0, 1, 1);

            Debug.Assert(BoxSprite.psTempColor != null);
            BoxSprite.psTempColor.Set(1, 1, 1);

            this.poAzulSpriteBox = new Azul.SpriteBox(psTempRect, psTempColor);
            Debug.Assert(this.poAzulSpriteBox != null);

            this.poLineColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.poLineColor != null);

            this.x = poAzulSpriteBox.x;
            this.y = poAzulSpriteBox.y;
            this.sx = poAzulSpriteBox.sx;
            this.sy = poAzulSpriteBox.sy;
            this.angle = poAzulSpriteBox.angle;
        }

        ~BoxSprite()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~BoxSprite():{0} ", this.GetHashCode());
#endif
            this.name = Name.Unitialized;
            this.poAzulSpriteBox = null;
            this.poLineColor = null;
        }

        public void Set(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pLineColor)
        {
            
            //created in the constructor, double-check
            Debug.Assert(this.poAzulSpriteBox != null);
            Debug.Assert(this.poLineColor != null);

            Debug.Assert(psTempRect != null);
            psTempRect.Set(x, y, width, height);

            this.name = name;

            if( pLineColor == null)
            {
                this.poLineColor.Set(1, 1, 1);
            }

            else
            {
                this.poLineColor.Set(pLineColor);
            }

            this.poAzulSpriteBox.Swap(psTempRect, this.poLineColor);
            Debug.Assert(poAzulSpriteBox != null);

            this.x = poAzulSpriteBox.x;
            this.y = poAzulSpriteBox.y;
            this.sx = poAzulSpriteBox.sx;
            this.sy = poAzulSpriteBox.sy;
            this.angle = poAzulSpriteBox.angle;

        }

        public void Set(BoxSprite.Name name, float x, float y, float width, float height)
        {
            //created in the constructor, double-check
            Debug.Assert(this.poAzulSpriteBox != null);
            Debug.Assert(this.poLineColor != null);

            Debug.Assert(psTempRect != null);
            psTempRect.Set(x, y, width, height);

            this.name = name;

            this.poAzulSpriteBox.Swap(psTempRect, this.poLineColor);
            Debug.Assert(poAzulSpriteBox != null);

            this.x = poAzulSpriteBox.x;
            this.y = poAzulSpriteBox.y;
            this.sx = poAzulSpriteBox.sx;
            this.sy = poAzulSpriteBox.sy;
            this.angle = poAzulSpriteBox.angle;
        }

        public new void Clear()
        {
            this.name = Name.Unitialized;

            this.poLineColor.Set(1, 1, 1);

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

        public void PosX(float x)
        {
            this.x = x;
        }

        public void PosY(float y)
        {
            this.y = y;
        }

        public override void Update()
        {
            this.poAzulSpriteBox.x = this.x;
            this.poAzulSpriteBox.y = this.y;
            this.poAzulSpriteBox.sx = this.sx;
            this.poAzulSpriteBox.sy = this.sy;
            this.poAzulSpriteBox.angle = this.angle;

            this.poAzulSpriteBox.Update();
        }

        public override void Render()
        {
            this.poAzulSpriteBox.Render();
        }

        public void SetLineColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.poLineColor != null);
            this.poLineColor.Set(red, green, blue, alpha);
        }

        public void SetScreenRect(float x, float y, float width, float height)
        {
            this.Set(this.name, x, y, width, height);
        }

        public void DumpBoxSprite()
        {
            Debug.WriteLine("Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("X: {0}, Y:{1}, SX:{2}, SY:{3}", this.x, this.y, this.sx, this.sy);
            Debug.WriteLine("Red: {0}, Green: {1}, Blue:{2}", this.poLineColor.red, this.poLineColor.green, this.poLineColor.blue);

            if (this.pPrev == null)
            {
                Debug.WriteLine("previous: null");
            }
            else
            {
                BoxSprite pTemp = (BoxSprite)this.pPrev;
                Debug.WriteLine("previous: {0}, {1}", pTemp.name, pTemp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("next: null");
            }
            else
            {
                BoxSprite pTemp = (BoxSprite)this.pNext;
                Debug.WriteLine("next: {0}, {1}", pTemp.name, pTemp.GetHashCode());

            }
        }


    }
}
