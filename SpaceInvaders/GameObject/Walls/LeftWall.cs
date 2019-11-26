using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class LeftWall : WallCategory
    {
        public LeftWall(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height) 
            : base(name, spriteName, WallCategory.Type.Left)
        {
            this.GetColObject().poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.GetColObject().pColSprite.SetLineColor(1, 0, 0);
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitLeftWall(this);
        }

        public override void VisitGroup(AlienGroup a)
        {
            //AlienGrid v. LeftWall
            //Go opposite direction and move down

            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitUFORoot(UFORoot u)
        {
            //Debug.WriteLine("collide: {0}<->{1}", u.GetName(), this.GetName());

            //go to the UFO object
            //check the missile v. "this" TopWall
            GameObject pGameObject = (GameObject)Iterator.GetChild(u);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitUFO(UFO u)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", u.GetName(), this.GetName());

            //missile hit wall(keep it alsphabetical)
            //Debug.WriteLine("-------> Missed It!  <--------");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(u, this);
            pColPair.NotifyListeners();

        }

        public override void VisitShip(Ship s)
        {
            //ship v Wall(left)
            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(s, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
