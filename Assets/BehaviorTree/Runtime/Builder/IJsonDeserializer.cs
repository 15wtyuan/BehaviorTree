using System.Collections.Generic;

namespace BT.Runtime
{
    public interface IJsonDeserializer
    {
        void BuildFromJson(string title, Dictionary<string, object> properties);
    }
}