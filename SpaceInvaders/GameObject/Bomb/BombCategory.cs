using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BombCategory : Leaf
    {
        public enum Type
        {
            Bomb, 
            BombRoot,
            Uninitialized
        }

        protected BombCategory.Type type;

        protected BombCategory(GameObject.Name name, GameSprite.Name spriteName, BombCategory.Type bombType) 
            : base(name, spriteName)
        {
            this.type = bombType;
        }

        ~BombCategory()
        {
        }
    }
}
