using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColObject
    {
        public BoxSprite pColSprite;
        public ColRect poColRect;

        public ColObject(ProxySprite pProxySprite)
        {
            Debug.Assert(pProxySprite != null);

            //use the Proxy's game Sprite for size and shape of collision rectangle
            GameSprite pSprite = pProxySprite.GetRealSprite();
            Debug.Assert(pSprite != null);

            //get the dimensions from the game sprite's rectangle
            // and store it in the collision rectangle
            this.poColRect = new ColRect(pSprite.GetScreenRect() );
            Debug.Assert(this.poColRect != null);

            //create the collision sprite from the collision rectangle
            this.pColSprite = BoxSpriteMan.Add(BoxSprite.Name.BoxSprite1, this.poColRect.x, this.poColRect.y, this.poColRect.width, this.poColRect.height);
            Debug.Assert(this.pColSprite != null);
            this.pColSprite.SetLineColor(1.0f, 1.0f, 1.0f);
        }

        public BoxSprite GetBoxSprite()
        {
            return this.pColSprite;
        }

        public void UpdatePos(float x, float y)
        {
            this.poColRect.x = x;
            this.poColRect.y = y;

            this.pColSprite.PosX(this.poColRect.x);
            this.pColSprite.PosY(this.poColRect.y);

            this.pColSprite.SetScreenRect(this.poColRect.x, this.poColRect.y, this.poColRect.width, this.poColRect.height);
            this.pColSprite.Update();
        }
    }
}
