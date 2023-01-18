using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Module.Core.Systems.Collections.Generic;

namespace Module.Core.Systems.Events
{
    public class EventEmmiter<T>
    {
        private Dictionary<T, UnorderedList<ListenerDelegate>> values;
        private int listenerCapacity;

        public EventEmmiter(int listenerCapacity = 50)
        {
            values = new Dictionary<T, UnorderedList<ListenerDelegate>>();
            this.listenerCapacity = listenerCapacity;
        }

        public void AddListener(T id, ListenerDelegate listener)
        {
            if (values.TryGetValue(id, out UnorderedList<ListenerDelegate> listeners))
            {
                listeners.Add(listener);
            }
            else
            {
                listeners = new UnorderedList<ListenerDelegate>(listenerCapacity);
                listeners.Add(listener);
                values.Add(id, listeners);
            }
        }

        public void RemoveAllListeners(T id)
        {
            if (values.TryGetValue(id, out UnorderedList<ListenerDelegate> listeners))
            {
                listeners.Clear();
            }
        }

        public void RemoveListener(T id, ListenerDelegate listener)
        {
            if (values.TryGetValue(id, out UnorderedList<ListenerDelegate> listeners))
            {
                listeners.Remove(listener);
            }
        }

        public void Emit(T id, IEventArgs args)
        {
            if (values.TryGetValue(id, out UnorderedList<ListenerDelegate> listeners))
            {
                for (int i = 0, range = listeners.Count; i < range; ++i)
                {
                    listeners[i].Invoke(args);
                }
            }
        }

        static public EventEmmiter<T> Create(int listenerCapacity = 50)
        {
            if (typeof(T) == typeof(string))
            {
                return new StringEventEmmiter(listenerCapacity) as EventEmmiter<T>;
            }
            else if (typeof(T) == typeof(int))
            {
                return new IntEventEmmiter(listenerCapacity) as EventEmmiter<T>;
            }

            throw new System.InvalidOperationException("Type not supported");
        }


        internal class StringEventEmmiter : EventEmmiter<string>
        {
            internal StringEventEmmiter(int listenerCapacity = 50) : base(listenerCapacity) { }
        }
        internal class IntEventEmmiter : EventEmmiter<int>
        {
            internal IntEventEmmiter(int listenerCapacity = 50) : base(listenerCapacity) { }
        }
    }
}