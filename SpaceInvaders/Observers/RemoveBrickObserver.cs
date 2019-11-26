using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBrickObserver : ColObserver
    {
        private GameObject pBrick;

        public RemoveBrickObserver()
        {
            this.pBrick = null;
        }

        public RemoveBrickObserver(RemoveBrickObserver b)
        {
            Debug.Assert(b != null);
            this.pBrick = b.pBrick;
        }

        public override void Notify()
        {
            //remove Brick
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pBrick = (ShieldBrick)this.pSubject.pObjB;
            Debug.Assert(this.pBrick != null);

            if (pBrick.bMarkForDeath == false)
            {
                pBrick.bMarkForDeath = true;

                //pass brick reference to manager thats executes/removes objects later
                RemoveBrickObserver pObserver = new RemoveBrickObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            GameObject pBrickObject = (GameObject)this.pBrick;
            GameObject pColumn = (GameObject)Iterator.GetParent(pBrickObject);

            SpaceInvaders pGame = GameMan.GetGame();

            //always remove the brick
            pBrickObject.Remove();

            //check if Column has bricks left
            //true means we have no more children, and safe to remove column
            if(PrivCheckParent(pColumn) == true)
            {
                GameObject pGroup = (GameObject)Iterator.GetParent(pColumn);
                pColumn.Remove();
                
                //Check if Shield has any columns left 
                if(PrivCheckParent(pGroup)== true)
                {
                    pGroup.Remove();
                }

            }
        }

        //checks an object and sees if it has children left
        //if it doesn't then it returns true
        private bool PrivCheckParent(GameObject pObject)
        {
            GameObject pGameObject = (GameObject)Iterator.GetChild(pObject);
            if(pGameObject == null)
            {
                return true;
            }

            return false;
        }
    }
}
