using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldColumn : Composite
    {
        public ShieldColumn(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY) 
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~ShieldColumn()
        {

        }

        public override void Accept(ColVisitor other)
        {
            other.VisitShieldColumn(this);
        }

        public override void VisitMissile(Missile m)
        {
            //Missile v. Shield Column
            //check its children
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObject);
        }

        public override void VisitBomb(Bomb b)
        {
            //Bomb v Shield Column
            //now check its children(bricks)
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(b, pGameObject);
        }

        public override void VisitAlien(AlienCategory a)
        {
            //Alien v Shield Column
            //now check its children(bricks)
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(a, pGameObject);
        }


        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }
    }
}
