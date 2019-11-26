using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatShipObserver : ColObserver
    {
        public override void Notify()
        {
            //when this notify is called we swap the images and draw it to the alien spritebatch
            //then maybe add a time event that..... removes it somehow... remove from spritebatch after a half second

            GameObject pObject = this.pSubject.pObjB;
            Debug.Assert(pObject != null);

            GameSprite pShipSplat = GameSpriteMan.Find(GameSprite.Name.ShipSplat);

            //future concept
            //pObject.GetProxy().SetRealSprite(this.pSprite);


            //get the locations and render the image
            //I'm really upset about this........ but here we are creating a new
            pShipSplat.PosX(pObject.x);
            pShipSplat.PosY(pObject.y);
            pShipSplat.Update();

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);

            pSB_Aliens.Attach(pShipSplat);

            TimerMan.Add(TimeEvent.Name.SplatAnim, new SplatAnim(pShipSplat, pSB_Aliens), 0.5f);


        }
    }
}
