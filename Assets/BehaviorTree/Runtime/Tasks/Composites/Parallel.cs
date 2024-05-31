﻿using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    [TaskIcon("CompareArrows.png")]
    public class Parallel : CompositeBase
    {
        private readonly Dictionary<TaskBase, TaskStatus> _childStatus = new();

        protected override TaskStatus OnUpdate()
        {
            var successCount = 0;
            var failureCount = 0;

            foreach (var child in Children)
            {
                if (_childStatus.TryGetValue(child, out var prevStatus) && prevStatus == TaskStatus.Success)
                {
                    successCount++;
                    continue;
                }

                var status = child.Update();
                _childStatus[child] = status;

                switch (status)
                {
                    case TaskStatus.Failure:
                        failureCount++;
                        break;
                    case TaskStatus.Success:
                        successCount++;
                        break;
                }
            }

            if (successCount == Children.Count)
            {
                End();
                return TaskStatus.Success;
            }

            if (failureCount > 0)
            {
                End();
                return TaskStatus.Failure;
            }

            return TaskStatus.Continue;
        }

        protected override void Reset()
        {
            _childStatus.Clear();

            base.Reset();
        }

        public override void End()
        {
            foreach (var child in Children)
            {
                child.End();
            }
        }
    }
}