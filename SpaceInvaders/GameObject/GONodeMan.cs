using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GONodeMan : Manager
    {
        private GONode poNodeCompare;
        private static GONodeMan pInstance;
        private NullGO poNullGO;

        private GONodeMan(int reserverNum = 3, int growth = 1)
            : base()
        {
            this.BaseIntialize(reserverNum, growth);

            this.poNodeCompare = new GONode();
            this.poNullGO = new NullGO();

            this.poNodeCompare.poGameObject = this.poNullGO;
        }

        public static void Destroy()
        {
            GONodeMan pGOMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pGOMan != null);

            pGOMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("GONodeMan.Destroy()");
#endif
            pGOMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pGOMan.poNullGO, pGOMan.poNullGO.GetHashCode());
            Debug.WriteLine("     {0} ({1})", pGOMan.poNodeCompare, pGOMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", GONodeMan.pInstance, GONodeMan.pInstance.GetHashCode());
#endif
            pGOMan.poNullGO = null;
            pGOMan.poNodeCompare = null;
            GONodeMan.pInstance = null;
        }

            public static void Create(int reserveNum = 3, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            if (pInstance == null)
                pInstance = new GONodeMan(reserveNum, growth);
        }

        public static GONode Attach(GameObject pGObject)
        {
            GONodeMan pGOMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pGOMan != null);

            GONode pNode = (GONode)pGOMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGObject);
            return pNode;
        }

        public static GameObject Find(GameObject.Name name)
        {
            GONodeMan pGOMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pGOMan != null);

            pGOMan.poNodeCompare.poGameObject.SetName(name);

            GONode pNode = (GONode)pGOMan.BaseFind(pGOMan.poNodeCompare);
            Debug.Assert(pNode != null);

            return pNode.poGameObject;
        }

        public static void Remove(GONode pNode)
        {
            GONodeMan pGOMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pGOMan != null);

            Debug.Assert(pNode != null);
            pGOMan.BaseRemove(pNode);
        }

        //to destroy game objects that are hit 
        public static void Dettach(GameObject pGameObject)
        {
            GONodeMan pGOMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pGOMan != null);
            
            //Remeber: a doublly linked list of trees

            //whatever game object we get, we have to travel up its tree
            // to its its tree's root/ upper most parent
            Debug.Assert(pGameObject != null);
            GameObject pTemp = pGameObject;

            GameObject pRoot = null;

            while (pTemp != null)
            {
                pRoot = pTemp;
                pTemp = (GameObject)Iterator.GetParent(pTemp);
                //keep traveling up the tree

                //exit out at the top of the tree
            }

            //Found the tree our game object is in
            // now go traverse the DLink list to that tree
            GONode pTree = (GONode)pGOMan.BaseGetActive();

            while (pTree != null)
            {
                //check if the game objects match
                if (pTree.poGameObject == pRoot)
                {
                    break;
                }

                pTree = (GONode)pTree.pNext;
            }

            //Now we are in the tree with the Game Object 
            //we need to remove
            Debug.Assert(pTree != null);
            Debug.Assert(pTree.poGameObject != null);

            GameObject pParent = (GameObject)Iterator.GetParent(pGameObject);
            Debug.Assert(pParent != null);


            GameObject pChild = (GameObject)Iterator.GetChild(pGameObject);
            Debug.Assert(pChild == null);

            //finally
            //Remove Gamobject from its parent composite
            pParent.Remove(pGameObject);

            

        }


        public static void Update()
        {
            // go through active list and update everything in it
            // while loop

            GONodeMan pMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            //Debug.WriteLine("---------------");

            GONode pGONode = (GONode)pMan.BaseGetActive();

            
            while (pGONode != null)
            {
                //Debug.WriteLine("update: GameObjectTree {0} ({1})", pGONode.poGameObject, pGONode.poGameObject.GetHashCode());
                //Debug.WriteLine("   +++++");
                if(pGONode.poGameObject.GetPlayerOption() == true)
                {
                    ReverseIterator pRev = new ReverseIterator(pGONode.poGameObject);

                    //need to update the children 1st then work our way back up the tree
                    // So depth first(ReverseIterator)
                    // maintaines integrity of the bounding collision rectangles
                    Component pNode = pRev.First();
                    while (!pRev.IsDone())
                    {
                        GameObject pGameObj = (GameObject)pNode;

                        //Debug.WriteLine("update: {0} ({1})", pGameObj, pGameObj.GetHashCode());
                        pGameObj.Update();

                        pNode = pRev.Next();
                    }
                    //Debug.WriteLine("   ------");
                    pGONode = (GONode)pGONode.pNext;

                }
               
            }
        }

        public static void StatDump()
        {
            GONodeMan pMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseStatDump();
        }

        public static void DumpGONodes()
        {
            GONodeMan pMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDumpNodes();
        }

        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            GONode pSNode1 = (GONode)pDLink1;
            GONode pSNode2 = (GONode)pDLink2;

            Boolean status = false;

            if (pSNode1.poGameObject.GetName() == pSNode2.poGameObject.GetName())
                status = true;

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new GONode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            GONode pNode = (GONode)pDLink;
            pNode.DumpGONode();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            GONode pNode = (GONode)pDLink;
            pNode.Clear();
        }

        private void PrivStatDump()
        {
            GONodeMan pGOMan = GONodeMan.PrivGetInstance();
            Debug.Assert(pGOMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("Game Object Manager Stats---------------------");
            pGOMan.BaseStatDump();
        }

        private static GONodeMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }


    }
}

