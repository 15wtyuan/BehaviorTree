using System.Collections.Generic;
using System.Diagnostics;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder Inverter(this BehaviorTreeBuilder builder, string name = "Inverter")
        {
            return builder.ParentTask<Inverter>(name);
        }
    }

    [TaskIcon("Invert.png")]
    public class Inverter : DecoratorBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            var childStatus = Child.Update();
            var status = childStatus;

            switch (childStatus)
            {
                case TaskStatus.Success:
                    status = TaskStatus.Failure;
                    break;
                case TaskStatus.Failure:
                    status = TaskStatus.Success;
                    break;
            }

            return status;
        }
        
        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}