using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    public abstract class Animation : Command
    {
        public enum Name
        {
            AnimateCrab,
            AnimateOcto,
            AnimateSquid,

            NullAnimation,

            Uninitialized
        }

        private Name name;

        public Animation(Animation.Name animName)
        {
            this.name = animName;
        }

        public Name GetName()
        {
            return this.name;
        }

        public void SetName(Animation.Name name)
        {
            this.name = name;
        }

    }
}
