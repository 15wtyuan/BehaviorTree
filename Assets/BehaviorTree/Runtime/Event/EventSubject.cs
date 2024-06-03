using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public class EventSubject
    {
        private string _eventType;
        private HashSet<IEventObserver> _observers;

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