using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BT.Runtime
{
    public class Error : ActionBase, IJsonDeserializer
    {
        public SharedString Text;

        protected override TaskStatus OnUpdate()
        {
            throw new Exception(Text.Value);
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];

            var properties = jsonData["properties"] as Dictionary<string, object>;
            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("text", out var value))
            {
                var strValue = MiniJsonHelper.ParseString(value);
                Text = strValue;
            }
            else if (properties.TryGetValue("b_text", out value))
            {
                var strValue = MiniJsonHelper.ParseString(value);
                Text = SelfBlackboard.Get<SharedString>(strValue);
            }
        }
    }
}