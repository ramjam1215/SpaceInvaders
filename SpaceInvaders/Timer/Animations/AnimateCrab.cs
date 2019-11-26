using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimateCrab : Animation
    {
        public SLink poFirstImage;
        public SLink pCurrentImage;

        public GameSprite pSprite;

        public AnimateCrab(Animation.Name animName, GameSprite.Name spriteName) 
            : base(animName)
        {
            this.poFirstImage = null;
            this.pCurrentImage = null;

            this.pSprite = GameSpriteMan.Find(spriteName);
            Debug.Assert(this.pSprite != null);

        }

        ~AnimateCrab()
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

            // add Image to the list
            SLink.AddToFront(ref this.poFirstImage, pImageHolder);

            //then set the current image to the image just added
            //its always the first image
            this.pCurrentImage = pImageHolder;
        }

        //when execute() is called it changes images
        public override void Execute(float deltaTime)
        {
            //downcasting and set to next image
            ImageHolder pImageHolder = (ImageHolder)this.pCurrentImage.pSNext;

            // if we are at the end of the list
            //go to the first image and set it
            if (pImageHolder == null)
                pImageHolder = (ImageHolder)this.poFirstImage;

            //update current image
            this.pCurrentImage = pImageHolder;

            //change the image
            this.pSprite.SwapImage(pImageHolder.pImage);

            //"this" is the AnimSprite class that derives from the command class
            //so its the pointer to "this" command
            TimerMan.Add(TimeEvent.Name.AnimateCrab, this, deltaTime);
        }
    }
}
