using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class NullGO : Leaf
    {
        public NullGO()
            : base(GameObject.Name.Null_Object, GameSprite.Name.NullObject)
        {
            
        }

        ~NullGO()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~NULLGO():{0} ", this.GetHashCode());
#endif
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitNullGameObject(this);
        }

        public override void Update()
        {
            //override the virtual method
            //does nothing
        }
    }
}
