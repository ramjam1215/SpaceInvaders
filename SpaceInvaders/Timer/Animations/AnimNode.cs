using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AnimNode : DLink
    {
        public Animation poAnimation;

        public AnimNode() 
            : base()
        {
            this.Clear();
        }

        public void Set(Animation pAnimation)
        {
            Debug.Assert(pAnimation != null);
            this.poAnimation = pAnimation;
        }

        public new void Clear()
        {
            this.poAnimation = null;
            base.Clear();
        }


        public void DumpAnimation()
        {
            Debug.WriteLine("Animation Node: {0}", this.GetHashCode());

            if(this.poAnimation != null)
            {
                Debug.WriteLine("Animation: {0} ({1})", this.poAnimation.GetName(), this.poAnimation.GetHashCode());
            }

            else
            {
                Debug.WriteLine("Animation: null");
            }
        }
    }
}
