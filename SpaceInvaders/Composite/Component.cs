using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    public abstract class Component : ColVisitor
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            Unknown
        }

        public Component pParent = null;
        public Component pReverse = null;
        public Container holder = Container.Unknown;
        

        public abstract void Add(Component comp);

        public abstract void Remove(Component comp);

        public abstract void Print();

        public abstract Component GetFirstChild();

        public abstract void DumpNode();

        
    }
}
