using System.Collections.Generic;
using UnityEngine.Events;
using EventType = Game.Core.Enums.EventType;

namespace Game.Core.Events
{
    public static class EventBase
    {
        private static readonly Dictionary<EventType, UnityEvent> Events = new Dictionary<EventType, UnityEvent>();
        private static readonly Dictionary<EventType, BooleanEvent> BooleanEvents = new Dictionary<EventType, BooleanEvent>();

        public static void StartListening(EventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.ContainsKey(eventType))
            {
                thisEvent = Events[eventType];
            }
            else
            {
                thisEvent = new UnityEvent();
                Events.Add(eventType, thisEvent);
            }

            thisEvent.AddListener(listener);
        }

        // Overloaded version of the StartListening
        public static void StartListening(EventType eventType, UnityAction<bool> listener)
        {
            BooleanEvent thisEvent;

            if (BooleanEvents.ContainsKey(eventType))
            {
                thisEvent = BooleanEvents[eventType];
            }
            else
            {
                thisEvent = new BooleanEvent();
                BooleanEvents.Add(eventType, thisEvent);
            }
        
            thisEvent.AddListener(listener);
        }

        public static void StopListening(EventType eventType, UnityAction listener)
        {
            if (Events.TryGetValue(eventType, out UnityEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        // Overloaded version of the StopListening
        public static void StopListening(EventType eventType, UnityAction<bool> listener)
        {
            if (BooleanEvents.TryGetValue(eventType, out BooleanEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
        
        public static void NotifyListeners(EventType eventType, bool data)
        {
            if (BooleanEvents.TryGetValue(eventType, out BooleanEvent thisEvent))
            {
                thisEvent.Invoke(data);
            }
        }

        // Overloaded version of the NotifyListeners
        public static void NotifyListeners(EventType eventType)
        {
            if (Events.TryGetValue(eventType, out UnityEvent thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        private class BooleanEvent : UnityEvent<bool> { }
    }
}
