using System;
using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public class Blackboard
    {
        private readonly Dictionary<string, SharedVariable> _data = new();

        private readonly Dictionary<string, EventSubject> _events = new();

        public void Set<T>(string key, T value) where T : SharedVariable
        {
            if (_data.TryGetValue(key, out var oldValue))
            {
                oldValue.SetValue(value);
            }
            else
            {
                _data[key] = value;
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

        public void AddObserver(string eventType, IEventObserver observer)
        {
            if (!_events.TryGetValue(eventType, out var subject))
            {
                subject = new EventSubject(eventType);
                _events[eventType] = subject;
            }

            subject.AddObserver(observer);
        }

        public void RemoveObserver(string eventType, IEventObserver observer)
        {
            if (_events.TryGetValue(eventType, out var subject))
            {
                subject.RemoveObserver(observer);
            }
        }

        public void SendEvent(string eventType)
        {
            if (_events.TryGetValue(eventType, out var subject))
            {
                subject.NotifyAllObservers();
            }
        }

        public string GetSharedVariablePrint()
        {
            var sharedStr = "";
            foreach (var item in _data)
            {
                sharedStr += $"{item.Key}:{item.Value.GetValue()}  \n";
            }

            return sharedStr;
        }
    }
}