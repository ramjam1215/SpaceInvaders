using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShipCategory : Leaf
    {
        public enum Type
        {
            Ship,
            ShipRoot,
            Uninitialized
        }

        protected ShipCategory.Type type;

        protected ShipCategory(GameObject.Name name, GameSprite.Name spriteName, ShipCategory.Type shipType) 
            : base(name, spriteName)
        {
            this.type = shipType;
        }


    }
}
