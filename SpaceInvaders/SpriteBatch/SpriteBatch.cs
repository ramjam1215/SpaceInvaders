using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBatch_Link : DLink
    {

    }

    public class SpriteBatch : SpriteBatch_Link
    {
        private SpriteBatch.Name name;
        private SBNodeMan poSBNodeMan;
        public bool bToggle;

        public enum Name
        {
            IntroScreen,
            InGameScreen,
            GameOver,

            Aliens,
            Boxes,
            Texts,
            Shields,

            Splats,

            Projectiles,

            Unitialized
        }

        public SpriteBatch()
            : base()
        {
            this.name = SpriteBatch.Name.Unitialized;

            // this is why SBNode manager cant be a singleton
            this.poSBNodeMan = new SBNodeMan();
            Debug.Assert(this.poSBNodeMan != null);
            this.bToggle = true;
        }

        ~SpriteBatch()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~SpriteBatch():{0} ", this.GetHashCode());
#endif
            this.name = Name.Unitialized;
            this.poSBNodeMan = null;
        }

        public void Destroy()
        {
            Debug.Assert(this.poSBNodeMan != null);
            this.poSBNodeMan.Destroy();
        }

        public void Set(SpriteBatch.Name name, int reserveNum = 3, int reserveGrowth = 1)
        {
            Debug.Assert(this.poSBNodeMan != null);
            this.name = name;
            this.poSBNodeMan.Set(name, reserveNum, reserveGrowth);
        }

        public void SetName(SpriteBatch.Name name)
        {
            this.name = name;
        }

        public SpriteBatch.Name GetName()
        {
            return this.name;
        }

        public SBNodeMan GetSBNodeMan()
        {
            return this.poSBNodeMan;
        }

        public void Attach(SpriteBase pNode)
        {
            Debug.Assert(pNode != null);

            SBNode pSBNode = (SBNode)this.poSBNodeMan.Attach(pNode);
            Debug.Assert(pSBNode != null);

            pSBNode.Set(pNode, this.poSBNodeMan);

            this.poSBNodeMan.SetSpriteBatch(this);
        }

        public void Dettach(SpriteBase pNodeToRemove)
        {
            //use the back pointers
            Debug.Assert(pNodeToRemove != null);
            SBNode pSBNode = pNodeToRemove.GetSBNode();

            SBNodeMan pSBNodeMan = this.GetSBNodeMan();

            pSBNodeMan.Remove(pSBNode);

        }

        //public SBNode Attach(GameSprite.Name name)
        //{
        //    // doing the attach here 
        //    //creates the list of sprite base nodes
        //    SBNode pNode = this.poSBNodeMan.Attach(name);

        //    return pNode;
        //}

        //public SBNode Attach(BoxSprite.Name name)
        //{
        //    SBNode pNode = this.poSBNodeMan.Attach(name);

        //    return pNode;
        //}

        //public SBNode Attach(ProxySprite pNode)
        //{
        //    Debug.Assert(this.poSBNodeMan != null);
        //    SBNode pSBNode = this.poSBNodeMan.Attach(pNode);

        //    return pSBNode;
        //}

        public void DumpSBatch()
        {
            poSBNodeMan.DumpSpriteBases();
        }
    }
}
