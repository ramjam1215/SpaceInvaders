using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatAnim : Command
    {
        public GameSprite pSplat;
        public SpriteBatch pBatch;

        public SplatAnim(GameSprite splat, SpriteBatch pBatch) 
        {
            Debug.Assert(splat != null);
            this.pSplat = splat;

            this.pBatch = pBatch;
        }

        public override void Execute(float deltaTime)
        {
            // remove from spriteBatch.......
            this.pBatch.Dettach(this.pSplat);
        }
    }
}
