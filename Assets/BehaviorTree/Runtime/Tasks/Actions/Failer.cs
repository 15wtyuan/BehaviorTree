using System.Collections.Generic;

namespace BT.Runtime
{
    public class Failer : ActionBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            return TaskStatus.Failure;
        }

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;
        }
    }
}