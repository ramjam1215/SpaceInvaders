using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //Thought i would keep the names similar
    // so its top wall and not floor
    public class BottomWall : WallCategory
    {
        public BottomWall(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, WallCategory.Type.Bottom)
        {
            this.GetColObject().poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.GetColObject().pColSprite.SetLineColor(0, 1, 0);
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitBottomWall(this);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            //Debug.WriteLine("collide: {0}<->{1}", b.GetName(), this.GetName());

            //go to the bomb object
            //check the bomb v. "this" BottomWall
            GameObject pGameObject = (GameObject)Iterator.GetChild(b);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitBomb(Bomb b)
        {
            // Bomb v. Wall(bottom)
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitGroup(AlienGroup a)
        {
            //Aliens Got to the bottom Game Over
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        //public override void VisitAlien(AlienCategory a)
        //{
        //    //Aliens Got to the bottom Game Over
        //    ColPair pColPair = ColPairMan.GetActiveColPair();
        //    pColPair.SetCollision(a, this);
        //    pColPair.NotifyListeners();
        //}
        public override void Update()
        {
            base.Update();
        }
    }
}
