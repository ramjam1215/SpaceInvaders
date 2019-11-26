using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class InputObserver : DLink
    {
        public InputSubject pSubject;

        abstract public void Notify();
    }
}
