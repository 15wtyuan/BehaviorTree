using System.Collections.Generic;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder RepeatForever(this BehaviorTreeBuilder builder,
            string name = "Repeat Forever")
        {
            return builder.ParentTask<RepeatForever>(name);
        }
    }

    [TaskIcon("Repeat.png")]
    public class RepeatForever : DecoratorBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            Child.Update();
            return TaskStatus.Continue;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}