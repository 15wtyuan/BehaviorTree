using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder RepeatUntilFailure(this BehaviorTreeBuilder builder,
            string name = "Repeat Until Failure")
        {
            return builder.ParentTask<RepeatUntilFailure>(name);
        }
    }

    [TaskIcon("EventBusy.png")]
    public class RepeatUntilFailure : DecoratorBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            if (Child.Update() == TaskStatus.Failure)
            {
                return TaskStatus.Failure;
            }

            return TaskStatus.Continue;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}