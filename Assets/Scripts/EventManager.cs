using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class EventManager : MonoBehaviour
{

    public System.Delegate? testDelegate;
    private static EventManager? events;
    public static EventManager Events 
    {
        get 
        {
        if (EventManager.events == null)
        {
            EventManager.events = new EventManager();
        }
        return EventManager.events;
        } 
        private set
        {
            EventManager.events = value;
        }
    }

    private class Event
    {
        private class EventCallback
        {
            public System.Delegate callback;
            public bool once;
            public EventCallback(System.Delegate callback, bool once = false)
            {
                this.callback = callback;
                this.once = once;
            }

            public void Call()
            {
                callback.DynamicInvoke();
            }
        }
        private string name;
        private List<EventCallback> callbacks;
        public Event(string name)
        {
            callbacks = new List<EventCallback>();
            this.name = name;
        }

        public void Add(System.Delegate callback, bool once = false)
        {
            callbacks.Add(new EventCallback(callback, once));
        }

        public void Call()
        {
            for (int i = 0; i < callbacks.Count; i++)
            {
                EventCallback callback = callbacks[i];
                callback.Call();
                if (callback.once)
                {
                    callbacks.RemoveAt(i);
                    i -= 1;
                }
            }
        }

        public void Remove(System.Delegate callback)
        {
            if (callbacks.Count > 0)
            {
                int index = callbacks.FindIndex(delegate(EventCallback eventCallback) { return eventCallback.callback == callback; });
                if (index >= 0)
                {
                    callbacks.RemoveAt(index);
                }
            }
        }

        public int Count
        {
            get
            {
                return callbacks.Count;
            }
        }
    }

    private readonly Dictionary<string, Event> eventMap = new();

    public EventManager On(string eventName, System.Delegate callback, bool once = false)
    {
        if (!eventMap.ContainsKey(eventName))
        {
            eventMap[eventName] = new Event(eventName);
        }
        eventMap[eventName].Add(callback, once);
        return this;
    }

    public EventManager Once(string eventName, System.Delegate callback)
    {
        if (!eventMap.ContainsKey(eventName))
        {
            eventMap[eventName] = new Event(eventName);
        }
        eventMap[eventName].Add(callback, true);
        return this;
    }

    public EventManager Off(string eventName, System.Delegate callback)
    {
        if (eventMap.ContainsKey(eventName))
        {
            Event e = eventMap[eventName];
            e.Remove(callback);

            if (e.Count == 0)
            {
                eventMap.Remove(eventName);
            }
        }
        return this;
    }

    public EventManager Trigger(string eventName)
    {
        if (eventMap.ContainsKey(eventName))
        {
            eventMap[eventName].Call();
        }
        return this;
    }
}
