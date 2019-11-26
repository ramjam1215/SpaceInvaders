using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SBNode_Link : DLink
    {

    }
    public class SBNode : SBNode_Link
    {
        private SpriteBase pSpriteBase;
        private SBNodeMan pBackSBNodeMan;

        public SBNode()
            : base()
        {
            this.pSpriteBase = null;
            this.pBackSBNodeMan = null;
        }

        ~SBNode()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~SBNode():{0} ", this.GetHashCode());
#endif
            this.pSpriteBase = null;
            this.pBackSBNodeMan = null;
        }

        public void Set(SpriteBase pNode, SBNodeMan pInSBNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            //set back pointers
            Debug.Assert(pSpriteBase != null);
            this.pSpriteBase.SetSBNode(this);

            Debug.Assert(pInSBNodeMan != null);
            this.pBackSBNodeMan = pInSBNodeMan;
        }

        //public void Set(GameSprite.Name name)
        //{
        //    //find the name in the given manager
        //    //set the pointer to that sprite
        //    this.pSpriteBase = GameSpriteMan.Find(name);
        //    Debug.Assert(this.pSpriteBase != null);
        //}

        //public void Set(BoxSprite.Name name)
        //{
        //    //find the name in the given manager
        //    //set the pointer to that sprite
        //    this.pSpriteBase = BoxSpriteMan.Find(name);
        //    Debug.Assert(this.pSpriteBase != null);
        //}

        //public void Set(ProxySprite pNode)
        //{
        //    Debug.Assert(pNode != null);
        //    this.pSpriteBase = pNode;
        //}

        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }

        public SBNodeMan GetSBNodeMan()
        {
            Debug.Assert(this.pBackSBNodeMan != null);
            return this.pBackSBNodeMan;
        }

        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSBNodeMan != null);
            return this.pBackSBNodeMan.GetSpriteBatch();
        }

        public void Wash()
        {
            this.pSpriteBase = null;
            this.pBackSBNodeMan = null;
        }

        public void DumpSB()
        {
            Debug.WriteLine("   SBNode: {0}", this.GetHashCode());

            
        }

    }
}
