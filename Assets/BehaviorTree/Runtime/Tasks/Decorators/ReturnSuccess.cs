namespace BehaviorTree.Runtime
{
    [TaskIcon("Checkmark.png")]
    public class ReturnSuccess : DecoratorBase
    {
        protected override TaskStatus OnUpdate()
        {
            var status = Child.Update();
            if (status == TaskStatus.Continue)
            {
                return status;
            }

            return TaskStatus.Success;
        }
    }
}