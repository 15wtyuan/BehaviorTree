using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public class EventSubject
    {
        private readonly string _eventType;
        private readonly HashSet<IEventObserver> _observers = new();

        public EventSubject(string eventType)
        {
            _eventType = eventType;
        }

        public void AddObserver(IEventObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IEventObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyAllObservers()
        {
            foreach (var o in _observers)
            {
                o.OnNotify(_eventType);
            }
        }
    }
}