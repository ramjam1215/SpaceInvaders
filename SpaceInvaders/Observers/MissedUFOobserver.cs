using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissedUFOobserver : ColObserver
    {
        private GameObject pUFO;
        private GameObject pWall;

        public MissedUFOobserver()
        {
            this.pUFO = null;
        }

        public MissedUFOobserver(MissedUFOobserver u)
        {
            Debug.Assert(u != null);
            this.pUFO = u.pUFO;
        }

        public override void Notify()
        {
            //Double-check to verify the different visitor pattern
            this.pUFO = (UFO)this.pSubject.pObjA;
            this.pWall = (WallCategory)this.pSubject.pObjB;
            Debug.Assert(this.pUFO != null);

            if (pUFO.bMarkForDeath == false)
            {
                pUFO.bMarkForDeath = true;

                //this is to eliminate some of the checks that the UFORoot 
                //does with the wall while it generates another UFO
                UFORoot pUFORoot = (UFORoot)GONodeMan.Find(GameObject.Name.UFORoot);
                Debug.Assert(pUFORoot != null);

                pUFORoot.x = -100;
                pUFORoot.y = -100;
                pUFORoot.SetDeltaMove(-1.0f);


                MissedUFOobserver pObserver = new MissedUFOobserver(this);
                DelayedObjectMan.Attach(pObserver);
            }


        }

        public override void Execute()
        {
            this.pUFO.Remove();
        }
    }
}
