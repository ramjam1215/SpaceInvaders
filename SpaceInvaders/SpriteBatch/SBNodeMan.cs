using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SBNodeMan_MLink : Manager
    {
        public SBNode_Link poActive;
        public SBNode_Link poReserve;
    }
    public class SBNodeMan : SBNodeMan_MLink
    {
        private SBNode poNodeCompare;
        private SpriteBatch.Name name;
        private SpriteBatch pBackSpriteBatch;

        public SBNodeMan(int reserveNum = 4, int growth = 1)
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.poNodeCompare = new SBNode();
            this.pBackSpriteBatch = null;
        }

        ~SBNodeMan()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~SBNodeMan():{0} ", this.GetHashCode());
#endif
            this.name = SpriteBatch.Name.Unitialized;
            this.poNodeCompare = null;
            this.pBackSpriteBatch = null;
        }

        public void Destroy()
        {
            this.BaseDestroy();

            this.name = SpriteBatch.Name.Unitialized;
            this.poNodeCompare = null;
            this.pBackSpriteBatch = null;
        }

        public void Set(SpriteBatch.Name name, int reserveNum, int reserveGrowth)
        {
            this.name = name;
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrowth > 0);

            this.BaseSetReserve(reserveNum, reserveGrowth);
        }
        
        public SBNode Attach(SpriteBase pNode)
        {
            SBNode pSBNode = (SBNode)this.BaseAdd();
            Debug.Assert(pSBNode != null);

            //intialize and set back pointer
            pSBNode.Set(pNode, this);

            return pSBNode;
        }

        //just like the set methods in SBNode; attach for each type
        //public SBNode Attach(GameSprite.Name name)
        //{
        //    // take node from reserve, wash the links
        //    //adds it to the active list and set the name
        //    SBNode pNode = (SBNode)this.baseAdd();
        //    Debug.Assert(pNode != null);

        //    // sets the SBnode pointer
        //    pNode.Set(name);

        //    return pNode;
        //}

        //public SBNode Attach(BoxSprite.Name name)
        //{
        //    // take node from reserve, wash the links
        //    //adds it to the active list and set the name
        //    SBNode pNode = (SBNode)this.baseAdd();
        //    Debug.Assert(pNode != null);

        //    //sets the SBnode pointer
        //    pNode.Set(name);

        //    return pNode;
        //}

        //public SBNode Attach(ProxySprite pNode)
        //{
        //    SBNode pSBNode = (SBNode)this.baseAdd();
        //    Debug.Assert(pNode != null);

        //    pSBNode.Set(pNode);
        //    return pSBNode;
        //}

        public void Draw()
        {
            // starting node
            SBNode pNode = (SBNode)this.BaseGetActive();

            SBNode pNext = null;

            while (pNode != null)
            {
                pNext = (SBNode)pNode.pNext;

                pNode.GetSpriteBase().Render();

                //ok because all nodes in this are SpriteBase objects
                pNode = pNext;
            }
        }

        public void Remove(SBNode pNode)
        {
            Debug.Assert(pNode != null);
            this.BaseRemove(pNode);
        }

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }

        public void SetSpriteBatch(SpriteBatch pInSpriteBatch)
        {
            this.pBackSpriteBatch = pInSpriteBatch;
        }
        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            throw new NotImplementedException();
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pSBNode = new SBNode();
            Debug.Assert(pSBNode != null);

            return pSBNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            SBNode pSBNode = (SBNode)pDLink;
            pSBNode.DumpSB();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            SBNode pSBNode = (SBNode)pDLink;
            pSBNode.Wash();

        }

        public void DumpSpriteBases()
        {
            SBNode pSBNode = (SBNode)this.BaseGetActive();

            int i = 0;
           
            while(pSBNode != null)
            {
                
                Debug.WriteLine("<->#{0}", i);
                pSBNode.DumpSB();
                
                pSBNode = (SBNode)pSBNode.pNext;
                i++;
            }
        }
    }
}
