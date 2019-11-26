using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombRoot : Composite
    {
        public BombRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.GetColObject().pColSprite.SetLineColor(1, 0, 0);
        }

        ~BombRoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitBombRoot(this);
        }

        //not sure if i need these, but taking a shot
        public override void VisitMissileGroup(MissileGroup m)
        {
            //Missile Group v BombGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", this.GetName(), m.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(this, pGameObject);
        }

        public override void VisitMissile(Missile m)
        {
            //Bomb v Missile 
            //Debug.WriteLine("         collide:  {0} <-> {1}", this.GetName(), m.GetName() );

            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(pGameObject, m);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }
    }
}
