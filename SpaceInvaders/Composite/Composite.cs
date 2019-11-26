using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Composite : GameObject
    {
        private DLink poHead;
        private DLink poTail;

        public Composite(GameObject.Name name, GameSprite.Name spriteName) 
            : base(name, spriteName)
        {
            this.poHead = null;
            this.poTail = null;
            this.holder = Container.COMPOSITE;
        }

        public override void Add(Component pComp)
        {
            Debug.Assert(pComp != null);
            DLink.AddToEnd(ref this.poHead, ref this.poTail, pComp);

            pComp.pParent = this;
        }

        public override void Remove(Component pComp)
        {
            Debug.Assert(pComp != null);
            //DLink.RemoveNode(ref this.poHead, pComp);
            DLink.RemoveNode(ref this.poHead, ref this.poTail ,pComp);
            
        }

        public override Component GetFirstChild()
        {
            DLink pNode = this.poHead;

            //missed this, sometimes this returns null,
            //aka a composite without a child
            //Debug.Assert(pNode != null);

            return (Component)pNode;
        }

        public override void Print()
        {
            Debug.WriteLine("Composite GO: {0} ({1})", this.GetName() , this.GetHashCode());

            DLink pSafety = this.poHead;

            DLink pNode = pSafety;

            while (pNode != null)
            {
                Component pComp = (Component)pNode;
                pComp.Print();

                pNode = pNode.pNext;
            }
        }

        public new void Clear()
        {
            this.poHead = null;
            this.poTail = null;
            base.Clear();
        }

        public override void DumpNode()
        {
            Debug.WriteLine("Composite GO: ({0})", this.GetHashCode() );
        }
    }
}

