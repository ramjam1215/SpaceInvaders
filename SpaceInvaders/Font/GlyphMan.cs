using System;
using System.Xml;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GlyphMan : Manager
    {
        private static GlyphMan pInstance = null;
        private Glyph pRefNode;

        private GlyphMan(int reserveNum = 3, int growth = 1): base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.pRefNode = (Glyph)this.DerivedCreateNode();
        }

        ~GlyphMan()
        {
            GlyphMan.pInstance = null;
            this.pRefNode = null;
        }

        public static void Create(int reserveNum = 3, int growth = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            if(pInstance == null)
            {
                pInstance = new GlyphMan(reserveNum, growth);
            }
        }

        public static void Destroy()
        {
            GlyphMan pGMan = GlyphMan.PrivGetInstance();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("GlyphMan.Destroy()");
#endif
            pGMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("{0} ({1})", pGMan.pRefNode, pGMan.pRefNode.GetHashCode());
            Debug.WriteLine("{0} ({1})", GlyphMan.pInstance, GlyphMan.pInstance.GetHashCode());
#endif

            pGMan.pRefNode = null;
            GlyphMan.pInstance = null;
        }

        public static Glyph Add(Glyph.Name name, int key, Texture.Name texName, float x, float y, float width, float height)
        {
            GlyphMan pGMan = GlyphMan.PrivGetInstance();
            Debug.Assert(pGMan != null);

            Glyph pGNode = (Glyph)pGMan.BaseAdd();
            Debug.Assert(pGNode != null);

            pGNode.Set(name, key, texName, x, y, width, height);
            return pGNode;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name texName)
        {
            System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if(reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }

                        else if(reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if(reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }

                        else if(reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if(reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }

                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement:
                        if(reader.Name == "character")
                        {
                            //Debug.WriteLine("key: {0}, x: {1}, y: {2}, width: {3}, height: {4}", key, x, y, width, height);
                            GlyphMan.Add(glyphName, key, texName, x, y, width, height);
                        }
                        break;
                }
            }


        }

        public static void Remove(Glyph pGNode)
        {
            Debug.Assert(pGNode != null);

            GlyphMan pGMan = GlyphMan.PrivGetInstance();
            Debug.Assert(pGMan != null);

            pGMan.BaseRemove(pGNode);
        }

        public static Glyph Find(Glyph.Name name, int key)
        {
            GlyphMan pGMan = GlyphMan.PrivGetInstance();
            Debug.Assert(pGMan != null);

            pGMan.pRefNode.name = name;
            pGMan.pRefNode.key = key;

            Glyph pGlyphData = (Glyph)pGMan.BaseFind(pGMan.pRefNode);
            return pGlyphData;
        }

        public static void DumpGlyphs()
        {
            GlyphMan pGMan = GlyphMan.PrivGetInstance();
            Debug.Assert(pGMan != null);

            Debug.WriteLine("------ Glyph Manager ------");
            pGMan.BaseDumpNodes();
        }


        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink1 != null);

            Glyph pGData1 = (Glyph)pDLink1;
            Glyph pGData2 = (Glyph)pDLink2;

            Boolean status = false;

            if (pGData1.name == pGData2.name && pGData1.key == pGData2.key)
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new Glyph();
            Debug.Assert(pNode != null);
            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            Glyph pNode = (Glyph)pDLink;

            Debug.Assert(pNode != null);
            pNode.DumpGlyph();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Debug.Assert(pDLink != null);
            Glyph pNode = (Glyph)pDLink;
            pNode.Wash();
        }

        private static GlyphMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }

    }
}
