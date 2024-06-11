﻿namespace BT.Runtime
{
    [TaskIcon("DownArrow.png")]
    public class TaskRoot : TaskParentBase
    {
        protected override int MaxChildren { get; } = 1;

        protected override TaskStatus OnUpdate()
        {
            if (Children.Count == 0)
            {
                return TaskStatus.Success;
            }

            var child = Children[0];
            return child.Update();
        }
    }
}