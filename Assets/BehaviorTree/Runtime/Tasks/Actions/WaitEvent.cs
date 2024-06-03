namespace BehaviorTree.Runtime
{
    public class WaitEvent : ActionBase, IEventObserver
    {
        public SharedString EventType;

        private bool _receivedEvent;

        protected override TaskStatus OnUpdate()
        {
            return _receivedEvent ? TaskStatus.Success : TaskStatus.Continue;
        }

        protected override void OnStart()
        {
            SharedBlackboard.AddObserver(EventType.Value, this);
        }

        protected override void OnExit()
        {
            SharedBlackboard.RemoveObserver(EventType.Value, this);
        }

        protected override void OnReset()
        {
            _receivedEvent = false;
        }

        public void OnNotify(string eventType)
        {
            _receivedEvent = true;
        }
    }
}