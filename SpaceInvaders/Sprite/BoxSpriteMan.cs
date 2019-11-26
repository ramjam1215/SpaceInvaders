using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BoxSpriteMan_MLink : Manager
    {
        public BoxSprite_Link poActive;
        public BoxSprite_Link poReserve;
    }
    class BoxSpriteMan : BoxSpriteMan_MLink
    {
        private static BoxSpriteMan pInstance = null;
        private BoxSprite poNodeCompare;

        public BoxSpriteMan(int reserveNum = 2, int growth = 1)
            : base()
        {
            base.BaseIntialize(reserveNum, growth);

            this.poNodeCompare = new BoxSprite();
        }
        public static void Create(int reserveNum, int growth)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
                pInstance = new BoxSpriteMan(reserveNum, growth);
        }
        public static void Destroy()
        {
            BoxSpriteMan pBSMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pBSMan != null);

            pBSMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("BoxSpriteMan.Destroy()");
#endif
            pBSMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pBSMan.poNodeCompare, pBSMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", BoxSpriteMan.pInstance, BoxSpriteMan.pInstance.GetHashCode());
#endif
            pBSMan.poNodeCompare = null;
            BoxSpriteMan.pInstance = null;
        }

        public static BoxSprite Add(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            BoxSpriteMan pBSMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pBSMan != null);

            BoxSprite pBSnode = (BoxSprite)pBSMan.BaseAdd();
            Debug.Assert(pBSnode != null);

            pBSnode.Set(name, x, y, width, height, pColor);
            return pBSnode;
        }

        public static BoxSprite Find(BoxSprite.Name name)
        {
            BoxSpriteMan pBSMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pBSMan != null);

            pBSMan.poNodeCompare.SetName(name);

            BoxSprite pBSnode = (BoxSprite)pBSMan.BaseFind(pBSMan.poNodeCompare);
            return pBSnode;
        }

        public static void Remove(BoxSprite pSBnode)
        {
            BoxSpriteMan pBSMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pBSMan != null);

            pBSMan.BaseRemove(pSBnode);
        }

        public static void PrintSpriteBoxes()
        {
            BoxSpriteMan pBSMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pBSMan != null);

            pBSMan.BaseDumpNodes();
        }

        private void PrivStatDump()
        {
            BoxSpriteMan pBSMan = BoxSpriteMan.PrivGetInstance();
            Debug.Assert(pBSMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("BoxSprite Manager Stats---------------------");
            pBSMan.BaseStatDump();
        }

        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            BoxSprite pBSnode1 = (BoxSprite)pDLink1;
            BoxSprite pBSnode2 = (BoxSprite)pDLink2;

            Boolean status = false;

            if (pBSnode1.GetName() == pBSnode2.GetName())
                status = true;

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new BoxSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Debug.Assert(pDLink != null);

            BoxSprite pBSnode = (BoxSprite)pDLink;
            pBSnode.DumpBoxSprite();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);

            BoxSprite pBSnode = (BoxSprite)pDLink;
            pBSnode.Wash();
        }

        private static BoxSpriteMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }

    }
}

