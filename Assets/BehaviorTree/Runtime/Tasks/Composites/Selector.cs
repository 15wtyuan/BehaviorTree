using System.Collections.Generic;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder Selector(this BehaviorTreeBuilder builder, string name = "Selector")
        {
            return builder.ParentTask<Selector>(name);
        }
    }

    [TaskIcon("LinearScale.png")]
    public class Selector : CompositeBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            for (var i = ChildIndex; i < Children.Count; i++)
            {
                var child = Children[ChildIndex];

                var status = child.Update();
                if (status != TaskStatus.Failure)
                {
                    return status;
                }

                ChildIndex++;
            }

            return TaskStatus.Failure;
        }

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;
        }
    }
}