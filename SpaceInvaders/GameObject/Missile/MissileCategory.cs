﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class MissileCategory : Leaf
    {

        public enum Type
        {
            Missile,
            MissileGroup,
            Uninitialized
        }

        public MissileCategory(GameObject.Name name, GameSprite.Name spriteName) 
            : base(name, spriteName)
        {

        }

        ~MissileCategory()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~MissileCategory():{0}", this.GetHashCode());
#endif
        }
    }
}
