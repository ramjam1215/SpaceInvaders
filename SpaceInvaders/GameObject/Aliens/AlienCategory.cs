using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienCategory : Leaf
    {
        public enum Type
        {
            Crab,
            Squid,
            Octopus,
            UFO,
            Group,
            Column

        }

        protected float FirstPosX;
        protected float FirstPosY; 
        

        public AlienCategory(GameObject.Name name, GameSprite.Name spriteName) 
            : base(name, spriteName)
        {
            
        }

        public float OrigX()
        {
            return this.FirstPosX;
        }

        public float OrigY()
        {
            return this.FirstPosY;
        }

    }
}
