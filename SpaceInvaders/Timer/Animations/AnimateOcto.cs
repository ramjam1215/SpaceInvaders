using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimateOcto : Animation
    {
        public SLink poFirstImage;
        public SLink pCurrentImage;

        public GameSprite pSprite;

        public AnimateOcto(Animation.Name animName, GameSprite.Name spriteName) 
            : base(animName)
        {
            this.poFirstImage = null;
            this.pCurrentImage = null;

            this.pSprite = GameSpriteMan.Find(spriteName);
            Debug.Assert(this.pSprite != null);

        }

        ~AnimateOcto()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Animation(): {0} ", this.GetHashCode());
#endif
            this.pCurrentImage = null;
            this.poFirstImage = null;
            this.pSprite = null;
        }

        public void Attach(Image.Name imageName)
        {
            Image pImage = ImageMan.Find(imageName);
            Debug.Assert(pImage != null);

            ImageHolder pImageHolder = new ImageHolder(pImage);
            Debug.Assert(pImageHolder != null);


            SLink.AddToFront(ref this.poFirstImage, pImageHolder);


            this.pCurrentImage = pImageHolder;
        }


        public override void Execute(float deltaTime)
        {

            ImageHolder pImageHolder = (ImageHolder)this.pCurrentImage.pSNext;


            if (pImageHolder == null)
                pImageHolder = (ImageHolder)this.poFirstImage;


            this.pCurrentImage = pImageHolder;


            this.pSprite.SwapImage(pImageHolder.pImage);


            TimerMan.Add(TimeEvent.Name.AnimateOcto, this, deltaTime);
        }
    }
}

