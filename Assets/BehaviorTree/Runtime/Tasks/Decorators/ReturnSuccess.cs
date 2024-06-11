using System.Collections.Generic;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder ReturnSuccess(this BehaviorTreeBuilder builder,
            string name = "Return Success")
        {
            return builder.ParentTask<ReturnSuccess>(name);
        }
    }

    [TaskIcon("Checkmark.png")]
    public class ReturnSuccess : DecoratorBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            var status = Child.Update();
            if (status == TaskStatus.Continue)
            {
                return status;
            }

            return TaskStatus.Success;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}