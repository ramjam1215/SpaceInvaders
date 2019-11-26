using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class WallCategory : Leaf
    {

        public enum Type
        {
            WallGroup,
            Right, 
            Left,
            Bottom,
            Top,
            Uninitialized
        }

        protected WallCategory.Type type;

        protected WallCategory(GameObject.Name name, GameSprite.Name spriteName, WallCategory.Type type) 
            : base(name, spriteName)
        {
            this.type = type;
        }

        ~WallCategory()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~WallCategory():{0}", this.GetHashCode());
#endif
        }

        public WallCategory.Type GetCategoryType()
        {
            return this.type;
        }
    }
}
