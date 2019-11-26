using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileGroup : Composite
    {

        public MissileGroup(GameObject.Name name, GameSprite.Name spriteName, int index, float posX, float posY)
                : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.GetColObject().pColSprite.SetLineColor(0, 0, 1);
        }


        public override void Accept(ColVisitor other)
        {
            //In MissileGroup now find the reaction
            //of the "other" object with MissileGroup
            other.VisitMissileGroup(this);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            //Missile Group v BombGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", b.GetName(), this.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(b);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitBomb(Bomb b)
        {
            ////Bomb v Missile 
            //Debug.WriteLine("         collide:  {0} <-> {1}", b.GetName(), this.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(b, pGameObject);
        }

        public override void Update()
        {

            base.BaseUpdateBoundingBox(this);
            base.Update();
        }
    }
}
