using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Manager
    {
        private int InitialNumReserve;
        private int growth;

        private int NumActive;
        private int NumReserve;
        private int TotalNodes;

        private DLink poActive;
        private DLink poReserve;

        protected Manager()
        {
            this.InitialNumReserve = 0;
            this.growth = 0;

            this.NumActive = 0;
            this.NumReserve = 0;

            this.poActive = null;
            this.poReserve = null;

        }

        ~Manager()
        {
            //compiler encounters directives #if and #endif
            // then compiles the code between them
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Manager(): {0}", this.GetHashCode() );
#endif
            //flag data for garbage collection
            this.poActive = null;
            this.poReserve = null;
        }

        protected void BaseIntialize(int InitialNumReserve = 5, int growth = 2)
        {
            Debug.Assert(InitialNumReserve >= 0);
            Debug.Assert(growth >= 0);

            this.growth = growth;
            this.InitialNumReserve = InitialNumReserve;

            this.PrivReFill(this.InitialNumReserve);
        }

        protected void BaseSetReserve(int reserveNum, int reserveGrow)
        {
            this.growth = reserveGrow;

            if (reserveNum > this.NumReserve)
                this.PrivReFill(reserveNum - this.NumReserve);
        }

        //access to the active list
        public DLink BaseGetActive()
        {
            return this.poActive;
        }

        public DLink SetActive(ref DLink pDLink)
        {
            this.poActive = pDLink;

            return this.poActive;
        }

        protected DLink GrabNode()
        {
            if (this.NumReserve == 0)
            {
                this.PrivReFill(this.growth);
            }

            DLink pDLink = DLink.PullFromFront(ref this.poReserve);
            this.NumReserve--;

            this.DerivedWash(pDLink);

            this.NumActive++;
            return pDLink;
        }

        protected DLink BaseAdd()
        {
            // refill Reserve List if empty
            if (this.NumReserve == 0)
            {
                this.PrivReFill(this.growth);
            }

            //pull from front of reserve list
            DLink pDlink = DLink.PullFromFront(ref this.poReserve);
            this.NumReserve--;

            //clean the notecard
            this.DerivedWash(pDlink);

            //then add to front of active list
            DLink.AddToFront(ref this.poActive, pDlink);


            this.NumActive++;

            return pDlink;
        }

        protected DLink BaseFind(DLink pLinkToFind)
        {
            DLink pDlink = this.poActive;

            while (pDlink != null)
            {
                if (this.DerivedCompare(pDlink, pLinkToFind))
                {
                    break;
                }
                pDlink = pDlink.pNext;
            }
            return pDlink;
        }

        protected void BaseRemove(DLink pDlink)
        {

            //depending on where the node is in the list we will have to move stuff around
            DLink.RemoveNode(ref this.poActive, pDlink);
            this.NumActive--;

            this.DerivedWash(pDlink);

            // then send that node or DLink back into the mNumReserve list
            DLink.AddToFront(ref this.poReserve, pDlink);
            this.NumReserve++;
        }

        protected void BaseDumpNodes()
        {

            // Active list 1st
            Debug.WriteLine("");
            Debug.WriteLine("Active List -------------------------\n");

            DLink pDlink = this.poActive;
            int i;

            if (pDlink == null)
            {
                Debug.WriteLine(" Empty");
            }

            else
            {
                i = 0;
                while (pDlink != null)
                {
                    Debug.WriteLine("");
                    Debug.WriteLine("{0})----------", i);
                    this.DerivedDumpNode(pDlink);

                    pDlink = pDlink.pNext;
                    i++;
                }
            }

            //Reserve list 2nd
            Debug.WriteLine("");
            Debug.WriteLine("Reserve List--------------------------\n");
            pDlink = this.poReserve;

            if (pDlink == null)
            {
                Debug.WriteLine(" Empty ");
            }

            else
            {
                i = 0;
                while (pDlink != null)
                {
                    Debug.WriteLine("{0})----------", i);
                    this.DerivedDumpNode(pDlink);
                    Debug.WriteLine("");

                    pDlink = pDlink.pNext;
                    i++;
                }
            }

        }

        protected void BaseStatDump()
        {

            Debug.WriteLine("Active Number: {0}", this.NumActive);
            Debug.WriteLine("Reserve Number: {0}", this.NumReserve);
            Debug.WriteLine("Total Number: {0}", this.TotalNodes);
            Debug.WriteLine("Initial Reserve Number:{0}", this.InitialNumReserve);
            Debug.WriteLine("Growth Rate:{0}", this.growth);
            Debug.WriteLine("------------------------------------------------------");
        }

        protected void BaseDestroy()
        {
            DLink pNode;
            DLink pTemp;

            pNode = this.poActive;

            while (pNode != null)
            {
                pTemp = pNode;
                pNode = pNode.pNext;

                Debug.Assert(pTemp != null);
                this.DerivedDestroyNode(pTemp);
                DLink.RemoveNode(ref this.poActive, pTemp);
                pTemp = null;

                this.NumActive--;
                this.TotalNodes--;
            }

            pNode = this.poReserve;

            while (pNode != null)
            {
                pTemp = pNode;
                pNode = pNode.pNext;

                Debug.Assert(pTemp != null);
                this.DerivedDestroyNode(pTemp);
                DLink.RemoveNode(ref this.poReserve, pTemp);
                pTemp = null;

                this.NumReserve--;
                this.TotalNodes--;
            }

        }

        abstract protected DLink DerivedCreateNode();
        abstract protected Boolean DerivedCompare(DLink pDLink1, DLink pDLink2);
        abstract protected void DerivedDumpNode(DLink pDLink);
        abstract protected void DerivedWash(DLink pDLink);

        virtual protected void DerivedDestroyNode(DLink pDLink)
        {
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine(" {0} ({1})",pDLink, pDLink.GetHashCode());
#endif
            pDLink = null;
        }

        private void PrivReFill(int count)
        {
            Debug.Assert(count > 0);

            this.NumReserve += count;
            this.TotalNodes += count;

            for (int i = 0; i < count; i++)
            {
                DLink pDlink = this.DerivedCreateNode();
                DLink.AddToFront(ref this.poReserve, pDlink);
            }

        }



    }
}
