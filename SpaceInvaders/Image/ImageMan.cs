using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ImageMan_MLink : Manager
    {
        public Image_Link poActive;
        public Image_Link poReserve;
    }
    class ImageMan : ImageMan_MLink
    {
        private static ImageMan pInstance = null;
        private Image poImageCompare;

        public ImageMan(int reserveNum = 5, int growth = 3)
            : base()
        {
            this.BaseIntialize(reserveNum, growth);

            this.poImageCompare = new Image();
        }

        public static void Create(int reserveNum, int growth)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(growth > 0);

            Debug.Assert(pInstance == null);

            if (pInstance == null)
                pInstance = new ImageMan(reserveNum, growth);

            Image pImage;

            //added a null object image from "hotpink.tga"
            pImage = ImageMan.Add(Image.Name.NullObject, Texture.Name.NullObject, 0, 0, 128, 128);
            Debug.Assert(pImage != null);

            //Added a default Image from "hotpink.tga"
            pImage = ImageMan.Add(Image.Name.Default, Texture.Name.Default, 0, 0, 128, 128);
            Debug.Assert(pImage != null);
        }

        public static void Destroy()
        {
            ImageMan pIMan = ImageMan.PrivGetInstance();
            Debug.Assert(pIMan != null);

            pIMan.PrivStatDump();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("ImageMan.Destroy()");
#endif
            pIMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("{0} ({1})", pIMan.poImageCompare, pIMan.poImageCompare.GetHashCode());
            Debug.WriteLine("{0} ({1})", ImageMan.pInstance, ImageMan.pInstance.GetHashCode());
#endif

            pIMan.poImageCompare = null;
            ImageMan.pInstance = null;
        }

        public static Image Add(Image.Name ImageName, Texture.Name TextureName, float x, float y, float width, float height)
        {
            ImageMan pIMan = ImageMan.PrivGetInstance();
            Debug.Assert(pIMan != null);

            Image pINode = (Image)pIMan.BaseAdd();
            Debug.Assert(pINode != null);

            // I really liked the default image and texture use
            Texture pTexture = TextureMan.Find(TextureName);
            if (pTexture == null)
            {
                pTexture = TextureMan.Find(Texture.Name.Default);
                Debug.Assert(pTexture != null);
                x = 0; y = 0; width = 128; height = 128;
            }


            pINode.Set(ImageName, pTexture, x, y, width, height);
            return pINode;
        }

        public static Image Find(Image.Name name)
        {
            ImageMan pIMan = ImageMan.PrivGetInstance();
            Debug.Assert(pIMan != null);

            pIMan.poImageCompare.SetName(name);

            Image pINode = (Image)pIMan.BaseFind(pIMan.poImageCompare);
            return pINode;
        }

        public static void Remove(Image pINode)
        {
            ImageMan pIMan = ImageMan.PrivGetInstance();
            Debug.Assert(pIMan != null);

            Debug.Assert(pINode != null);
            pIMan.BaseRemove(pINode);
        }

        public static void DumpImages()
        {
            ImageMan pIMan = ImageMan.PrivGetInstance();
            Debug.Assert(pIMan != null);

            pIMan.BaseDumpNodes();
        }

        private void PrivStatDump()
        {
            ImageMan pIMan = ImageMan.PrivGetInstance();
            Debug.Assert(pIMan != null);

            Debug.WriteLine("");
            Debug.WriteLine("Image Manager Stats-------------------------");
            pIMan.BaseStatDump();
        }

        protected override bool DerivedCompare(DLink pDLink1, DLink pDLink2)
        {
            Debug.Assert(pDLink1 != null);
            Debug.Assert(pDLink2 != null);

            Image pINode1 = (Image)pDLink1;
            Image pINode2 = (Image)pDLink2;

            Boolean status = false;

            if (pINode1.GetName() == pINode2.GetName())
                status = true;

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new Image();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pDLink)
        {
            Image pINode = (Image)pDLink;
            pINode.DumpImage();
        }

        protected override void DerivedWash(DLink pDLink)
        {
            Image pINode = (Image)pDLink;
            pINode.Wash();
        }

        private static ImageMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }


    }
}

