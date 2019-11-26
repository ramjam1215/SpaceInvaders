using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRoot : Composite
    {
        //same concept as the AlienGroup, in my mind, 
        //its just a holder, sort of speak, to attach to the GONodeMan
        public ShipRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY) 
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.GetColObject().pColSprite.SetLineColor(1, 0, 0);
        }

        public override void Accept(ColVisitor other)
        {
            //Q: what hits the shipRoot? 
            //A: mostly Bombs, but also aliens/AlienGroup(Get to later)....
            other.VisitShipRoot(this);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            //BombRoot v ShipRoot
            //now check bomb
            GameObject pGameObject = (GameObject)Iterator.GetChild(b);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitBomb(Bomb b)
        {
            //BombRoot v ShipRoot
            //now check ship
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(b, pGameObject);
        }

        public override void VisitGroup(AlienGroup a)
        {
            //AlienGroup v ShipRoot
            //now check Column
            GameObject pGameObject = (GameObject)Iterator.GetChild(a);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitColumn(AlienColumn a)
        {
            //Alien Column v ShipRoot
            //now check aliens
            GameObject pGameObject = (GameObject)Iterator.GetChild(a);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitAlien(AlienCategory a)
        {
            //Alien v ShipRoot
            //now check ship
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
