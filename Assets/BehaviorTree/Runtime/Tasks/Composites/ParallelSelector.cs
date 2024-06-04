using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public class ParallelSelector : CompositeBase
    {
        private readonly Dictionary<TaskBase, TaskStatus> _childStatus = new();

        protected override TaskStatus OnUpdate()
        {
            var successCount = 0;
            var failureCount = 0;

            foreach (var child in Children)
            {
                if (_childStatus.TryGetValue(child, out var prevStatus) && prevStatus != TaskStatus.Continue)
                {
                    switch (prevStatus)
                    {
                        case TaskStatus.Failure:
                            failureCount++;
                            break;
                        case TaskStatus.Success:
                            successCount++;
                            break;
                    }

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

            if (failureCount == Children.Count)
            {
                End();
                return TaskStatus.Failure;
            }

            if (successCount > 0)
            {
                End();
                return TaskStatus.Success;
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