namespace BehaviorTree.Runtime
{
    [TaskIcon("LinearScale.png")]
    public class Selector : CompositeBase
    {
        protected override TaskStatus OnUpdate()
        {
            for (var i = ChildIndex; i < Children.Count; i++)
            {
                var child = Children[ChildIndex];

                var status = child.Update();
                if (status != TaskStatus.Failure)
                {
                    return status;
                }

                ChildIndex++;
            }

            return TaskStatus.Failure;
        }
    }
}