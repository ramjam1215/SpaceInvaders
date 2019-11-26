using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ColObserver : DLink
    {
        public ColSubject pSubject;
        abstract public void Notify();

        virtual public void Execute()
        {
            //the default
        }
    }
}
