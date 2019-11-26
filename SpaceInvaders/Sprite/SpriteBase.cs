using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBase : DLink
    {
        //very clever back pointer to the object, 
        //so we can use in its pool or the object its encapsulated by
        private SBNode pBackSBNode;
        public SpriteBase()
            : base()
        {
            this.pBackSBNode = null;
        }

        ~SpriteBase()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("   ~SpriteBase():{0} ", this.GetHashCode());
#endif
        }

        public SBNode GetSBNode()
        {
            Debug.Assert(this.pBackSBNode != null);
            return this.pBackSBNode;
        }

        public void SetSBNode(SBNode pSpriteBaseNode)
        {
            Debug.Assert(pSpriteBaseNode != null);
            this.pBackSBNode = pSpriteBaseNode;
        }
        
        abstract public void Update();
        abstract public void Render();

    }
}
