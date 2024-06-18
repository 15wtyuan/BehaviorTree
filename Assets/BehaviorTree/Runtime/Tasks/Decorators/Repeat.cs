using System.Collections.Generic;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder RepeatForever(this BehaviorTreeBuilder builder,
            string name = "Repeat Forever")
        {
            return builder.ParentTask<Repeat>(name);
        }
    }

    [TaskIcon("Repeat.png")]
    public class Repeat : DecoratorBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            Child.Update();
            return TaskStatus.Continue;
        }

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;
        }
    }
}