using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveAlienObserver : ColObserver
    {
        private GameObject pAlien;

        public RemoveAlienObserver()
        {
            this.pAlien = null;
        }

        public RemoveAlienObserver(RemoveAlienObserver a)
        {
            Debug.Assert(a != null);
            this.pAlien = a.pAlien;
        }

        public override void Notify()
        {
            //remove alien
            //Debug.WriteLine("RemoveAlienObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            //a little tricky ObjA is the missile, we are not alphabetical
            //because of how the missile observer is set up
            this.pAlien = (AlienCategory)this.pSubject.pObjB;
            Debug.Assert(this.pAlien != null);



            if(pAlien.bMarkForDeath == false)
            {
                pAlien.bMarkForDeath = true;

                if(pAlien.GetName() == GameObject.Name.Squid)
                {
                    PlayerMan.SetP1Score(30);
                }
                    
                else if (pAlien.GetName() == GameObject.Name.Crab)
                {
                    PlayerMan.SetP1Score(40);
                }

                else
                {
                    PlayerMan.SetP1Score(50);
                }

                //pass brick reference to manager thats executes/removes objects later
                RemoveAlienObserver pObserver = new RemoveAlienObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            GameObject pAlienObject = (GameObject)this.pAlien;
            GameObject pColumn = (GameObject)Iterator.GetParent(pAlienObject);

            SpaceInvaders pGame = GameMan.GetGame();
            

            //always remove the alien
            pAlienObject.Remove();

            //check if Column has aliens left
            //true means we have no more children, and safe to remove column
            if (PrivCheckParent(pColumn) == true)
            {
                GameObject pGroup = (GameObject)Iterator.GetParent(pColumn);
                pColumn.Remove();

                //check if Grid/Group had any columns left
                //true means we have no more children, and safe to change states
                if (PrivCheckParent(pGroup) == true)
                {
                    //We just beat a wave, so call the the states handle
                    // to either go to next wave or to see if its the end of the game
                    TimerMan.Add(TimeEvent.Name.GameStateChange, new GameStateChange(true), 5.0f);
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
