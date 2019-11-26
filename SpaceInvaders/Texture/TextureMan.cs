using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TextureMan_MLink : Manager
    {
        public Texture_Link poActive;
        public Texture_Link poReserve;
    }
    class TextureMan : TextureMan_MLink
    {
        private static TextureMan pInstance = null;
        private Texture poTexCompare;

        public TextureMan(int reserveNum = 2, int growth = 1)
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.poTexCompare = new Texture();
        }

        ~TextureMan()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~TextureMan(): {0} ", this.GetHashCode());
#endif
            this.poTexCompare = null;
            TextureMan.pInstance = null;
        }

        public static void Create(int reserveNum = 2, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            //Singleton Time!!!
            if (pInstance == null)
                pInstance = new TextureMan(reserveNum, growth);

            // added a null object texture to refer to right when manager is created
            TextureMan.Add(Texture.Name.NullObject, "HotPink.tga");

            //added a default texture to refer to right when manager is created 
            TextureMan.Add(Texture.Name.Default, "HotPink.tga");
        }

        public static void Destroy()
        {
            TextureMan pTMan = TextureMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            pTMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("TextureMan.Destroy()");
#endif
            pTMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("{0} ({1})", pTMan.poTexCompare, pTMan.poTexCompare.GetHashCode() );
            Debug.WriteLine("{0} ({1})", TextureMan.pInstance, TextureMan.pInstance.GetHashCode() );
#endif

            pTMan.poTexCompare = null;
            TextureMan.pInstance = null;
        }


        public static Texture Add(Texture.Name name, String pTexName)
        {
            TextureMan pTMan = TextureMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            Texture pTNode = (Texture)pTMan.BaseAdd();
            Debug.Assert(pTNode != null);

            Debug.Assert(pTexName != null);

            pTNode.Set(name, pTexName);
            return pTNode;
        }

        public static Texture Find(Texture.Name name)
        {
            TextureMan pTMan = TextureMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            pTMan.poTexCompare.SetName(name);

            Texture pTNode = (Texture)pTMan.BaseFind(pTMan.poTexCompare);
            return pTNode;
        }

        public static void Remove(Texture pTNode)
        {
            TextureMan pTMan = TextureMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            Debug.Assert(pTNode != null);

            pTMan.BaseRemove(pTNode);
        }

        public static void DumpTextures()
        {
            TextureMan pTMan = TextureMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            pTMan.BaseDumpNodes();
        }

        private void PrivStatDump()
        {
            TextureMan pTMan = TextureMan.PrivGetInstance();
            Debug.Assert(pTMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("Texture Manager Stats-------------------------");
            Debug.WriteLine("TO SELF!! The null texture and default texture included in stats.");
            pTMan.BaseStatDump();
        }

        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            Texture pTNode1 = (Texture)pDLink1;
            Texture pTNode2 = (Texture)pDLink2;

            Boolean status = false;

            if (pTNode1.GetName() == pTNode2.GetName())
                status = true;

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new Texture();

            Debug.Assert(pNode != null);
            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Texture pTNode = (Texture)pDLink;
            pTNode.DumpTexture();
        }


        protected override void DerivedWash(DLink pDLink)
        {
            Texture pTNode = (Texture)pDLink;
            pTNode.Wash();
        }

        private static TextureMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}

