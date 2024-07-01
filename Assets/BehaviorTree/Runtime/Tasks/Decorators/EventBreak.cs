using System.Collections.Generic;
using System.Diagnostics;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder EventBreak(this BehaviorTreeBuilder builder,
            SharedString eventType)
        {
            return builder.EventBreak("Event Break", eventType);
        }

        public static BehaviorTreeBuilder EventBreak(this BehaviorTreeBuilder builder,
            string name, SharedString eventType)
        {
            var eventBreak = new EventBreak
            {
                Name = name,
                EventType = eventType
            };

            return builder.AddNodeWithPointer(eventBreak);
        }
    }

    public class EventBreak : DecoratorBase, IEventObserver, IJsonDeserializer
    {
        public SharedString EventType;

        private bool _receivedEvent;

        protected override TaskStatus OnUpdate()
        {
            var childStatus = Child.Update();
            if (_receivedEvent && childStatus == TaskStatus.Continue)
            {
                Child.End();
                return TaskStatus.Failure;
            }

            return childStatus;
        }

        protected override void OnStart()
        {
            base.OnStart();
            SelfBlackboard.AddObserver(EventType.Value, this);
        }

        protected override void OnExit()
        {
            base.OnExit();
            SelfBlackboard.RemoveObserver(EventType.Value, this);
        }

        protected override void OnReset()
        {
            base.OnReset();
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