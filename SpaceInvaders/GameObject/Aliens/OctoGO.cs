using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class OctoGO : AlienCategory
    {
        public OctoGO(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.FirstPosX = posX;
            this.FirstPosY = posY;

        }

        public override void Accept(ColVisitor other)
        {
            other.VisitAlien(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            //Go to the missile object
            //check missile vs. "this" Octo object 
            GameObject pGameObject = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObject, this);
        }

        
            public override void VisitMissile(Missile m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            //Debug.WriteLine(" Alien ({0})", this.GetHashCode());
            //Debug.WriteLine("-------> BOOM!  <--------");
            //missile hit object

            ColPair pColPair = ColPairMan.GetActiveColPair();
            
            //not alaphabetical, RemoveMissileObserver expects missile to be objA
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }


        

        //override virtual method in base class
        //to extend and/or modify a method
        public override void Update()
        {
            base.Update();
        }
    }
}
