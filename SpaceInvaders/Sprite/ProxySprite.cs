using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ProxySprite_Link : SpriteBase
    {

    }
    public class ProxySprite : ProxySprite_Link
    {
        public enum Name
        {
            Proxy,
            Unitialized
        }

        private ProxySprite.Name name;
        private float x;
        private float y;
        public float sx;
        public float sy;
        private GameSprite pSprite;
        

        public ProxySprite()
            : base()
        {
            this.name = ProxySprite.Name.Unitialized;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = null;
        }


        ~ProxySprite()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~ProxySprite:{0} ", this.GetHashCode());
#endif
            this.name = ProxySprite.Name.Unitialized;
            this.pSprite = null;
        }

        public ProxySprite(GameSprite.Name name)
        {
            this.name = ProxySprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = GameSpriteMan.Find(name);
            Debug.Assert(pSprite != null);
        }

        public void Set(GameSprite.Name name)
        {
            this.name = ProxySprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = GameSpriteMan.Find(name);
            Debug.Assert(pSprite != null);
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }
        public new void Clear()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.name = Name.Unitialized;
            this.pSprite = null;
        }

        private void PrivPushToReal()
        {
            Debug.Assert(this.pSprite != null);

            this.pSprite.PosX(this.x);
            this.pSprite.PosY(this.y);
            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }

        public Name GetName()
        {
            return this.name;
        }

        public void SetName(Name name)
        {
            this.name = name;
        }

        public void SetRealSprite(GameSprite sprite)
        {
            this.pSprite = sprite;
        }

        public GameSprite GetRealSprite()
        {
            return this.pSprite;
        }

        public void PosX(float x)
        {
            this.x = x;
        }

        public void PosY(float y)
        {
            this.y = y;
        }


        public void DumpProxy()
        {
            Debug.WriteLine("Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("PosX: {0}, PosY: {1}", this.x, this.y);
            Debug.WriteLine("Image: {0}, ({1})", this.pSprite.GetImage().GetName(), this.pSprite.GetImage().GetHashCode());

            if (this.pPrev == null)
            {
                Debug.WriteLine("previous: null");
            }
            else
            {
                ProxySprite pTemp = (ProxySprite)this.pPrev;
                Debug.WriteLine("previous: {0}, {1}", pTemp.name, pTemp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("next: null");
            }
            else
            {
                ProxySprite pTemp = (ProxySprite)this.pNext;
                Debug.WriteLine("next: {0}, {1}", pTemp.name, pTemp.GetHashCode());

            }
        }

        public override void Render()
        {
            this.PrivPushToReal();
            this.pSprite.Update();
            this.pSprite.Render();
        }

        public override void Update()
        {
            this.PrivPushToReal();
            this.pSprite.Update();
        }



    }
}
