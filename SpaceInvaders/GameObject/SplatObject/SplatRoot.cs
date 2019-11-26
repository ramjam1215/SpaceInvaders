using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SplatRoot : Composite
    {
        public SplatRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

        }

        public override void Update()
        {
            //does nothing, kind of like the nullGameObject
        }
        public override void Accept(ColVisitor other)
        {
            Debug.Assert(false);
        }
    }
}
