using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatObserver : ColObserver
    {

        public override void Notify()
        {
            //when this notify is called we swap the images and draw it to the alien spritebatch
            //then maybe add a time event that..... removes it somehow... remove from spritebatch after a half second

            GameObject pObject = this.pSubject.pObjB;
            Debug.Assert(pObject != null);

            GameSprite pSplat = GameSpriteMan.Find(GameSprite.Name.AlienSplat);

            //future concept
            //pObject.GetProxy().SetRealSprite(this.pSprite);


            //get the locations and render the image
            pSplat.PosX(pObject.x);
            pSplat.PosY(pObject.y);
            pSplat.Update();

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);

            pSB_Aliens.Attach(pSplat);

            TimerMan.Add(TimeEvent.Name.SplatAnim, new SplatAnim(pSplat, pSB_Aliens), 0.02f);

            


            

        }
    }
}
