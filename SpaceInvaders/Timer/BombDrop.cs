using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombDrop : Command
    {
        private AlienGroup pAlienGroup = null;


        public BombDrop(AlienGroup pGrid)
        {
            this.pAlienGroup = pGrid;
            Debug.Assert(this.pAlienGroup != null);
        }
        
        // need to test this
        public override void Execute(float deltaTime)
        {
            AlienColumn pSafety = (AlienColumn)Iterator.GetChild(this.pAlienGroup);
            if(pSafety != null)
            {
                AlienColumn pNode = pSafety;

                Random rNum = new Random();
                float ColNum = rNum.Next(0, 11);
                int count = 0;

                while (pNode != null)
                {
                    //check to shoot bomb
                    if (pNode.GetColIndex() == ColNum)
                    {
                        float minX = pNode.GetColObject().poColRect.minX;
                        float minY = pNode.GetColObject().poColRect.minY;

                        pNode.ActivateBomb(minX, minY);

                        TimerMan.Add(TimeEvent.Name.ColumnShoot, this, deltaTime);
                        break;
                    }

                    //might get some random or extra bomb drops
                    if (ColNum == count)
                    {
                        float minX = pNode.GetColObject().poColRect.minX;
                        float minY = pNode.GetColObject().poColRect.minY;

                        pNode.ActivateBomb(minX, minY);

                        TimerMan.Add(TimeEvent.Name.ColumnShoot, this, deltaTime);
                        break;
                    }

                    //at the last node and its not the column to shoot bomb
                    if (pNode.pNext == null && pNode.GetColIndex() != ColNum)
                    {
                        //go back to the front of the list
                        pNode = pSafety;
                        count++;
                        continue;
                    }

                    pNode = (AlienColumn)pNode.pNext;
                    count++;
                }
            }

            else
            {
                // do nothing
            }
            
        }
    }
}
