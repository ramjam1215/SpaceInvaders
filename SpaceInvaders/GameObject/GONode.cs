using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GONode : DLink
    {
        public GameObject poGameObject;

        public GONode()
            : base()
        {
            this.poGameObject = null;
        }

        ~GONode()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~GONode():{0} ", this.GetHashCode());
#endif
            this.poGameObject = null;
        }

        public void Set(GameObject pGObject)
        {
            Debug.Assert(pGObject != null);
            this.poGameObject = pGObject;
        }

        
        public new void Clear()
        {
            this.poGameObject = null;
            base.Clear();
        }

        public void DumpGONode()
        {
            
            Debug.WriteLine("GO_Node: {0}", this.GetHashCode() );

            if(this.poGameObject != null)
            {
                this.poGameObject.DumpNode();
            }

            else
            {
                Debug.WriteLine("GameObject: null");
            }
            
        }
    }
}
