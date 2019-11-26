using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TimerMan_MLink : Manager
    {
        public TimeEvent_Link poActive;
        public TimeEvent_Link poReserve;
    }
    class TimerMan : TimerMan_MLink
    {
        private static TimerMan pInstance = null;
        private TimeEvent poNodeCompare;
        protected float mCurrentTime;

        public TimerMan(int reserveNum = 3, int growth = 1)
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.poNodeCompare = new TimeEvent();
        }

        public static void Destroy()
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            pTMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("TimerMan.Destroy()");
#endif
            pTMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pTMan.poNodeCompare, pTMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", TimerMan.pInstance, TimerMan.pInstance.GetHashCode());
#endif
            pTMan.poNodeCompare = null;
            TimerMan.pInstance = null;
        }

        public static void Create(int reserveNum = 3, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            if (pInstance == null)
                pInstance = new TimerMan(reserveNum, growth);
        }

        public static float GetCurrentTime()
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();

            return pTMan.mCurrentTime;
        }

        private TimeEvent GetActiveList()
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            TimeEvent pEvent = (TimeEvent)pTMan.BaseGetActive();

            return pEvent;
        }

        public static void ClearActiveList()
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            TimeEvent pHead = pTMan.GetActiveList();
            TimeEvent pNode = pHead;

            while(pNode != null)
            {
                TimeEvent pNext = (TimeEvent)pNode.pNext;

                Remove(pNode);

                pNode = pNext;
            }


        }

        public static void Update(float TotalTime)
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            //wasn't sure how we started the timer until i saw its use in the game.cs
            pTMan.mCurrentTime = TotalTime;

            TimeEvent pEvent = (TimeEvent)pTMan.BaseGetActive();

            //set the next to walk the list
            TimeEvent pNextEvent = null;

            while (pTMan.mCurrentTime >= pEvent.trigger)
            {
                
                //trigger event
                if (pTMan.mCurrentTime >= pEvent.trigger)
                {
                    pEvent.Process();

                    //intially i instantiated this outside of the loop "as-is" and it caused issues
                    pNextEvent = (TimeEvent)pEvent.pNext;

                    pTMan.BaseRemove(pEvent);
                }



                pEvent = pNextEvent;
            }
        }

        //needs to insert as a priority queue
        public static TimeEvent Add(TimeEvent.Name name, Command pCommand, float deltaTime)
        {
            Debug.Assert(pCommand != null);

            //real-time systems  must be causal
            //cant have an event occur before current time
            Debug.Assert(deltaTime >= 0.0f);

            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            TimeEvent pTEnode = (TimeEvent)pTMan.GrabNode();
            Debug.Assert(pTEnode != null);

            //the TimeEvent class does the updated trigger time
            pTEnode.Set(name, pCommand, deltaTime);


            Insert(pTEnode);
            return pTEnode;
        }


        //i definitely think the insert() needs to be cleaned up
        // i had alot of reference issues
        private static void Insert(TimeEvent pTEnode)
        {
            Debug.Assert(pTEnode != null);

            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            TimeEvent pEvent = (TimeEvent)pTMan.BaseGetActive();
            TimeEvent pNextEvent = null;
            TimeEvent pPrevEvent = null;


            //very first node
            if (pEvent == null)
            {
                pTEnode.pPrev = null;
                pTEnode.pNext = null;
                DLink pTemp = (DLink)pTEnode;

                pEvent = (TimeEvent)pTMan.SetActive(ref pTemp);
            }

            //insert before the first node
            if (pTEnode.trigger < pEvent.trigger)
            {
                pTEnode.pNext = pEvent;
                pEvent.pPrev = pTEnode;
                pTEnode.pPrev = null;


                DLink pTemp = (DLink)pTEnode;
                pEvent = (TimeEvent)pTMan.SetActive(ref pTemp);
            }

            while (pTEnode.trigger >= pEvent.trigger && pTEnode.GetHashCode() != pEvent.GetHashCode())
            {
                pNextEvent = (TimeEvent)pEvent.pNext;

                //then something is there
                if (pNextEvent != null)
                {
                    //insert into middle of list
                    if (pTEnode.trigger <= pNextEvent.trigger)
                    {
                        pPrevEvent = (TimeEvent)pEvent;

                        pTEnode.pNext = pNextEvent;
                        pTEnode.pPrev = pPrevEvent;

                        pPrevEvent.pNext = pTEnode;

                        pNextEvent.pPrev = pTEnode;

                        break;
                    }

                }

                //insert at the end of the list
                else
                {
                    pTEnode.pPrev = pEvent;
                    pEvent.pNext = pTEnode;
                    pTEnode.pNext = null;

                    break;

                }
                pEvent = pNextEvent;
            }
        }

        public static TimeEvent Find(TimeEvent.Name name)
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            pTMan.poNodeCompare.name = name;

            TimeEvent pTEnode = (TimeEvent)pTMan.BaseFind(pTMan.poNodeCompare);
            return pTEnode;
        }

        public static void Remove(TimeEvent pTEnode)
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            Debug.Assert(pTEnode != null);
            pTMan.BaseRemove(pTEnode);
        }

        public static void DumpTimeEvents()
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            pTMan.BaseDumpNodes();
        }

        public void PrivStatDump()
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("Timer Manager Stats---------------------");
            pTMan.BaseStatDump();
        }

        public static void StatDump()
        {
            TimerMan pTMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            pTMan.BaseStatDump();
        }


        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            TimeEvent pTEnode1 = (TimeEvent)pDLink1;
            TimeEvent pTEnode2 = (TimeEvent)pDLink2;

            Boolean status = false;

            if (pTEnode1.name == pTEnode2.name)
                status = true;

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new TimeEvent();

            Debug.Assert(pNode != null);
            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            TimeEvent pTEnode = (TimeEvent)pDLink;
            pTEnode.DumpTimeEvent();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            TimeEvent pTEnode = (TimeEvent)pDLink;
            pTEnode.Wash();
        }

        private static TimerMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}

