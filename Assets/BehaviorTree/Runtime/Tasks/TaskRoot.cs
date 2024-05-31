﻿namespace BehaviorTree.Runtime
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

        public override void End()
        {
        }

        protected override void Reset()
        {
        }
    }
}