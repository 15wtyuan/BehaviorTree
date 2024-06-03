namespace BehaviorTree.Runtime
{
    public class SendEvent : ActionBase
    {
        public SharedString EventType;

        protected override TaskStatus OnUpdate()
        {
            SharedBlackboard.SendEvent(EventType.Value);
            return TaskStatus.Success;
        }
    }
}