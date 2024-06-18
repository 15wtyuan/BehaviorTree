using System.Collections.Generic;
using System.Diagnostics;

namespace BT.Runtime
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
            SelfBlackboard.SendEvent(EventType.Value);
            return TaskStatus.Success;
        }

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;

            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("eventType", out var value))
            {
                EventType = MiniJsonHelper.ParseString(value);
            }
            else if (properties.TryGetValue("b_eventType", out value))
            {
                EventType = SelfBlackboard.Get<SharedString>(MiniJsonHelper.ParseString(value));
            }
        }
    }
}