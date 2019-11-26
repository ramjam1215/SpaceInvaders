using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveMissileObserver : ColObserver
    {
        private Missile pMissile;


        public RemoveMissileObserver()
        {
            this.pMissile = null;
        }

        public RemoveMissileObserver(RemoveMissileObserver m)
        {
            Debug.Assert(m != null);
            this.pMissile = m.pMissile;
        }

        public override void Notify()
        {
            // Remove MissileObserver ObjectA is missile, 
            this.pMissile = (Missile)this.pSubject.pObjA;
            Debug.Assert(this.pMissile != null);


            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;


                //pass missile reference to manager thats executes/removes objects later
                RemoveMissileObserver pObserver = new RemoveMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            this.pMissile.Remove();
        }
    }
}
