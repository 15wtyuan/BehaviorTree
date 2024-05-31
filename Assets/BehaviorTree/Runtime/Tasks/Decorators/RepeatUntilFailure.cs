namespace BehaviorTree.Runtime
{
    [TaskIcon("EventBusy.png")]
    public class RepeatUntilFailure : DecoratorBase
    {
        protected override TaskStatus OnUpdate()
        {
            if (Child.Update() == TaskStatus.Failure)
            {
                return TaskStatus.Failure;
            }

            return TaskStatus.Continue;
        }
    }
}