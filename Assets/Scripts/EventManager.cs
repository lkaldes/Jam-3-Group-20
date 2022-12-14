using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class EventManager
{

    private static EventManager? events;
    public static EventManager Events 
    {
        get 
        {
        if (EventManager.events == null)
        {
            Debug.Log("get was null");
            EventManager.events = new EventManager();
        }
        //Debug.Log("get was not null");
        return EventManager.events;
        } 
        private set
        {
            EventManager.events = value;
        }
    }

    public object[]? Args {get; private set;}

    private class Event
    {
        private class EventCallback
        {
            public System.Action callback;
            public bool once;
            public EventCallback(System.Action callback, bool once = false)
            {
                this.callback = callback;
                this.once = once;
            }

            public void Call()
            {
                callback.Invoke();
            }
        }
        private object[]? lastArgs;
        private string name;
        private List<EventCallback> callbacks;
        public Event(string name)
        {
            callbacks = new List<EventCallback>();
            this.name = name;
        }

        public void Add(System.Action callback, bool once = false)
        {
            callbacks.Add(new EventCallback(callback, once));
        }

        public void Call(object[] args)
        {
            lastArgs = args;
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

        public void Remove(System.Action callback)
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

    public EventManager On(string eventName, System.Action callback, bool once = false)
    {
        if (!eventMap.ContainsKey(eventName))
        {
            eventMap[eventName] = new Event(eventName);
        }
        eventMap[eventName].Add(callback, once);
        return this;
    }

    public EventManager Once(string eventName, System.Action callback)
    {
        if (!eventMap.ContainsKey(eventName))
        {
            eventMap[eventName] = new Event(eventName);
        }
        eventMap[eventName].Add(callback, true);
        return this;
    }

    public EventManager Off(string eventName, System.Action callback)
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

    public EventManager Trigger(string eventName, params object[] args)
    {
        if (eventMap.ContainsKey(eventName))
        {
            Args = args;
            eventMap[eventName].Call(args);
            Args = null;
        }
        return this;
    }
}
