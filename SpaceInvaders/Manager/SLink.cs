using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class SLink
    {
        public SLink pSNext;

        public SLink()
        {
            pSNext = null;
        }

        public static void AddToFront(ref SLink pStart, SLink pSLink)
        {
            Debug.Assert(pSLink != null);

            // O(1) by adding to the front

            if (pStart == null)
                pStart = pSLink;

            else
            {
                pSLink.pSNext = pStart;
                pStart = pSLink;
            }
        }
    }
}
