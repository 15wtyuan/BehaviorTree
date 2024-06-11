using System.Collections.Generic;

namespace BT.Runtime
{
    public interface IJsonDeserializer
    {
        void BuildFromJson(Dictionary<string, object> jsonData);
    }
}