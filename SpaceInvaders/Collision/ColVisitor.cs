using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ColVisitor : DLink
    {

        public virtual void VisitGroup(AlienGroup a)
        {
            Debug.WriteLine("Visit by AlienGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitColumn(AlienColumn a)
        {
            Debug.WriteLine("Visit by AlienColumn not implemented");
            Debug.Assert(false);
        }


        public virtual void VisitAlien(AlienCategory a)
        {
            Debug.WriteLine("Visit by octo not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitNullGameObject(NullGO n)
        {
            Debug.WriteLine("Visit by NullGO not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile m)
        {
            Debug.WriteLine("Visit by Missile not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissileGroup(MissileGroup m)
        {
            Debug.WriteLine("Visit by MissileGroup not implemented");
            Debug.Assert(false);
        }


        public virtual void VisitWallRoot(WallRoot w)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by WallGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitLeftWall(LeftWall w)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by LeftWall not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitRightWall(RightWall w)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by RightWall not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitTopWall(TopWall w)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by TopWall not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBottomWall(BottomWall w)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by BottomWall not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShipRoot(ShipRoot s)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by ShipRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShip(Ship s)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by Ship not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBombRoot(BombRoot b)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by BombRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBomb(Bomb b)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by Bomb not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldRoot(ShieldRoot s)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by ShieldRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldGrid(ShieldGrid s)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by ShieldGrid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldColumn(ShieldColumn s)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by ShieldColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldBrick(ShieldBrick s)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by ShieldBrick not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUFORoot(UFORoot u)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by UFORoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUFO(UFO u)
        {
            //shouldn't call this
            //need to implement in concrete class
            Debug.WriteLine("Visit by UFORoot not implemented");
            Debug.Assert(false);
        }

        abstract public void Accept(ColVisitor other);

    }
}
