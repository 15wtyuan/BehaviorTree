using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder ParallelSelector(this BehaviorTreeBuilder builder,
            string name = "Parallel Selector")
        {
            return builder.ParentTask<ParallelSelector>(name);
        }
    }

    public class ParallelSelector : CompositeBase, IJsonDeserializer
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

        protected override void OnReset()
        {
            _childStatus.Clear();
        }

        protected override void OnExit()
        {
            foreach (var child in Children)
            {
                child.End();
            }
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}