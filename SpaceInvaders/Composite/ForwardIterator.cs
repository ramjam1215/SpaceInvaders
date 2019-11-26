using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ForwardIterator : Iterator
    {
        private Component pCurrent;
        private Component pRoot;
        public ForwardIterator(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.holder == Component.Container.COMPOSITE);

            this.pCurrent = pStart;
            this.pRoot = pStart;
        }

        private Component PrivNextStep(Component pNode, Component pParent, Component pChild,Component pSibling)
        {
            //this keeps us from getting an infinite loop
            //i wonder how i know that
            pNode = null;

            if (pChild != null)
            {
                pNode = pChild;
            }

            else
            {
                if(pSibling != null)
                {
                    pNode = pSibling;
                }
                else
                {

                    while(pParent != null)
                    {
                        pNode = GetSibling(pParent);

                        if(pNode != null)
                        {
                            //found one
                            break;
                        }
                        else
                        {
                            //Go further up if you didn't find one
                            pParent = GetParent(pParent);
                        }
                    }
                }
            }   

            return pNode;
        }

        public override Component First()
        {
            Debug.Assert(this.pRoot != null);
            Component pNode = this.pRoot;

            Debug.Assert(pNode != null);
            this.pCurrent = pNode;

            return this.pCurrent;
        }

        public override Component Next()
        {
            Debug.Assert(this.pRoot != null);

            Component pNode = this.pCurrent;

            Component pChild = GetChild(pNode);
            Component pSibling = GetSibling(pNode);
            Component pParent = GetParent(pNode);

            pNode = PrivNextStep(pNode, pParent, pChild, pSibling);
            this.pCurrent = pNode;

            return this.pCurrent;
        }

        public override bool IsDone()
        {
            return (this.pCurrent == null);
        }
    }
}
