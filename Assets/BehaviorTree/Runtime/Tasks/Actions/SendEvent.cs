using System.Collections.Generic;
using System.Diagnostics;

namespace BehaviorTree.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder SendEvent(this BehaviorTreeBuilder builder, string name,
            SharedString eventType)
        {
            return builder.AddNode(new SendEvent
            {
                Name = name,
                EventType = eventType
            });
        }

        public static BehaviorTreeBuilder SendEvent(this BehaviorTreeBuilder builder,
            SharedString eventType)
        {
            return builder.SendEvent("Send Event", eventType);
        }
    }

    public class SendEvent : ActionBase, IJsonDeserializer
    {
        public SharedString EventType;

        protected override TaskStatus OnUpdate()
        {
            SharedBlackboard.SendEvent(EventType.Value);
            return TaskStatus.Success;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];

            var properties = jsonData["properties"] as Dictionary<string, object>;
            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("eventType", out var value))
            {
                var strValue = MiniJsonHelper.ParseString(value);
                EventType = strValue;
            }
            else if (properties.TryGetValue("b_eventType", out value))
            {
                var strValue = MiniJsonHelper.ParseString(value);
                EventType = SharedBlackboard.Get<SharedString>(strValue);
            }
        }
    }
}