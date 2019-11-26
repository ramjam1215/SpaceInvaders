using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObject : Component
    {
        
        public enum Name
        {
            Crab,
            Squid,
            Octopus,
            AlienGrid,

            

            UFO,
            UFORoot,

            Ship,
            ShipRoot,

            Missile,
            MissileGroup,

            Column_1,
            Column_2,
            Column_3,
            Column_4,
            Column_5,
            Column_6,
            Column_7,
            Column_8,
            Column_9,
            Column_10,
            Column_11,

            ShieldRoot,
            ShieldGrid_1,
            ShieldGrid_2,
            ShieldGrid_3,
            ShieldGrid_4,

            ShieldColumn_1,
            ShieldColumn_2,
            ShieldColumn_3,
            ShieldColumn_4,
            ShieldColumn_5,
            ShieldColumn_6,
            ShieldColumn_7,
            ShieldBrick,

            WallRoot,
            WallGroup,
            TopWall,
            LeftWall,
            RightWall,
            BottomWall,

            BombRoot,
            Bomb,
            
            SplatRoot,
            SplatAnim,
            Splat,
            Explosion,

            Null_Object,
            Unitialized
        }

        private GameObject.Name name;
        public float x;
        public float y;

        public bool bMarkForDeath;
        private ProxySprite poProxySprite;
        private ColObject poColObj;

        private bool bPlayerOpt;

        //protected GameObject(GameObject.Name goName)
        //{
        //    this.name = goName;
        //    this.x = 0.0f;
        //    this.y = 0.0f;
        //    this.pProxySprite = null;
        //}

        protected GameObject(GameObject.Name goName, GameSprite.Name spriteName)
        {
            this.name = goName;
            this.bMarkForDeath = false;
            this.x = 0.0f;
            this.y = 0.0f;

            this.bPlayerOpt = true;

            this.poProxySprite = ProxySpriteMan.Add(spriteName);

            this.poColObj = new ColObject(this.poProxySprite);
            Debug.Assert(this.poColObj != null);
        }

        ~GameObject()
        {
            this.name = GameObject.Name.Unitialized;
            this.poProxySprite = null;
            this.poColObj = null;
        }

        public GameObject.Name GetName()
        {
            return this.name;
        }

        public void SetName(Name name)
        {
            this.name = name;
        }

        public bool GetPlayerOption()
        {
            return this.bPlayerOpt;
        }

        public void SetPlayerOption(bool b)
        {
            this.bPlayerOpt = b;
        }

        public ProxySprite GetProxy()
        {
            return this.poProxySprite;
        }

       

        public ColObject GetColObject()
        {
            return this.poColObj;
        }

        

        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.poColObj != null);

            pSpriteBatch.Attach(this.poColObj.pColSprite);
        }

        public void ActivateGameSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.poProxySprite);
        }

        //public void DeActivateColSprite(SpriteBatch pSpriteBatch)
        //{
        //    Debug.Assert(pSpriteBatch != null);
        //    pSpriteBatch.Dettach(this.poColObj.pColSprite);
        //}

        //"virtual" property behaves like an abstract method in a base class
        // lets the "override" property to be used in derived 
        //classes to extend or change a method
        public virtual void Update()
        {

            Debug.Assert(this.poProxySprite != null);
            this.poProxySprite.PosX(this.x);
            this.poProxySprite.PosY(this.y);

            Debug.Assert(this.poColObj != null);
            this.poColObj.UpdatePos(this.x, this.y);

            Debug.Assert(this.poColObj.pColSprite != null);
            this.poColObj.pColSprite.Update();
        }

        //intially had this in each of my objects(D.R.Y)
        //to remove Sprite & its collision box 
        //from the sprite batches and GO node man
        public virtual void Remove()
        {

            //Debug.WriteLine("Remove: {0}", this);


            Debug.Assert(this.poProxySprite != null);
            SBNode pSBNode = this.poProxySprite.GetSBNode();

            Debug.Assert(pSBNode != null);
            SpriteBatchMan.RemoveSprite(pSBNode);

            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);
            pSBNode = this.poColObj.pColSprite.GetSBNode();

            Debug.Assert(pSBNode != null);
            SpriteBatchMan.RemoveSprite(pSBNode);


            GONodeMan.Dettach(this);

            //then add it to a graveyard to use later
            GraveyardMan.Bury(this);
        }

        public virtual void Reset()
        {
            Debug.Assert(this.poProxySprite != null);
            SBNode pSBNode = this.poProxySprite.GetSBNode();

            Debug.Assert(pSBNode != null);
            SpriteBatchMan.RemoveSprite(pSBNode);

            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);
            pSBNode = this.poColObj.pColSprite.GetSBNode();

            Debug.Assert(pSBNode != null);
            SpriteBatchMan.RemoveSprite(pSBNode);


            GONodeMan.Dettach(this);
        }


        // method for classes that inherit from Game Object
        // updates x, y, height, and width for the collision rectangle that encapsulates the child collission rectangles
        protected void BaseUpdateBoundingBox(Component pStart)
        {

            GameObject pNode = (GameObject)pStart;

            //get collision retangle so we can update in a loop of it's children
            ColRect ColTotal = this.poColObj.poColRect;

            //Get its first child so we can do a loop
            pNode = (GameObject)Iterator.GetChild(pNode);

            if(pNode != null)
            {
                //make "this" parent collision rectangle start off
                //as the size of its first child
                ColTotal.Set(pNode.poColObj.poColRect);

                //Now we loop through children 
                //and make parent collision box bigger via a union method
                while (pNode != null)
                {
                    ColTotal.Union(pNode.poColObj.poColRect);

                    pNode = (GameObject)Iterator.GetSibling(pNode);
                }


                this.x = this.poColObj.poColRect.x;
                this.y = this.poColObj.poColRect.y;
            }
           
        }

        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);

            this.poColObj.pColSprite.SetLineColor(red, green, blue);
        }

        public void DumpGO()
        {
            Debug.WriteLine("Name: {0} ({1})", this.name, this.GetHashCode() );

            if(this.poProxySprite != null)
            {
                Debug.WriteLine("------pProxySprite: {0}", this.poProxySprite.GetName());
                Debug.WriteLine("------pRealSprite: {0}", this.poProxySprite.GetRealSprite().GetName());
            }

            else
            {
                Debug.WriteLine("------pProxySprite: null");
                Debug.WriteLine("------pRealSprite: null");
            }
            Debug.WriteLine("------(x,y): {0}, {1}", this.x, this.y);
        }
    }
}

