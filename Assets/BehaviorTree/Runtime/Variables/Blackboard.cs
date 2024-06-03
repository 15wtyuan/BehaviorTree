using System;
using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public class Blackboard
    {
        private Dictionary<string, SharedVariable> _data = new();

        public void Set<T>(string key, object value) where T : SharedVariable
        {
            if (_data.TryGetValue(key, out var oldValue))
            {
                oldValue.SetValue(value);
            }
            else
            {
                var newValue = (T)value;
                _data[key] = newValue;
            }
        }

        public T Get<T>(string key) where T : SharedVariable, new()
        {
            if (_data.TryGetValue(key, out var value) && value is T sharedVariable)
            {
                return sharedVariable;
            }

            throw new ArgumentNullException($"key:{key} not found");
        }
    }
}