using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class MoveShipState
    {
        public abstract void Handle(Ship pShip);

        public abstract void MoveRight(Ship pShip);

        public abstract void MoveLeft(Ship pShip);
    }
}
