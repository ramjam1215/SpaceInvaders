using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class PSMan_MLink : Manager
    {
        public ProxySprite_Link poActive;
        public ProxySprite_Link poReserve;
    }
    class ProxySpriteMan : PSMan_MLink
    {
        private static ProxySpriteMan pInstance = null;
        private ProxySprite poNodeCompare;

        public ProxySpriteMan(int reserveNum = 3, int growth = 1)
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.poNodeCompare = new ProxySprite();
        }

        public static void Destroy()
        {
            ProxySpriteMan pPSMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pPSMan != null);

            pPSMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("ProxySpriteMan.Destroy()");
#endif
            pPSMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pPSMan.poNodeCompare, pPSMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", ProxySpriteMan.pInstance, ProxySpriteMan.pInstance.GetHashCode());
#endif
            pPSMan.poNodeCompare = null;
            ProxySpriteMan.pInstance = null;
        }

        public static void Create(int reserveNum = 3, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            if (pInstance == null)
                pInstance = new ProxySpriteMan(reserveNum, growth);
        }

        public static ProxySprite Add(GameSprite.Name name)
        {
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            ProxySprite pNode = (ProxySprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);
            return pNode;
        }

        public static ProxySprite Find(ProxySprite.Name name)
        {
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.SetName(name);

            ProxySprite pNode = (ProxySprite)pMan.BaseFind(pMan.poNodeCompare);
            return pNode;
        }

        public static void Remove(GameSprite pNode)
        {
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            ProxySprite pNode1 = (ProxySprite)pDLink1;
            ProxySprite pNode2 = (ProxySprite)pDLink2;

            Boolean status = false;

            if (pNode1.GetName() == pNode2.GetName())
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new ProxySprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            ProxySprite pNode = (ProxySprite)pDLink;
            pNode.DumpProxy();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            ProxySprite pNode = (ProxySprite)pDLink;
            pNode.Wash();
        }

        private void PrivStatDump()
        {
            ProxySpriteMan pPSMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pPSMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("ProxySprite Manager Stats---------------------");
            pPSMan.BaseStatDump();
        }

        private static ProxySpriteMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}

