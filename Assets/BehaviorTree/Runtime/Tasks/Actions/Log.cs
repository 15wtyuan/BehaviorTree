using System.Collections.Generic;
using System.Diagnostics;

namespace BT.Runtime
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

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;

            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("text", out var value))
            {
                Text = MiniJsonHelper.ParseString(value);
            }
            else if (properties.TryGetValue("b_text", out value))
            {
                var key = MiniJsonHelper.ParseString(value);
                if (SelfBlackboard.ContainsKey(key))
                {
                    Text = SelfBlackboard.Get<SharedString>(key);
                }
                else
                {
                    Text = "";
                    SelfBlackboard.Set(key, Text);
                }
            }
        }
    }
}