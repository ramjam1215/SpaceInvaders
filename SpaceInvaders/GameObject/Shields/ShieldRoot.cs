using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldRoot : Composite
    {
        public ShieldRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

        }
        ~ShieldRoot()
        {

        }

        public override void Accept(ColVisitor other)
        {
            other.VisitShieldRoot(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //MissileRoot v ShieldRoot
            //so check the missile against the ShieldRoot
            GameObject pGameObject = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitMissile(Missile m)
        {
            //Missile v ShieldRoot
            //now check against the Shield columns(for now)
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObject);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            //BombRoot v ShieldRoot
            // now check bomb against the shielroot
            GameObject pGameObject = (GameObject)Iterator.GetChild(b);
            ColPair.Collide(pGameObject, this);
        }
        public override void VisitBomb(Bomb b)
        {
            //Bomb v Shield
            //now check bomb against the shield columns
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(b, pGameObject);
        }

        public override void VisitGroup(AlienGroup a)
        {
            //Alien Grid v ShieldRoot
            GameObject pGameObject = (GameObject)Iterator.GetChild(a);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitColumn(AlienColumn a)
        {
            //now Alien Column v ShieldRoot
            GameObject pGameObject = (GameObject)Iterator.GetChild(a);
            ColPair.Collide(pGameObject, this);
        }
        public override void VisitAlien(AlienCategory a)
        {
            //now Alien Column v. ShieldRoot
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
