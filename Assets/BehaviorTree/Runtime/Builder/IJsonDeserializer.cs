using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public interface IJsonDeserializer
    {
        void BuildFromJson(Dictionary<string, object> jsonData);
    }
}