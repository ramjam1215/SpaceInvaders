using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOSplatObs : ColObserver
    { 
        public override void Notify()
        {
            GameObject pObject = this.pSubject.pObjB;
            Debug.Assert(pObject != null);

            GameSprite pUFOSplat = GameSpriteMan.Find(GameSprite.Name.UFOsplat);

            //future concept
            //pObject.GetProxy().SetRealSprite(this.pSprite);


            //get the locations and render the image
            pUFOSplat.PosX(pObject.x);
            pUFOSplat.PosY(pObject.y);
            pUFOSplat.Update();

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            

            pSB_Aliens.Attach(pUFOSplat);


            TimerMan.Add(TimeEvent.Name.SplatAnim, new SplatAnim(pUFOSplat, pSB_Aliens), 0.5f);
        }
    }
}
