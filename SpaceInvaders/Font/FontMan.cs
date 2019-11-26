using System;
using System.Xml;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FontMan : Manager
    {
        private static FontMan pInstance = null;
        private Font pRefNode;

        private FontMan(int reserveNum = 3, int growth = 1) 
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.pRefNode = (Font)this.DerivedCreateNode();
        }

        public static void Create(int reserveNum = 3, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            if(pInstance == null)
            {
                pInstance = new FontMan(reserveNum, growth);
            }
        }

        public static void Destroy()
        {
            FontMan pFMan = FontMan.PrivGetInstance();
            Debug.Assert(pFMan != null);

            pFMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("FontMan.Destroy()");
#endif
            pFMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pFMan.pRefNode, pFMan.pRefNode.GetHashCode());
            Debug.WriteLine("     {0} ({1})", FontMan.pInstance, FontMan.pInstance.GetHashCode());
#endif
            pFMan.pRefNode = null;
            FontMan.pInstance = null;
        }

        public static Font Add(Font.Name name, SpriteBatch.Name SB_Name, String pMessage, Glyph.Name glyphName, float x, float y)
        {
            FontMan pFMan = FontMan.PrivGetInstance();
            Debug.Assert(pFMan != null);

            Font pFNode = (Font)pFMan.BaseAdd();
            Debug.Assert(pFNode != null);

            pFNode.Set(name, pMessage, glyphName, x, y);

            SpriteBatch pSpriteBatch = SpriteBatchMan.Find(SB_Name);
            Debug.Assert(pSpriteBatch != null);

            Debug.Assert(pFNode.pFontSprite != null);
            pSpriteBatch.Attach(pFNode.pFontSprite);

            return pFNode;
        }

        public static void AddXml(Glyph.Name glypName, String assetName, Texture.Name texName)
        {
            GlyphMan.AddXml(glypName, assetName, texName);
        }

        //DoubleCheck this
        public static void Remove(Font pFNode)
        {
            Debug.Assert(pFNode != null);
            FontMan pFMan = FontMan.PrivGetInstance();

            pFMan.BaseRemove(pFNode);
        }

        public static Font Find(Font.Name name)
        {
            FontMan pFMan = FontMan.PrivGetInstance();

            pFMan.pRefNode.name = name;

            Font pFontData = (Font)pFMan.BaseFind(pFMan.pRefNode);
            return pFontData;
        }

        public static void DumpFonts()
        {
            FontMan pFMan = FontMan.PrivGetInstance();
            Debug.Assert(pFMan != null);

            Debug.WriteLine("------ Fontzee ------");
            pFMan.BaseDumpNodes();
        }


        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            Font pFData1 = (Font)pDLink1;
            Font pFData2 = (Font)pDLink2;

            Boolean status = false;

            if (pFData1.name == pFData2.name)
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pFNode = new Font();
            Debug.Assert(pFNode != null);
            return pFNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            Font pFNode = (Font)pDLink;

            Debug.Assert(pFNode != null);
            pFNode.DumpFont();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            Font pFNode = (Font)pDLink;
            pFNode.Wash();
        }

        private void PrivStatDump()
        {
            FontMan pFMan = FontMan.PrivGetInstance();
            Debug.Assert(pFMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("Font Manager Stats---------------------");
            pFMan.BaseStatDump();
        }

        private static FontMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
