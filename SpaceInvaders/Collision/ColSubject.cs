using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColSubject
    {
        private ColObserver pHead;
        public GameObject pObjA;
        public GameObject pObjB;

        public ColSubject()
        {
            this.pHead = null;
            this.pObjA = null;
            this.pObjB = null;
        }

        public void Attach(ColObserver observer)
        {
            Debug.Assert(observer != null);

            observer.pSubject = this;

            if(pHead == null)
            {
                pHead = observer;
                observer.pNext = null;
                observer.pPrev = null;
            }

            else
            {
                observer.pNext = pHead;
                pHead.pPrev = observer;
                pHead = observer;
            }
        }

        public void Notify()
        {
            ColObserver pNode = this.pHead;

            while(pNode != null)
            {
                pNode.Notify();

                pNode = (ColObserver)pNode.pNext;
            }
        }

    }
}
