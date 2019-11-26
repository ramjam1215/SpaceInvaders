using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputSubject
    {
        private InputObserver pHead;

        public void Attach(InputObserver observer)
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
                //attaching to the front
                observer.pNext = pHead;
                observer.pPrev = null;
                pHead.pPrev = observer;
                pHead = observer;
            }
        }


        public void Notify()
        {
            InputObserver pONode = this.pHead;

            while(pONode != null)
            {
                pONode.Notify();

                pONode = (InputObserver)pONode.pNext;
            }
        }

        public void Detach()
        {

        }

    }
}
