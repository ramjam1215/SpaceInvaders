using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGroup : Composite
    {
        private float deltaMove;
        private float deltaStep;
        public bool bToggle;

        public AlienGroup(GameObject.Name goName, GameSprite.Name spriteName, float posX, float posY)
            : base(goName, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.deltaMove = 15.0f;
            this.deltaStep = 20.0f;

            this.bToggle = true;
        }



        public override void Accept(ColVisitor other)
        {
            //what is the reaction of the "other" object
            // with the Alien Group
            other.VisitGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //AlienGroup is hit by Missile Group
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            //Now check the missile group v Alien Columns
            GameObject pGameObject = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitMissile(Missile m)
        {
            //AlienGroup is hit by Missile Group
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            //Now check the missile group v Alien Columns
            GameObject pColumnGO = (GameObject)ForwardIterator.GetChild(this);

            //Check the Columns
            ColPair.Collide(m, pColumnGO);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);

            //just a visual check to make sure it works
            //this.poColObj.poColRect.width += 10.0f;

            base.Update();

        }

        public void MoveAcross()
        {
            ForwardIterator pForIter = new ForwardIterator(this);

            Component pNode = pForIter.First();
            while (!pForIter.IsDone())
            {
                GameObject pGameObject = (GameObject)pNode;
                pGameObject.x += this.deltaMove;

                pNode = pForIter.Next();
                
            }
        }

        public void MoveDown()
        {
            ForwardIterator pForIter = new ForwardIterator(this);

            Component pNode = pForIter.First();
            while (!pForIter.IsDone())
            {
                GameObject pGameObject = (GameObject)pNode;
                pGameObject.y -= this.deltaStep;

                pNode = pForIter.Next();

            }
        }

        public void MoveUp()
        {
            //this is when the aliens hit the player the reset
            GameObject pAlienColumn = (GameObject)this.GetFirstChild();
            while(pAlienColumn != null)
            {
                GameObject pNextCol = (GameObject)pAlienColumn.pNext;

                GameObject pAlien = (GameObject)pAlienColumn.GetFirstChild();
                while(pAlien != null)
                {
                    GameObject pNextAlien = (GameObject)pAlien.pNext;
                    pAlien.y += 400;

                    pAlien = pNextAlien;
                }

                pAlienColumn = pNextCol;
            }
        }

        public float GetDeltaMove()
        {
            return this.deltaMove;
        }

        public void SetDeltaMove(float delta)
        {
            this.deltaMove = delta;
        }
        
    }
}
