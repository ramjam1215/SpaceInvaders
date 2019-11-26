using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GraveyardMan
    {
        private GameObject poHead;
        private static GraveyardMan pInstance = null;
        private int Total = 0;

        public static void Destroy()
        {
            GraveyardMan pGraveyard = GraveyardMan.PrivGetInstance();

            //TO DO need this big time!!!!
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("GraveyardMan.Destroy()");
#endif
            pGraveyard.PrivFlush();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("{0} ({1})", GraveyardMan.pInstance, GraveyardMan.pInstance.GetHashCode());
#endif
            GraveyardMan.pInstance = null;
            pGraveyard.poHead = null;
        }

        static public void Create()
        {
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new GraveyardMan();
            }

            Debug.Assert(pInstance != null);

            pInstance.poHead = null;

        }

        static public void Bury(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);

            GraveyardMan pGraveyard = GraveyardMan.PrivGetInstance();

            //everything is added to the front of the list
            //first time
            if(pGraveyard.poHead == null)
            {
                pGraveyard.poHead = pGameObject;
                pGameObject.pNext = null;
                pGameObject.pPrev = null;
            }

            //every other time
            else
            {
                pGameObject.pNext = pGraveyard.poHead;
                pGameObject.pPrev = null;
                pGraveyard.poHead.pPrev = pGameObject;
                pGraveyard.poHead = pGameObject;
            }
            pGraveyard.Total++;
        }
        
        //rebuilds the Alien Group Tree
        public static void RaiseDead()
        {
            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            GameObject pRoot = GONodeMan.Find(GameObject.Name.AlienGrid);

            GraveyardMan pGraveyard = GraveyardMan.PrivGetInstance();
            
            GameObject pSafety = pGraveyard.poHead;

            GameObject pNode = pSafety;

            GameObject pNextNode = null;

            //search for the columns
            while (pNode != null)
            {
                //save this to iterate, because we will possibly remove the next pointer 
                pNextNode = (GameObject)pNode.pNext;

                //we still have the parent reference on the objects
                if(pRoot == pNode.pParent)
                {
                    //dettach from list then add it to tree
                    pGraveyard.PrivDetach(pNode, ref pGraveyard.poHead);

                    pNode.Clear();

                    pRoot.Add(pNode);
                    pNode.ActivateGameSprite(pSB_Aliens);
                    pNode.ActivateCollisionSprite(pSB_Boxes);
                    
                }

                pNode = pNextNode;
            }
            
            //random check
            //check to see if AlienGroup has a column
            Debug.Assert(pRoot.GetFirstChild() != null);

            GameObject pColumn = (GameObject)pRoot.GetFirstChild();

            //iterate through the column siblings
            while(pColumn != null)
            {

                //iterate through the graveyard
                pNode = pGraveyard.poHead;
                while (pNode != null)
                {
                    //save this to iterate, because we will possibly remove the next pointer it
                    pNextNode = (GameObject)pNode.pNext;

                    //check the parent reference to the column
                    if (pColumn == pNode.pParent)
                    {
                        //dettach from list then add it to tree
                        pGraveyard.PrivDetach(pNode, ref pGraveyard.poHead);

                        AlienCategory pAlien = (AlienCategory)pNode;
                        pAlien.x = pAlien.OrigX();
                        pAlien.y = pAlien.OrigY() - 100.0f;
                        pAlien.bMarkForDeath = false;

                        pNode.Clear();

                        pColumn.Add(pNode);
                        pNode.ActivateGameSprite(pSB_Aliens);
                        pNode.ActivateCollisionSprite(pSB_Boxes);
                    }

                    pNode = pNextNode;
                }


                pColumn = (GameObject)pColumn.pNext;
            }
            
        }

        //make it rebuild the Shields now
        public static void RebuildShields()
        {
            SpriteBatch pSB_Shields = SpriteBatchMan.Find(SpriteBatch.Name.Shields);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            GameObject pShieldRoot = GONodeMan.Find(GameObject.Name.ShieldRoot);

            GraveyardMan pGraveyard = GraveyardMan.PrivGetInstance();

            GameObject pSafety = pGraveyard.poHead;

            GameObject pNode = pSafety;

            GameObject pNextNode = null;

            //-------------------------------------------------------------------------------------------------
            //add the Shields to the Shield Root
            //-------------------------------------------------------------------------------------------------

            //search for the shields
            while (pNode != null)
            {
                //save this to iterate, because we will possibly remove the next pointer it
                pNextNode = (GameObject)pNode.pNext;

                //we still have the parent reference on the objects
                if (pShieldRoot == pNode.pParent)
                {
                    //dettach from list then add it to tree
                    pGraveyard.PrivDetach(pNode, ref pGraveyard.poHead);

                    pNode.Clear();

                    pShieldRoot.Add(pNode);
                    pNode.ActivateGameSprite(pSB_Shields);
                    pNode.ActivateCollisionSprite(pSB_Boxes);

                }

                pNode = pNextNode;
            }

            //-------------------------------------------------------------------------------------------------
            //add the Columns to the Shields
            //-------------------------------------------------------------------------------------------------

            //random check
            //check to see if ShieldRoot has a shield
            Debug.Assert(pShieldRoot.GetFirstChild() != null);

            GameObject pShield = (GameObject)pShieldRoot.GetFirstChild();

            //iterate through the shield siblings
            while (pShield != null)
            {

                //iterate through the graveyard
                pNode = pGraveyard.poHead;
                while (pNode != null)
                {
                    //save this to iterate, because we will possibly remove the next pointer it
                    pNextNode = (GameObject)pNode.pNext;

                    //check the parent reference to the shield
                    if (pShield == pNode.pParent)
                    {
                        //dettach from list then add it to tree
                        pGraveyard.PrivDetach(pNode, ref pGraveyard.poHead);

                        pNode.Clear();

                        pShield.Add(pNode);
                        pNode.ActivateGameSprite(pSB_Shields);
                        pNode.ActivateCollisionSprite(pSB_Boxes);
                    }

                    pNode = pNextNode;
                }


                pShield = (GameObject)pShield.pNext;
            }

            //-------------------------------------------------------------------------------------------------
            //lastly add the bricks to the Shield columns in each shield
            //-------------------------------------------------------------------------------------------------

            pShield = (GameObject)pShieldRoot.GetFirstChild();
            Debug.Assert(pShield != null);

            while(pShield != null)
            {
                GameObject pShieldColumn = (GameObject)pShield.GetFirstChild();
                Debug.Assert(pShieldColumn != null);

                //iterate through the shield columns
                while (pShieldColumn != null)
                {

                    //iterate through the graveyard
                    pNode = pGraveyard.poHead;
                    while (pNode != null)
                    {
                        //save this to iterate, because we will possibly remove the next pointer it
                        pNextNode = (GameObject)pNode.pNext;

                        //check the parent reference to the column
                        if (pShieldColumn == pNode.pParent)
                        {
                            //dettach from list then add it to tree
                            pGraveyard.PrivDetach(pNode, ref pGraveyard.poHead);

                            ShieldBrick pAlien = (ShieldBrick)pNode;
                            pAlien.x = pAlien.OrigX();
                            pAlien.y = pAlien.OrigY();
                            pAlien.bMarkForDeath = false;

                            pNode.Clear();

                            pShieldColumn.Add(pNode);
                            pNode.ActivateGameSprite(pSB_Shields);
                            pNode.ActivateCollisionSprite(pSB_Boxes);
                        }

                        pNode = pNextNode;
                    }


                    pShieldColumn = (GameObject)pShieldColumn.pNext;
                }

                pShield = (GameObject)pShield.pNext;
            }


        }

        private void PrivDetach(GameObject pNode, ref GameObject pHead)
        {
            Debug.Assert(pNode != null);

            //check if its at the middle or end
            if(pNode.pPrev != null)
            {
                pNode.pPrev.pNext = pNode.pNext;
            }

            //its the head node
            else
            {
                pHead = (GameObject)pNode.pNext;
            }

            //don't forget the next pointer
            //for the front and middle nodes
            if(pNode.pNext != null)
            {
                pNode.pNext.pPrev = pNode.pPrev;
            }
        }

        public static void DumpNodes()
        {
            GraveyardMan pGraveyard = GraveyardMan.PrivGetInstance();
            Debug.Assert(pGraveyard != null);

            GameObject pNode = pGraveyard.poHead;

            if(pNode == null)
            {
                Debug.WriteLine("-----------------------------");
                Debug.WriteLine("Graveyard Empty");
                Debug.WriteLine("-----------------------------");
            }
            else
            {
                int i = 0;
                while (pNode != null)
                {
                    Debug.WriteLine("");
                    Debug.WriteLine("{0})----------", i);
                    Debug.WriteLine("{0} ({1})", pNode.GetName(), pNode.GetHashCode());
                    Debug.WriteLine("Parent: {0} X:{1}, Y:{2}", pNode.pParent, pNode.x, pNode.y);

                    pNode = (GameObject)pNode.pNext;
                    i++;
                }
            }
            

        }

        public static void KillAll()
        {
            GraveyardMan pGraveyard = GraveyardMan.PrivGetInstance();
            Debug.Assert(pGraveyard != null);

            // trying to gather leftover Aliens
            GameObject pAlienGroup = GONodeMan.Find(GameObject.Name.AlienGrid);

            //Start with the columns
            GameObject pColumn = (GameObject)pAlienGroup.GetFirstChild();
            while (pColumn != null)
            {
                //store the next column
                GameObject pNextColumn = (GameObject)pColumn.pNext;

                //remove aliens from the column
                GameObject pAlien = (GameObject)pColumn.GetFirstChild();
                while (pAlien != null)
                {
                    GameObject pNextAlien = (GameObject)pAlien.pNext;

                    pAlien.Remove();

                    pAlien = pNextAlien;
                }

                //once your out do a check to see if its null
                Debug.Assert(pColumn.GetFirstChild() == null);

                //remove the column
                pColumn.Remove();

                pColumn = pNextColumn;
            }

            // trying to gather leftover Shieldbricks
            GameObject pShieldRoot = GONodeMan.Find(GameObject.Name.ShieldRoot);

            //start with shields (4 of them if all there)
            GameObject pShield = (GameObject)pShieldRoot.GetFirstChild();
            while (pShield != null)
            {
                //store the next shield
                GameObject pNextShield = (GameObject)pShield.pNext;

                //go to the shield columns
                GameObject pShieldColumn = (GameObject)pShield.GetFirstChild();
                while (pShieldColumn != null)
                {
                    GameObject pNextColumn = (GameObject)pShieldColumn.pNext;

                    //finally at the bricks
                    GameObject pBrick = (GameObject)pShieldColumn.GetFirstChild();
                    while(pBrick != null)
                    {
                        GameObject pNextBrick = (GameObject)pBrick.pNext;

                        pBrick.Remove();

                        pBrick = pNextBrick;
                    }

                    //once your out do a check to see if its null
                    Debug.Assert(pShieldColumn.GetFirstChild() == null);

                    //remove the column from shield
                    pShieldColumn.Remove();

                    pShieldColumn = pNextColumn;
                }

                //once your out do a check to see if its null
                Debug.Assert(pShield.GetFirstChild() == null);

                //remove the shield from root
                pShield.Remove();

                pShield = pNextShield;
            }
        }
        //Breaking DRY Rule
        //Not happy, but its late
        private void PrivRelease(GameObject pGameObject)
        {
            if (this.poHead == null)
            {
                this.poHead = pGameObject;
                pGameObject.pNext = null;
                pGameObject.pPrev = null;
            }

            //every other time
            else
            {
                pGameObject.pNext = this.poHead;
                pGameObject.pPrev = null;
                this.poHead.pPrev = pGameObject;
                this.poHead = pGameObject;
            }
            this.Total++;
        }

        private void PrivFlush()
        {
            GameObject pNode;
            GameObject pTemp;

            pNode = this.poHead;

            while(pNode != null)
            {
                pTemp = pNode;
                pNode = (GameObject)pNode.pNext;

                Debug.Assert(pTemp != null);

                this.PrivDetach(pTemp, ref this.poHead);

                pTemp = null;

                this.Total--;
            }
        }

        private GraveyardMan()
        {
            this.poHead = null;
        }

        private static GraveyardMan PrivGetInstance()
        {

            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
