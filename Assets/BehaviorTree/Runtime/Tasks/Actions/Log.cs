using System.Collections.Generic;
using System.Diagnostics;

namespace BehaviorTree.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder Log(this BehaviorTreeBuilder builder, string name,
            SharedString text)
        {
            return builder.AddNode(new Log
            {
                Name = name,
                Text = text
            });
        }

        public static BehaviorTreeBuilder Log(this BehaviorTreeBuilder builder,
            SharedString text)
        {
            return builder.Log("Log", text);
        }
    }

    public class Log : ActionBase, IJsonDeserializer
    {
        public SharedString Text;

        protected override TaskStatus OnUpdate()
        {
            UnityEngine.Debug.Log(Text.Value);
            return TaskStatus.Success;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];

            var properties = jsonData["properties"] as Dictionary<string, object>;
            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("text", out var value))
            {
                var strValue = (string)value;
                Text = strValue;
            }
            else if (properties.TryGetValue("b_text", out value))
            {
                var strValue = (string)value;
                Text = SharedBlackboard.Get<SharedString>(strValue);
            }
        }
    }
}