namespace BehaviorTree.Runtime
{
    [TaskIcon("Cancel.png")]
    public class ReturnFailure : DecoratorBase
    {
        protected override TaskStatus OnUpdate()
        {
            var status = Child.Update();
            if (status == TaskStatus.Continue)
            {
                return status;
            }

            return TaskStatus.Failure;
        }
    }
}