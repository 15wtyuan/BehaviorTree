﻿using System;
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
                Text = SelfBlackboard.Get<SharedString>(MiniJsonHelper.ParseString(value));
            }
        }
    }
}