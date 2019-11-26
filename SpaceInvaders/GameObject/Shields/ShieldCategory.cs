using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShieldCategory : Leaf
    {
        public enum Type
        {
            Root,
            Column,
            Brick,
            Grid,

            LeftTop1,
            LeftTop2,
            LeftBottom,
            RightTop1,
            RightTop2,
            RightBottom,

            Unitialized
        }

        protected ShieldCategory.Type type;

        protected ShieldCategory(GameObject.Name name, GameSprite.Name spriteName, ShieldCategory.Type shieldType)
            : base(name, spriteName)
        {
            this.type = shieldType;
        }

        ~ShieldCategory()
        {
        }

        public ShieldCategory.Type GetCategoryType()
        {
            return this.type;
        }
    }
}
