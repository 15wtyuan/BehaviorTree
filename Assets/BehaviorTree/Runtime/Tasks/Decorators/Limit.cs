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

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];

            var properties = jsonData["properties"] as Dictionary<string, object>;
            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("maxLoop", out var value))
            {
                var intValue = MiniJsonHelper.ParseInt(value);
                MaxLoop = intValue;
            }
            else if (properties.TryGetValue("b_maxLoop", out value))
            {
                var strValue = MiniJsonHelper.ParseString(value);
                MaxLoop = SelfBlackboard.Get<SharedInt>(strValue);
            }
        }
    }
}