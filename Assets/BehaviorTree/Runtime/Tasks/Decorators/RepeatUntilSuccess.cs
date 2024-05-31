namespace BehaviorTree.Runtime
{
    [TaskIcon("EventAvailable.png")]
    public class RepeatUntilSuccess : DecoratorBase
    {
        protected override TaskStatus OnUpdate()
        {
            if (Child.Update() == TaskStatus.Success)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Continue;
        }
    }
}