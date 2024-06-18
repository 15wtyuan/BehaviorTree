using System.Collections.Generic;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder ReturnFailure(this BehaviorTreeBuilder builder,
            string name = "Return Failure")
        {
            return builder.ParentTask<ReturnFailure>(name);
        }
    }

    [TaskIcon("Cancel.png")]
    public class ReturnFailure : DecoratorBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            var status = Child.Update();
            if (status == TaskStatus.Continue)
            {
                return status;
            }

            return TaskStatus.Failure;
        }

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;
        }
    }
}