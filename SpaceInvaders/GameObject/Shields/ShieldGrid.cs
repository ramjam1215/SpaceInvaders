using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldGrid : Composite
    {
        public ShieldGrid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~ShieldGrid()
        {

        }

        public override void Accept(ColVisitor other)
        {
            other.VisitShieldGrid(this);
        }

        public override void VisitMissile(Missile m)
        {
            //Missile v ShieldGrid
            //so check its children(columns)
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObject);
        }

        public override void VisitBomb(Bomb b)
        {
            //Bomb v ShieldGrid
            //so check its children(columns)
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(b, pGameObject);
        }

        public override void VisitAlien(AlienCategory a)
        {
            //Alien v ShieldGrid
            //so check its children(columns)
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
