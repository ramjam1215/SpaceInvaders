using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SndObserver : ColObserver
    {
        IrrKlang.ISoundEngine pSndEngine;
        String pSndName;

        public SndObserver(IrrKlang.ISoundEngine pEng, String pSnd)
        {
            Debug.Assert(pEng != null);
            this.pSndEngine = pEng;

            Debug.Assert(pSnd != null);
            this.pSndName = pSnd;

        }
        public override void Notify()
        {
            //Debug.WriteLine(" Sound_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            pSndEngine.SoundVolume = 0.2f;
            IrrKlang.ISound pSnd = pSndEngine.Play2D(this.pSndName);
        }
    }
}
