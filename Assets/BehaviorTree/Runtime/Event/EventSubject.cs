﻿using System.Collections.Generic;

namespace BT.Runtime
{
    public class EventSubject
    {
        private readonly string _eventType;
        private readonly HashSet<IEventObserver> _observers = new HashSet<IEventObserver>();

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