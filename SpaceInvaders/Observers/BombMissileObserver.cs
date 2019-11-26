using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombMissileObserver : ColObserver
    {
        private GameObject pBomb;
        private GameObject pMissile;
        
    public BombMissileObserver()
        {
            this.pBomb = null;
            this.pMissile = null;
        }

        public BombMissileObserver(BombMissileObserver pObserv)
        {
            Debug.Assert(pObserv != null);

            this.pBomb = pObserv.pBomb;
            this.pMissile = pObserv.pMissile;

        }
       

        public override void Notify()
        {
            this.pBomb = (Bomb)this.pSubject.pObjA;
            Debug.Assert(this.pBomb != null);
            this.pMissile = (Missile)this.pSubject.pObjB;
            Debug.Assert(this.pMissile != null);


            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;

                if (pMissile.bMarkForDeath == false)
                {
                    pMissile.bMarkForDeath = true;

                    BombMissileObserver pObserver = new BombMissileObserver(this);
                    DelayedObjectMan.Attach(pObserver);

                }
            }


        }


        public override void Execute()
        {
            GameObject pBomb = this.pBomb;
            GameObject pBombRoot = (GameObject)Iterator.GetParent(pBomb);

            GameObject pMissile = this.pMissile;
            GameObject pMissileRoot = (GameObject)Iterator.GetParent(pMissile);

            pBomb.Remove();
            pMissile.Remove();


        }

    }
}
