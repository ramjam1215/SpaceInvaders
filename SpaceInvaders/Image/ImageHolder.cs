using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageHolder : SLink
    {
        public Image pImage;
        public ImageHolder(Image Image)
            : base()
        {
            this.pImage = Image;
        }

        ~ImageHolder()
        {
            this.pImage = null;
        }
    }
}