using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TimeEvent_Link : DLink
    {

    }
    class TimeEvent : TimeEvent_Link
    {
        public enum Name
        {
            AnimateCrab,
            AnimateSquid,
            AnimateOcto,

            MoveGrid,
            ColumnShoot,
            SendUFO,
            SpeedCheck,
            FixShip,
            GameStateChange,

            PlaySound,
            UFOBomb,

            SplatAnim,

            Unitialized
        }

        public Name name;
        public Command pCommand;
        public float trigger;
        public float deltaTime;

        public TimeEvent()
            : base()
        {
            this.Clear();
        }

        ~TimeEvent()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~TimeEvent():{0} ", this.GetHashCode());
#endif
            this.name = Name.Unitialized;
            this.pCommand = null;
        }

        public new void Clear()
        {
            this.name = TimeEvent.Name.Unitialized;
            this.pCommand = null;
            this.trigger = 0.0f;
            this.deltaTime = 0.0f;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }

        public Name GetName()
        {
            return this.name;
        }

        public void Set(TimeEvent.Name eventName, Command pCommand, float deltaTimeToTrigger)
        {
            Debug.Assert(pCommand != null);

            this.name = eventName;
            this.pCommand = pCommand;
            this.deltaTime = deltaTimeToTrigger;

            this.trigger = TimerMan.GetCurrentTime() + deltaTimeToTrigger;
        }

        public void SetTriggerTime(float TriggerTime)
        {
            this.deltaTime = TriggerTime;
        }

        public void Process()
        {
            Debug.Assert(this.pCommand != null);

            this.pCommand.Execute(this.deltaTime);
        }

        public void DumpTimeEvent()
        {
            Debug.WriteLine("Name: {0}, {1}", this.name, this.GetHashCode());
            Debug.WriteLine("Trigger: {0}, deltaTime: {1}", this.trigger, this.deltaTime);

            if (pCommand == null)
            {
                Debug.WriteLine("Command: null");
            }

            else
            {
                Debug.WriteLine("Command: #{0}", this.pCommand.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("previous: null");
            }
            else
            {
                TimeEvent pTemp = (TimeEvent)this.pPrev;
                Debug.WriteLine("previous: {0}, {1}", pTemp.name, pTemp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("next: null");
            }
            else
            {
                TimeEvent pTemp = (TimeEvent)this.pNext;
                Debug.WriteLine("next: {0}, {1}", pTemp.name, pTemp.GetHashCode());

            }

        }

    }
}
