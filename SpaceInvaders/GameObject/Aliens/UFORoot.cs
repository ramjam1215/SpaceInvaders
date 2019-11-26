using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFORoot : Composite
    {
        private float deltaMove;
        public UFORoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY) 
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.deltaMove = 2.0f;

            //will only appear if I add it to a spritebatch
            this.GetColObject().pColSprite.SetLineColor(1, 0, 0);
        }
        public override void Accept(ColVisitor other)
        {
            other.VisitUFORoot(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObject = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitMissile(Missile m)
        {
            GameObject pGameObject = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObject);
        }

        public float GetDeltaMove()
        {
            return this.deltaMove;
        }

        public float SetDeltaMove(float delta)
        {
            this.deltaMove *= delta;

            return this.deltaMove;
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }
    }
}
