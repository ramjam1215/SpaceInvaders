using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AnimMan : Manager
    {

        private static AnimMan pInstance = null;
        private AnimNode poAnimNodeCompare;
        private NullAnim poNullAnim;

        public AnimMan(int reserveNum = 3, int growth = 1)
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.poAnimNodeCompare = new AnimNode();
            this.poNullAnim = new NullAnim();

            this.poAnimNodeCompare.poAnimation = this.poNullAnim;
        }

        public static void Destroy()
        {
            AnimMan pANodeMan = AnimMan.PrivGetInstance();
            Debug.Assert(pANodeMan != null);


#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("AnimMan.Destroy()");
#endif
            pANodeMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("{0} ({1})", pANodeMan.poAnimNodeCompare, pANodeMan.poAnimNodeCompare.GetHashCode());
            Debug.WriteLine("{0} ({1})", AnimMan.pInstance, AnimMan.pInstance.GetHashCode());
#endif

            pANodeMan.poAnimNodeCompare = null;
            pANodeMan.poNullAnim = null;
            AnimMan.pInstance = null;
            
        }

        public static void Create(int reserveNum = 3, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            if(pInstance == null)
                pInstance = new AnimMan(reserveNum, growth);
            
        }

        public static AnimNode Attach(Animation pAnimation)
        {
            AnimMan pANodeMan = AnimMan.PrivGetInstance();
            Debug.Assert(pANodeMan != null);

            AnimNode pNode = (AnimNode)pANodeMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pAnimation);
            return pNode;
        }

        
        public static  Animation Find(Animation.Name name)
        {
            AnimMan pANodeMan = AnimMan.PrivGetInstance();
            Debug.Assert(pANodeMan != null);

            pANodeMan.poAnimNodeCompare.poAnimation.SetName(name);

            AnimNode pANode = (AnimNode)pANodeMan.BaseFind(pANodeMan.poAnimNodeCompare);
            Debug.Assert(pANode != null);

            return pANode.poAnimation;
        }

        public static void DumpAnimNodes()
        {
            AnimMan pANodeMan = AnimMan.PrivGetInstance();
            Debug.Assert(pANodeMan != null);

            pANodeMan.BaseDumpNodes();
        }


        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            AnimNode pSNode1 = (AnimNode)pDLink1;
            AnimNode pSNode2 = (AnimNode)pDLink2;

            Boolean status = false;

            if (pSNode1.poAnimation.GetName() == pSNode2.poAnimation.GetName())
                status = true;

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new AnimNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            AnimNode pNode = (AnimNode)pDLink;
            pNode.DumpAnimation();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            AnimNode pNode = (AnimNode)pDLink;
            pNode.Clear();
        }

        private static AnimMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
