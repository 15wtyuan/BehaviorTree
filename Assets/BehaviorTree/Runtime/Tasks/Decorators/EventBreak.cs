namespace BehaviorTree.Runtime
{
    public class EventBreak : DecoratorBase, IEventObserver
    {
        public SharedString EventType;

        private bool _receivedEvent;

        protected override TaskStatus OnUpdate()
        {
            var childStatus = Child.Update();
            if (_receivedEvent && childStatus == TaskStatus.Continue)
            {
                Child.End();
                return TaskStatus.Failure;
            }

            return childStatus;
        }
        
        protected override void OnStart()
        {
            base.OnStart();
            SharedBlackboard.AddObserver(EventType.Value, this);
        }

        protected override void OnExit()
        {
            base.OnExit();
            SharedBlackboard.RemoveObserver(EventType.Value, this);
        }

        protected override void OnReset()
        {
            base.OnReset();
            _receivedEvent = false;
        }

        public void OnNotify(string eventType)
        {
            _receivedEvent = true;
        }
    }
}