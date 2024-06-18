using System.Collections.Generic;

namespace BT.Runtime
{
    public class Succeeder : ActionBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;
        }
    }
}