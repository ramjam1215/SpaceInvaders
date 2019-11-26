using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallRoot : Composite
    {
        public WallRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY) 
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }


        public override void Accept(ColVisitor other)
        {
            other.VisitWallRoot(this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitGroup(AlienGroup a)
        {
            //WallGroup is hit by Aliengrid
            //Debug.WriteLine("collide: {0}<->{1}", a.GetName(), this.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(a, pGameObject);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //WallGroup is hit by MissileGroup
            //Debug.WriteLine("collide: {0}<->{1}", m.GetName(), this.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObject);
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            //shiproot v Wallroot
            //Debug.WriteLine("collide: {0}<->{1}", s.GetName(), this.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(s);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitShip(Ship s)
        {
            //shiproot v Wallroot
            //Debug.WriteLine("collide: {0}<->{1}", s.GetName(), this.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(s, pGameObject);
        }

        public override void VisitUFORoot(UFORoot u)
        {
            //UFOroot v Wallroot
            //Debug.WriteLine("collide: {0}<->{1}", u.GetName(), this.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(u, pGameObject);
        }


        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs WallRoot
            //Debug.WriteLine("collide: {0}<->{1}", b.GetName(), this.GetName());

            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(b, pGameObject);
        }


    }
}
