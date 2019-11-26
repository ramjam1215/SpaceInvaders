using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class NullAnim : Animation
    {

        public NullAnim()
            : base(Animation.Name.NullAnimation)
        {

        }

        ~NullAnim()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~NULLAnim():{0} ", this.GetHashCode());
#endif
        }

        public override void Execute(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}
