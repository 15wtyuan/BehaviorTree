using System.Collections.Generic;
using System.Diagnostics;

namespace BehaviorTree.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder WaitEvent(this BehaviorTreeBuilder builder, string name,
            SharedString eventType)
        {
            return builder.AddNode(new WaitEvent
            {
                Name = name,
                EventType = eventType
            });
        }

        public static BehaviorTreeBuilder WaitEvent(this BehaviorTreeBuilder builder,
            SharedString eventType)
        {
            return builder.WaitEvent("Wait Event", eventType);
        }
    }

    public class WaitEvent : ActionBase, IEventObserver, IJsonDeserializer
    {
        public SharedString EventType;

        private bool _receivedEvent;

        protected override TaskStatus OnUpdate()
        {
            return _receivedEvent ? TaskStatus.Success : TaskStatus.Continue;
        }

        protected override void OnStart()
        {
            SharedBlackboard.AddObserver(EventType.Value, this);
        }

        protected override void OnExit()
        {
            SharedBlackboard.RemoveObserver(EventType.Value, this);
        }

        protected override void OnReset()
        {
            _receivedEvent = false;
        }

        public void OnNotify(string eventType)
        {
            _receivedEvent = true;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];

            var properties = jsonData["properties"] as Dictionary<string, object>;
            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("eventType", out var value))
            {
                var strValue = (string)value;
                EventType = strValue;
            }
            else if (properties.TryGetValue("b_eventType", out value))
            {
                var strValue = (string)value;
                EventType = SharedBlackboard.Get<SharedString>(strValue);
            }
        }
    }
}