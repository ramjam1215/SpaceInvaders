using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatObject : Leaf
    {
        public SplatObject(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

        }

        public override void Update()
        {
            base.Update();
        }
        public override void Accept(ColVisitor other)
        {
            throw new NotImplementedException();
        }
    }
}
