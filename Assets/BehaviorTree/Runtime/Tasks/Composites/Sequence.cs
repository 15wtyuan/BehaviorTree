﻿namespace BehaviorTree.Runtime
{
    [TaskIcon("RightArrow.png")]
    public class Sequence : CompositeBase
    {
        protected override TaskStatus OnUpdate()
        {
            for (var i = ChildIndex; i < Children.Count; i++)
            {
                var child = Children[ChildIndex];

                var status = child.Update();
                if (status != TaskStatus.Success)
                {
                    return status;
                }

                ChildIndex++;
            }

            return TaskStatus.Success;
        }
    }
}