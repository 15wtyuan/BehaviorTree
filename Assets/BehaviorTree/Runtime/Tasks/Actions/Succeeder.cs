using System.Collections.Generic;

namespace BT.Runtime
{
    public class Succeeder : ActionBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}