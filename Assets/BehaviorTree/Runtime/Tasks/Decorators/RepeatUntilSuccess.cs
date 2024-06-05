using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder RepeatUntilSuccess(this BehaviorTreeBuilder builder,
            string name = "Repeat Until Success")
        {
            return builder.ParentTask<RepeatUntilSuccess>(name);
        }
    }

    [TaskIcon("EventAvailable.png")]
    public class RepeatUntilSuccess : DecoratorBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            if (Child.Update() == TaskStatus.Success)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Continue;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}