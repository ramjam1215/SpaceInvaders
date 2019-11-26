using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class DelayedObjectMan
    {
        private ColObserver head;
        private static DelayedObjectMan instance = null;

        //Constructor is towards the bottom of the class body
        static public void Attach(ColObserver observer)
        {
            Debug.Assert(observer != null);

            DelayedObjectMan pDelayMan = DelayedObjectMan.PrivGetInstance();

            //adding to the front
            //first time
            if(pDelayMan.head == null)
            {
                pDelayMan.head = observer;
                observer.pNext = null;
                observer.pPrev = null;
            }

            //every other time
            else
            {
                observer.pNext = pDelayMan.head;
                observer.pPrev = null;
                pDelayMan.head.pPrev = observer;
                pDelayMan.head = observer;
            }
            
        }

        //just like how we use DLink's RemoveNode method
        private void PrivDetach(ColObserver node, ref ColObserver head)
        {
            Debug.Assert(node != null);

            if(node.pPrev != null)
            {
                node.pPrev.pNext = node.pNext;
            }

            else
            {
                head = (ColObserver)node.pNext;
            }

            if(node.pNext != null)
            {
                node.pNext.pPrev = node.pPrev;
            }
        }

        static public void Process()
        {
            DelayedObjectMan pDelayMan = DelayedObjectMan.PrivGetInstance();

            ColObserver pNode = pDelayMan.head;

            while(pNode != null)
            {
                pNode.Execute();

                pNode = (ColObserver)pNode.pNext;
            }

            pNode = pDelayMan.head;
            ColObserver pTemp = null;

            while(pNode != null)
            {
                pTemp = pNode;

                pNode = (ColObserver)pNode.pNext;

                //this might have been my issue with my timermanager references
                pDelayMan.PrivDetach(pTemp, ref pDelayMan.head);
            }
        }

        private DelayedObjectMan()
        {
            this.head = null;
        }

        private static DelayedObjectMan PrivGetInstance()
        {
            if(instance == null)
            {
                instance = new DelayedObjectMan();
            }

            Debug.Assert(instance != null);

            return instance;
        }
    }
}
