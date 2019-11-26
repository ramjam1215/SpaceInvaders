using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputManager
    {
        private static InputManager pInstance = null;
        private bool privSpaceKeyPrev;

        private InputSubject pSubjectArrowLeft;
        private InputSubject pSubjectArrowRight;
        private InputSubject pSubjectSpaceBar;

        private InputSubject pSubject_C;
        private bool privCkeyPrev;

        private InputSubject pSubject_X;
        private bool privSkeyPrev;

        private InputSubject pSubject_1;
        private bool priv1keyPrev;

        private InputSubject pSubject_2;
        private bool priv2keyPrev;

        private InputManager()
        {
            this.pSubjectArrowLeft = new InputSubject();
            this.pSubjectArrowRight = new InputSubject();
            this.pSubjectSpaceBar = new InputSubject();

            this.pSubject_C = new InputSubject();
            this.pSubject_X = new InputSubject();

            this.pSubject_1 = new InputSubject();
            this.pSubject_2 = new InputSubject();

            this.privSpaceKeyPrev = false;

            this.priv1keyPrev = false;
            this.priv2keyPrev = false;

            this.privCkeyPrev = false;
            this.privSkeyPrev = false;
        }

        public static void Destroy()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("InputMan.Destroy()");
#endif
            

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("{0} ({1})", InputManager.pInstance, InputManager.pInstance.GetHashCode());
#endif

            pMan.pSubjectArrowLeft = null;
            pMan.pSubjectArrowRight = null;
            pMan.pSubjectSpaceBar = null;

            pMan.pSubject_C = null;
            pMan.pSubject_X = null;

            pMan.pSubject_1 = null;
            pMan.pSubject_2 = null;
            InputManager.pInstance = null;
        }

        private static InputManager PrivGetInstance()
        {
            if (pInstance == null)
                pInstance = new InputManager();

            Debug.Assert(pInstance != null);

            return pInstance;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowLeft;
        }

        public static InputSubject GetSpaceBarSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectSpaceBar;
        }

        public static InputSubject GetCkeySubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubject_C;
        }

        public static InputSubject GetSkeySubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubject_X;
        }

        public static InputSubject Get1keySubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubject_1;
        }

        public static InputSubject Get2keySubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubject_2;
        }

        public static void Update()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            if(Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                pMan.pSubjectArrowLeft.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                pMan.pSubjectArrowRight.Notify();
            }

            bool SpaceBarKeyCurrent = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);
            if (SpaceBarKeyCurrent == true && pMan.privSpaceKeyPrev == false)
            {
                pMan.pSubjectSpaceBar.Notify();
            }
            pMan.privSpaceKeyPrev = SpaceBarKeyCurrent;

            //-------------------------------------------------------------------------
            //Toggle Tests
            //-------------------------------------------------------------------------

            bool CkeyCurrent = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_C);
            if(CkeyCurrent == true && pMan.privCkeyPrev == false)
            {
                pMan.pSubject_C.Notify();
            }
            pMan.privCkeyPrev = CkeyCurrent;

            bool SkeyCurrent = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_X);
            if(SkeyCurrent == true && pMan.privSkeyPrev == false)
            {
                pMan.pSubject_X.Notify();
            }
            pMan.privSkeyPrev = SkeyCurrent;

            //-------------------------------------------------------------------------
            //Intro Tests
            //-------------------------------------------------------------------------

            bool key1Current = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1);
            if(key1Current == true && pMan.priv1keyPrev == false)
            {
                pMan.pSubject_1.Notify();
            }
            pMan.priv1keyPrev = key1Current;

            bool key2Current = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2);
            if (key2Current == true && pMan.priv2keyPrev == false)
            {
                pMan.pSubject_2.Notify();
            }
            pMan.priv2keyPrev = key2Current;
        }
    }
}
