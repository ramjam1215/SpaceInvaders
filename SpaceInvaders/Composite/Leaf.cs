using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Leaf : GameObject
    {

        public Leaf(GameObject.Name name, GameSprite.Name spriteName) 
            : base(name, spriteName)
        {
            this.holder = Container.LEAF;
        }

        public override void Add(Component comp)
        {
            Debug.Assert(false);
        }

        public override void Print()
        {
            Debug.WriteLine("Leaf GO: {0}, ({1})", this.GetName(), this.GetHashCode());
        }

        public override void DumpNode()
        {
            Debug.WriteLine("Leaf GO: {0}, ({1})", this.GetName(), this.GetHashCode());
        }

        public override void Remove(Component comp)
        {
            Debug.Assert(false);
        }

        public override Component GetFirstChild()
        {
            Debug.Assert(false);
            return null;
        }

    }
}
