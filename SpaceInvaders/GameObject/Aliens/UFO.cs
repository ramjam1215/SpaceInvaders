using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFO : AlienCategory
    {
        //private bool bSndToggle;
        private Bomb pBomb;

        public UFO(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY) 
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pBomb = null;
            //this.bSndToggle = true;
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitUFO(this);
        }

        //public override void VisitMissileGroup(MissileGroup m)
        //{
        //    //Debug.WriteLine("collide: {0}<->{1}", m.GetName(), this.GetName());

        //    //go to the missile object
        //    //check the missile v. "this" UFO
        //    GameObject pGameObject = (GameObject)Iterator.GetChild(m);
        //    ColPair.Collide(pGameObject, this);
        //}

        public override void VisitMissile(Missile m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            //Debug.WriteLine("-------> BOOM!  <--------");

            //add the points here.... because UFO can be removed if it hits a wall
            //this ensures that it was hit by a missile
            PlayerMan.SetP1Score(100);

            ColPair pColPair = ColPairMan.GetActiveColPair();

            //alphabetical ordering
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void VisitLeftWall(LeftWall w)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", w.GetName(), this.GetName());

            //Debug.WriteLine("-------> MissedHim!  <--------");
            ColPair pColPair = ColPairMan.GetActiveColPair();

            //might need to change when i create UFOobserver
            pColPair.SetCollision(this, w);
            pColPair.NotifyListeners();
        }

        public override void VisitRightWall(RightWall w)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", w.GetName(), this.GetName());

            //Debug.WriteLine("-------> MissedHim!  <--------");
            ColPair pColPair = ColPairMan.GetActiveColPair();

            //might need to change when i create UFOobserver
            pColPair.SetCollision(this, w);
            pColPair.NotifyListeners();
        }

        public void MoveAcross()
        {
            
            UFORoot pUFORoot = (UFORoot)Iterator.GetParent(this);
            float delta = pUFORoot.GetDeltaMove();
            this.x += delta;
        }


        public void ActivateBomb(float minX, float minY)
        {
            GameObject pBombRoot = GONodeMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            Random rNum = new Random();
            int choice = rNum.Next(1, 4);

            switch (choice)
            {
                case 1:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombStraight, new FallStraight(), this, minX, minY);
                    break;

                case 2:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombDagger, new FallDagger(), this, minX, minY);
                    break;

                case 3:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombZigZag, new FallZigZag(), this, minX, minY);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            pBombRoot.Add(this.pBomb);

            SpriteBatch pSB_Projectiles = SpriteBatchMan.Find(SpriteBatch.Name.Projectiles);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            this.pBomb.ActivateGameSprite(pSB_Projectiles);
            this.pBomb.ActivateCollisionSprite(pSB_Boxes);


        }

        public override void Update()
        {
            MoveAcross();
            base.Update();
        }
    }
}
