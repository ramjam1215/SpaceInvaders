using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SBatchMan_MLink : Manager
    {
        public SpriteBatch_Link poActive;
        public SpriteBatch_Link poReserve;
    }
    public class SpriteBatchMan : SBatchMan_MLink
    {

        private static SpriteBatchMan pInstance = null;
        private SpriteBatch poNodeCompare;

        public SpriteBatchMan(int reserveNum = 3, int growth = 1)
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.poNodeCompare = new SpriteBatch();
            
        }

        ~SpriteBatchMan()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~SpriteBatchMan(): {0} ", this.GetHashCode());
#endif
            this.poNodeCompare = null;
            SpriteBatchMan.pInstance = null;
        }

        public static void Create(int reserveNum = 3, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new SpriteBatchMan(reserveNum, growth);
            }
        }

        public static void Destroy()
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.PrivStatBatchDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("SpriteBatchMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", SpriteBatchMan.pInstance, SpriteBatchMan.pInstance.GetHashCode());
#endif

            SpriteBatchMan.pInstance = null;
            pMan.poNodeCompare = null;

        }

        public static SpriteBatch Add(SpriteBatch.Name name)
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            SpriteBatch pSBatch = (SpriteBatch)pMan.BaseAdd();

            pSBatch.SetName(name);

            return pSBatch;
        }

        public static void Remove(SpriteBatch pNode)
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void RemoveSprite(SBNode pSpriteBaseNode)
        {
            Debug.Assert(pSpriteBaseNode != null);
            //Get the manager that holds the sprite base node 
            SBNodeMan pSBNodeMan = pSpriteBaseNode.GetSBNodeMan();

            Debug.Assert(pSBNodeMan != null);
            pSBNodeMan.Remove(pSpriteBaseNode);

        }

        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.SetName(name);

            SpriteBatch pNode = (SpriteBatch)pMan.BaseFind(pMan.poNodeCompare);
            return pNode;
        }

        public static void Draw()
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            //need access to the Sprites,
            //in the SpriteBatch to render
            SpriteBatch pSBatch = (SpriteBatch)pMan.BaseGetActive();

            while (pSBatch != null)
            {
                if(pSBatch.bToggle == true)
                {
                    SBNodeMan pSBNodeMan = pSBatch.GetSBNodeMan();
                    Debug.Assert(pSBNodeMan != null);

                    pSBNodeMan.Draw();
                }
                
                pSBatch = (SpriteBatch)pSBatch.pNext;
            }
        }

        public static void DumpBatches()
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("Sprite Batch Manager Stats---------------------");
            pMan.BaseDumpNodes();
        }

        //special kind of node reference to a manager object
        protected override void DerivedDestroyNode(DLink pDLink)
        {
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine(" {0} ({1})", pDLink, pDLink.GetHashCode());
#endif

            SpriteBatch pNode = (SpriteBatch)pDLink;
            Debug.Assert(pNode != null);

            pNode.Destroy();
        }

        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            SpriteBatch pINode1 = (SpriteBatch)pDLink1;
            SpriteBatch pINode2 = (SpriteBatch)pDLink2;

            Boolean status = false;

            if (pINode1.GetName() == pINode2.GetName())
                status = true;

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new SpriteBatch();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            SpriteBatch pSpriteBatch = (SpriteBatch)pDLink;
            pSpriteBatch.DumpSBatch();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            SpriteBatch pNode = (SpriteBatch)pDLink;
        }

        private void PrivStatBatchDump()
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("Sprite Batch Manager Stats---------------------");
            pMan.BaseStatDump();
        }

        private static SpriteBatchMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
