using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class DLink
    {
        public DLink pNext;
        public DLink pPrev;

        public DLink()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.pNext = null;
            this.pPrev = null;
        }

        public static DLink PullFromFront(ref DLink pHead)
        {
            Debug.Assert(pHead != null);

            DLink pDLink = pHead;

            pHead = pHead.pNext;

            // if we have more links
            //set the new start's previous to null
            if (pHead != null)
            {
                pHead.pPrev = null;
            }

            //we dont want any links coming with
            //found out the hard way
            pDLink.Clear();

            return pDLink;
        }

        public static void AddToFront(ref DLink pHead, DLink pDLink)
        {

            Debug.Assert(pDLink != null);


            if (pHead == null)
            {
                pHead = pDLink;
                pDLink.pNext = null;
                pDLink.pPrev = null;

            }

            else
            {
                //put link in the front with next
                pDLink.pPrev = null;
                pDLink.pNext = pHead;

                //set the start's previous and update the start of LL
                pHead.pPrev = pDLink;
                pHead = pDLink;
            }

        }

        public static void AddToEnd(ref DLink pHead, ref DLink pTail, DLink pNode)
        {
            Debug.Assert(pNode != null);

            //add to end, but will be at front initially
            if (pTail == pHead && pHead == null)
            {
                pHead = pNode;
                pTail = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }

            // everyother scenario other than the first node
            else
            {
                Debug.Assert(pTail != null);
                pTail.pNext = pNode;
                pNode.pPrev = pTail;
                pNode.pNext = null;

                //update the tail
                pTail = pNode;
            }

            Debug.Assert(pHead != null);
            Debug.Assert(pTail != null);
        }

        

        public static DLink RemoveNode(ref DLink pHead, ref DLink pTail, DLink pNode)
        {
            // protection
            Debug.Assert(pNode != null);

            // Might have bug
            //tried to diagram and change, but added more issues
            //check again later

            // 4 different conditions... 
            if (pNode.pPrev != null)
            {	// middle or last node
                pNode.pPrev.pNext = pNode.pNext;

                if (pNode == pTail)
                {
                    pTail = pNode.pPrev;
                }
            }

            else
            {  // first
                pHead = pNode.pNext;

                if (pNode == pTail)
                {
                    // Only one node
                    pTail = pNode.pNext;
                }

                else
                {
                    // Only first not the last
                    // do nothing more
                }
            }

            if (pNode.pNext != null)
            {	// middle node
                pNode.pNext.pPrev = pNode.pPrev;
            }

            pNode.Clear();

            return pNode;
        }

        public static void RemoveNode(ref DLink pHead, DLink pDLink)
        {
            // if its not there?
            Debug.Assert(pDLink != null);

            //this is crazy clever to me, to make it O(1)
            // i thought we had to walk the list to get it,
            // if we have the list the node is in we just need to adjust the referneces around the node we want
            if (pDLink.pPrev != null)
            {
                //set node before it, to the node after it
                pDLink.pPrev.pNext = pDLink.pNext;
            }

            else
            {
                // its at the front
                pHead = pDLink.pNext;
            }

            if (pDLink.pNext != null)
            {
                // set node after it, to the node before it
                pDLink.pNext.pPrev = pDLink.pPrev;
            }

        }



    }
}
