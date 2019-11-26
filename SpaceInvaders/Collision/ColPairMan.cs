using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColPairMan : Manager
    {
        private static ColPairMan pInstance = null;
        private ColPair poNodeCompare;

        private ColPair pActiveColPair;
        public ColPairMan(int reserveNum = 3, int growth = 1) 
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.pActiveColPair = null;

            this.poNodeCompare = new ColPair();
           
        }

        public static void Create(int reserveNum = 3, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            //should be true, so continue
            //need to initialize singleton once and only once here
            Debug.Assert(pInstance == null);

            if(pInstance == null)
            {
                pInstance = new ColPairMan(reserveNum, growth);
            }
        }

        public static void Destroy()
        {
            ColPairMan pMan = ColPairMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("ColPairMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", ColPairMan.pInstance, ColPairMan.pInstance.GetHashCode());
#endif
            pMan.pActiveColPair = null;
            pMan.poNodeCompare = null;
            ColPairMan.pInstance = null;
        }

        public static ColPair Add(ColPair.Name pairName, GameObject treeRootA, GameObject treeRootB)
        {
            ColPairMan pMan = ColPairMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            ColPair pColPair = (ColPair)pMan.BaseAdd();
            Debug.Assert(pColPair != null);

            pColPair.Set(pairName, treeRootA, treeRootB);

            return pColPair;
        }

        public static void Process()
        {
            ColPairMan pMan = ColPairMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            ColPair pColPair = (ColPair)pMan.BaseGetActive();

            while(pColPair != null)
            {
                //Set the current to active
                pMan.pActiveColPair = pColPair;

                //do the collision pair
                pColPair.Process();

                //go to next
                pColPair = (ColPair)pColPair.pNext;
            }
        }

        public static ColPair Find(ColPair.Name pairName)
        {
            ColPairMan pMan = ColPairMan.PrivGetInstance();
            Debug.Assert(pMan != null);


            pMan.poNodeCompare.SetName(pairName);

            ColPair pCPNode = (ColPair)pMan.BaseFind(pMan.poNodeCompare);
            return pCPNode;
        }

        public static void Remove(ColPair pCPNode)
        {
            ColPairMan pMan = ColPairMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pCPNode != null);
            pMan.BaseRemove(pCPNode);
        }

        static public ColPair GetActiveColPair()
        {
            ColPairMan pMan = ColPairMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pActiveColPair;
        }

        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            ColPair pCPNode1 = (ColPair)pDLink1;
            ColPair pCPNode2 = (ColPair)pDLink2;

            bool status = false;

            if(pCPNode1.GetName() == pCPNode2.GetName())
            {
                status = true;
            }
                
            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new ColPair();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            ColPair pCPNode = (ColPair)pDLink;
            pCPNode.DumpColPair();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            ColPair pCPNode = (ColPair)pDLink;
            pCPNode.Wash();
        }

        private void PrivStatDump()
        {
            ColPairMan pMan = ColPairMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("Collision Pair Manager Stats-------------------------");
            pMan.BaseStatDump();
        }

        private static ColPairMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
