using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColPair : DLink
    {

        public enum Name
        {
            Bomb_V_Missile,

            Ship_V_LWall,
            Ship_V_RWall,
            Ship_V_Bomb,
            

            Aliens_V_LWall,
            Aliens_V_RWall,
            Aliens_V_BWall,
            Aliens_V_Shield,
            Aliens_V_Ship,

            Missile_V_TWall,
            Missile_V_Alien,
            Missile_V_Shield,
            Missile_V_UFO,

            Bomb_V_Wall,
            Bomb_V_Shield,

            UFO_V_LWall,
            UFO_V_RWall,    

            NullObject,
            Uninitialized
        }

        public ColPair.Name name;
        public GameObject treeA;
        public GameObject treeB;
        public ColSubject poSubject;

        public ColPair()
            : base()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = ColPair.Name.Uninitialized;

            this.poSubject = new ColSubject();
            Debug.Assert(this.poSubject != null);

        }

        ~ColPair()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~ColPair():{0} ", this.GetHashCode());
#endif
            this.name = ColPair.Name.Uninitialized;
            this.treeA = null;
            this.treeB = null;
            this.poSubject = null;
        }

        public void Set(ColPair.Name colpairName, GameObject pTreeRootA, GameObject pTreeRootB)
        {
            Debug.Assert(pTreeRootA != null);
            Debug.Assert(pTreeRootB != null);

            this.treeA = pTreeRootA;
            this.treeB = pTreeRootB;
            this.name = colpairName;
        }

        public void Wash()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = ColPair.Name.Uninitialized;

        }

        public ColPair.Name GetName()
        {
            return this.name;
        }

        public void SetName(ColPair.Name inName)
        {
            this.name = inName;
        }

        //FOR THE RECORD
        //I'm not proud of this, but i created another collision with a slight difference for columns
        // i'm breaking the D.R.Y rule, but i'm sucking right now
        static public void CollideColumns(GameObject pSafeTreeA, GameObject pSafeTreeB) 
        {
            GameObject pNodeA = pSafeTreeA;
            GameObject pNodeB = pSafeTreeB;

            while (pNodeA != null)
            {
                pNodeB = pSafeTreeB;

                while (pNodeB != null)
                {

                    //Debug.WriteLine("ColPair:    test:  {0}, {1}", pNodeA.GetName(), pNodeB.GetName());

                    ColRect rectA = pNodeA.GetColObject().poColRect;
                    ColRect rectB = pNodeB.GetColObject().poColRect;

                    //check if their is a collision
                    if (ColRect.Intersect(rectA, rectB))
                    {
                        //then see what the reaction is
                        pNodeA.Accept(pNodeB);
                        break;
                    }

                    //this is the change and questionable part
                    //finicky when the aliens weren't close enough
                    Component pComp = (Component)pNodeB.pNext;
                    
                    //Checked all the Columns and there weren't any collisions so... continue
                    if(pComp == null)
                    {
                        break;
                    }
                    
                    //check the next column
                    if (pComp.holder == Component.Container.COMPOSITE)
                    {
                        Component pTemp = (Component)pNodeB;
                        Component pParent = pTemp.pParent;
                        Component pSibling = ForwardIterator.GetSibling(pParent);

                        //if there are no more columns check the children of the last column?
                        if(pSibling == null)
                        {
                            pNodeB = (GameObject)ForwardIterator.GetChild(pNodeB);
                            continue;
                        }

                        pNodeB = (GameObject)ForwardIterator.GetChild(pSibling);
                        continue;

                    }
                    pNodeB = (GameObject)Iterator.GetSibling(pNodeB);
                }

                pNodeA = (GameObject)Iterator.GetSibling(pNodeA);
            }
            
        }


        static public void Collide(GameObject pSafeTreeA, GameObject pSafeTreeB)
        {
            GameObject pNodeA = pSafeTreeA;
            GameObject pNodeB = pSafeTreeB;

            

            while (pNodeA != null)
            {
                pNodeB = pSafeTreeB;

                while (pNodeB != null)
                {

                    //Debug.WriteLine("ColPair:    test:  {0}, {1}", pNodeA.GetName(), pNodeB.GetName() );

                    ColRect rectA = pNodeA.GetColObject().poColRect;
                    ColRect rectB = pNodeB.GetColObject().poColRect;

                    //check if their is a collision
                    if(ColRect.Intersect(rectA, rectB))
                    {
                        //then see what the reaction is
                        pNodeA.Accept(pNodeB);
                        
                        break;
                    }

                    pNodeB = (GameObject)Iterator.GetSibling(pNodeB);
                }

                pNodeA = (GameObject)Iterator.GetSibling(pNodeA);
            }
        }

        public void Process()
        {
            Collide(this.treeA, this.treeB);
        }

        public void Attach(ColObserver observer)
        {
            this.poSubject.Attach(observer);
        }

        public void NotifyListeners()
        {
            this.poSubject.Notify();
        }

        public void SetCollision(GameObject pGobjA, GameObject pGObjB)
        {
            Debug.Assert(pGobjA != null);
            Debug.Assert(pGObjB != null);

            this.poSubject.pObjA = pGobjA;
            this.poSubject.pObjB = pGObjB;
        }

        public void DumpColPair()
        {
            Debug.WriteLine("Name: {0} ({1})", this.GetName(), this.GetHashCode());
            Debug.WriteLine("TreeA: {0} ({1})", this.treeA.GetName(), this.treeA.GetHashCode());
            Debug.WriteLine("TreeA: {0} ({1})", this.treeB.GetName(), this.treeB.GetHashCode());
        }
    }
}
