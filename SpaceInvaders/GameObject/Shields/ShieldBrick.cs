using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldBrick : ShieldCategory
    {
        public float FirstPosX;
        public float FirstPosY;
        public ShieldBrick(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY) 
            : base(name, spriteName, ShieldCategory.Type.Brick)
        {
            this.x = posX;
            this.y = posY;

            this.FirstPosX = posX;
            this.FirstPosY = posY;

            this.SetCollisionColor(1.0f, 1.0f, 1.0f);
        }

        ~ShieldBrick()
        {

        }
        public override void Accept(ColVisitor other)
        {
            other.VisitShieldBrick(this);
        }

        public override void VisitMissile(Missile m)
        {
            //keep it alphabetical
            // Missile v Shield brick
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBomb(Bomb b)
        {
            //keep it alphabetical
            //Bomb v Shield brick
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitAlien(AlienCategory a)
        {
            //Alien v Shield Brick
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public float OrigX()
        {
            return this.FirstPosX;
        }

        public float OrigY()
        {
            return this.FirstPosY;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
