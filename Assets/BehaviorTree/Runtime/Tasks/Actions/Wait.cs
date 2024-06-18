using System.Collections.Generic;
using System.Diagnostics;

namespace BT.Runtime
{
    public interface ITimeMonitor
    {
        int DeltaMillisecondsTime { get; }
    }

    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder Wait(this BehaviorTreeBuilder builder, string name,
            SharedInt millisecondsTime)
        {
            return builder.AddNode(new Wait
            {
                Name = name,
                Milliseconds = millisecondsTime
            });
        }

        public static BehaviorTreeBuilder Wait(this BehaviorTreeBuilder builder,
            SharedInt millisecondsTime)
        {
            return builder.Wait("Wait Time", millisecondsTime);
        }
    }

    [TaskIcon("Hourglass.png")]
    public class Wait : ActionBase, IJsonDeserializer
    {
        public SharedInt Milliseconds;
        private readonly ITimeMonitor _timeMonitor;
        private int _timePassed;

        public Wait()
        {
            _timeMonitor = new TimeMonitor();
        }

        public Wait(ITimeMonitor timeMonitor)
        {
            _timeMonitor = timeMonitor;
        }

        protected override void OnStart()
        {
            _timePassed = 0;
        }

        protected override TaskStatus OnUpdate()
        {
            _timePassed += _timeMonitor.DeltaMillisecondsTime;

            if (_timePassed < Milliseconds.Value)
            {
                return TaskStatus.Continue;
            }

            return TaskStatus.Success;
        }

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;

            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("milliseconds", out var value))
            {
                Milliseconds = MiniJsonHelper.ParseInt(value);
                Name = Name.Replace("<milliseconds>", $"{Milliseconds.Value}");
            }
            else if (properties.TryGetValue("b_milliseconds", out value))
            {
                Milliseconds = SelfBlackboard.Get<SharedInt>(MiniJsonHelper.ParseString(value));
            }
        }
    }
}