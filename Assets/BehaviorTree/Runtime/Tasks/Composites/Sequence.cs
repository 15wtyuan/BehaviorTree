using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder Sequence(this BehaviorTreeBuilder builder, string name = "Sequence")
        {
            return builder.ParentTask<Sequence>(name);
        }
    }

    [TaskIcon("RightArrow.png")]
    public class Sequence : CompositeBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            for (var i = ChildIndex; i < Children.Count; i++)
            {
                var child = Children[ChildIndex];

                var status = child.Update();
                if (status != TaskStatus.Success)
                {
                    return status;
                }

                ChildIndex++;
            }

            return TaskStatus.Success;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}