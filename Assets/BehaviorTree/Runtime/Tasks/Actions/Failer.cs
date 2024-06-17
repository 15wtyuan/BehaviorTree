using System.Collections.Generic;

namespace BT.Runtime
{
    public class Failer : ActionBase, IJsonDeserializer
    {
        protected override TaskStatus OnUpdate()
        {
            return TaskStatus.Failure;
        }

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];
        }
    }
}