using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipStateReady : ShipShootState
    {

        private static IrrKlang.ISoundEngine pSndEngine = new IrrKlang.ISoundEngine();
        //ready state; you can shoot a missile

        public override void Handle(Ship pShip)
        { 
            pShip.SetShootState(ShipMan.ShootState.MissileFlying);
        }

        public override void ShootMissile(Ship pShip)
        {
            Missile pMissile = ShipMan.ActivateMissile();

            pSndEngine.SoundVolume = 0.2f;
            pSndEngine.Play2D("shoot.wav");

            pMissile.SetPos(pShip.x, pShip.y + 15);
            pMissile.SetActive(true);

            //then sets you to missile flying state
            //aka u cant shoot a missile
            this.Handle(pShip);
        }
    }
}
