using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ReverseIterator : Iterator
    {
        private Component pRoot;
        private Component pCurrent;
        private Component pPrev;
        public ReverseIterator(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.holder == Component.Container.COMPOSITE);

            ForwardIterator pForward = new ForwardIterator(pStart);

            this.pRoot = pStart;
            this.pCurrent = this.pRoot;
            this.pPrev = null;

            Component pPrevNode = this.pRoot;

            Component pNode = pForward.First();

            while (!pForward.IsDone())
            {
                pPrevNode = pNode;

                pNode = pForward.Next();

                if(pNode != null)
                {
                    pNode.pReverse = pPrevNode;
                }
            }

            pRoot.pReverse = pPrevNode;
        }
        public override Component First()
        {
            Debug.Assert(this.pRoot != null);

            this.pCurrent = this.pRoot.pReverse;

            return this.pCurrent;
        }

        public override Component Next()
        {
            Debug.Assert(this.pCurrent != null);

            this.pPrev = this.pCurrent;
            this.pCurrent = this.pCurrent.pReverse;
            return this.pCurrent;
        }

        public override bool IsDone()
        {
            return (this.pPrev == this.pRoot);
        }



    }
}
