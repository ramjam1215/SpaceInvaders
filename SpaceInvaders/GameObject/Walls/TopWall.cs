using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //Thought i would keep the names similar
    // so its top wall and not ceiling
    public class TopWall : WallCategory
    {
        public TopWall(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, WallCategory.Type.Top)
        {
            this.GetColObject().poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.GetColObject().pColSprite.SetLineColor(0, 1, 0);
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitTopWall(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //Debug.WriteLine("collide: {0}<->{1}", m.GetName(), this.GetName());

            //go to the missile object
            //check the missile v. "this" TopWall
            GameObject pGameObject = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObject, this);
        }

        public override void VisitMissile(Missile m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            //missile hit wall(keep it alsphabetical)
            //Debug.WriteLine("-------> BOOM!  <--------");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
            
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
