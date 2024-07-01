using System.Collections.Generic;
using System.Diagnostics;

namespace BT.Runtime
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
            SelfBlackboard.AddObserver(EventType.Value, this);
        }

        protected override void OnExit()
        {
            SelfBlackboard.RemoveObserver(EventType.Value, this);
        }

        protected override void OnReset()
        {
            _receivedEvent = false;
        }

        public void OnNotify(string eventType)
        {
            _receivedEvent = true;
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
                var key = MiniJsonHelper.ParseString(value);
                if (SelfBlackboard.ContainsKey(key))
                {
                    EventType = SelfBlackboard.Get<SharedString>(key);
                }
                else
                {
                    EventType = "";
                    SelfBlackboard.Set(key, EventType);
                }
            }
        }
    }
}