using System.Collections.Generic;
using System.Diagnostics;

namespace BT.Runtime
{
    [TaskIcon("Repeat.png")]
    public class Limit : DecoratorBase, IJsonDeserializer
    {
        public SharedInt MaxLoop;

        private int _loopTime = 0;

        protected override TaskStatus OnUpdate()
        {
            var lastTaskStatus = Child.Update();
            if (lastTaskStatus != TaskStatus.Continue)
            {
                _loopTime++;
            }

            return _loopTime < MaxLoop.Value ? TaskStatus.Continue : lastTaskStatus;
        }

        protected override void OnReset()
        {
            base.OnReset();
            _loopTime = 0;
        }

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;

            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("maxLoop", out var value))
            {
                MaxLoop = MiniJsonHelper.ParseInt(value);
            }
            else if (properties.TryGetValue("b_maxLoop", out value))
            {
                var key = MiniJsonHelper.ParseString(value);
                if (SelfBlackboard.ContainsKey(key))
                {
                    MaxLoop = SelfBlackboard.Get<SharedInt>(key);
                }
                else
                {
                    MaxLoop = 0;
                    SelfBlackboard.Set(key, MaxLoop);
                }
            }
        }
    }
}