using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipMan
    {
        public enum ShootState
        {
            Ready,
            MissileFlying,
            End
        }

        public enum MoveState
        {
            NoLeft,
            NoRight,
            FreeMove,
            NoMove
        }

        private static ShipMan instance = null;

        private Ship poShip;
        private Missile pMissile;
        private float lives;

        private ShipStateReady pStateReady;
        private ShipStateMissileFlying pStateMissileFlying;
        private ShipStateEnd pStateEnd;

        private NoLeftState pNoLeftState;
        private NoRightState pNoRightState;
        private FreeMoveState pFreeMoveState;
        private NoMoveState pNoMoveState;



        private ShipMan()
        {
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipStateMissileFlying();
            this.pStateEnd = new ShipStateEnd();

            this.pNoLeftState = new NoLeftState();
            this.pNoRightState = new NoRightState();
            this.pFreeMoveState = new FreeMoveState();
            this.pNoMoveState = new NoMoveState();
            

            this.poShip = null;
            this.pMissile = null;
            this.lives = 3;

        }

        public static void Create()
        {
            Debug.Assert(instance == null);

            if(instance == null)
            {
                instance = new ShipMan();
            }

            Debug.Assert(instance != null);

            instance.poShip = ActivateShip();
            instance.poShip.SetShootState(ShipMan.ShootState.Ready);
            instance.poShip.SetMoveState(ShipMan.MoveState.FreeMove);
        } 

        public static void Destroy()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("ShipMan.Destroy()");
#endif
            

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", ShipMan.instance, ShipMan.instance.GetHashCode());

#endif
            pShipMan.pStateReady = null;
            pShipMan.pStateMissileFlying = null;
            pShipMan.pStateEnd = null;

            pShipMan.pNoLeftState = null;
            pShipMan.pNoRightState = null;
            pShipMan.pFreeMoveState = null;

            pShipMan.poShip = null;
            pShipMan.pMissile = null;

            ShipMan.instance = null;
        }

        public static Ship GetShip()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.poShip != null);

            return pShipMan.poShip;
        }

        public static Missile GetMissile()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();

            Debug.Assert(pShipMan != null);

            return pShipMan.pMissile;
        }

        public static ShipShootState GetShootState(ShootState state)
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            ShipShootState pShipState = null;

            switch (state)
            {
                case ShipMan.ShootState.Ready:
                    pShipState = pShipMan.pStateReady;
                    break;

                case ShipMan.ShootState.MissileFlying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                case ShipMan.ShootState.End:
                    pShipState = pShipMan.pStateEnd;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static MoveShipState GetMoveState(MoveState state)
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            MoveShipState pMoveState = null;

            switch (state)
            {
                case ShipMan.MoveState.NoLeft:
                    pMoveState = pShipMan.pNoLeftState;
                    break;

                case ShipMan.MoveState.FreeMove:
                    pMoveState = pShipMan.pFreeMoveState;
                    break;

                case ShipMan.MoveState.NoRight:
                    pMoveState = pShipMan.pNoRightState;
                    break;

                case ShipMan.MoveState.NoMove:
                    pMoveState = pShipMan.pNoMoveState;
                    break;

                default:
                    Debug.Assert(false);
                    break;

            }

            return pMoveState;
        }

        public static Missile ActivateMissile()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.Missile, 10, 10);

            //attach missile to missile group(should be missileRoot)
            GameObject pMissileGroup = GONodeMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            pMissileGroup.Add(pShipMan.pMissile);

            SpriteBatch pSB_Projectiles = SpriteBatchMan.Find(SpriteBatch.Name.Projectiles);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            pShipMan.pMissile.ActivateGameSprite(pSB_Projectiles);
            pShipMan.pMissile.ActivateCollisionSprite(pSB_Boxes);


            return pShipMan.pMissile;
        }

        //similar to Activate Missile
        public static Ship ActivateShip()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            Ship pShip = new Ship(GameObject.Name.Ship, GameSprite.Name.Ship, 200, 60);
            pShipMan.poShip = pShip;

            GameObject pShipRoot = GONodeMan.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            pShipRoot.Add(pShipMan.poShip);

            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            pShipMan.poShip.ActivateGameSprite(pSB_Aliens);
            pShipMan.poShip.ActivateCollisionSprite(pSB_Boxes);

            pShipMan.TakeLife(1);
            return pShipMan.poShip;
        }


        private static ShipMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public void TakeLife(float kill)
        {
            this.lives -= kill;
        }

        //public static float GetLives()
        //{
        //    ShipMan pShipMan = ShipMan.PrivInstance();
        //    Debug.Assert(pShipMan != null);

        //    return pShipMan.lives;
        //}

        public static void DestroyShip()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.poShip = null;
        }

    }
}
