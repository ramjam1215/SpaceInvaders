using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombObserver : ColObserver
    {
        private Bomb pBomb;

        public BombObserver()
        {
            this.pBomb = null;
        }

        public BombObserver(BombObserver b)
        {
            Debug.Assert(b != null);
            this.pBomb = b.pBomb;
        }
        public override void Notify()
        {
            this.pBomb = (Bomb)this.pSubject.pObjA;
            Debug.Assert(this.pBomb != null);

            
            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;

                BombObserver pObserver = new BombObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }

        }

        public override void Execute()
        {
            this.pBomb.Remove();
        }
    }
}
