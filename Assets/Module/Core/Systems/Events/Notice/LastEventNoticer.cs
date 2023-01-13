using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Module.Core.Systems.Collections.Generic;

namespace Module.Core.Systems.Events
{
    public class LastEventNoticer<T>
    {
        private Dictionary<T, LastEventListeners> m_Values;
        private int m_ListenerCapacity;

        internal LastEventNoticer(int listenerCapacity = 50)
        {
            m_Values = new Dictionary<T, LastEventListeners>();
            m_ListenerCapacity = listenerCapacity;
        }

        public void AddListener(T id, IEventNoticeListener listener, ListenerDelegate listenerAction)
        {
            if(m_Values.TryGetValue(id, out LastEventListeners listeners))
            {
                listeners.Add(new LastEventListenerData(listener, listenerAction));
            }
            else
            {
                listeners = new LastEventListeners(m_ListenerCapacity);
                listeners.Add(new LastEventListenerData(listener, listenerAction));
                m_Values.Add(id, listeners);
            }
        }

        public void RemoveAllListeners(T id)
        {
            if(m_Values.TryGetValue(id, out LastEventListeners listeners))
            {
                listeners.Clear();
            }
        }

        public void RemoveListener(T id, IEventNoticeListener listener)
        {
            if(m_Values.TryGetValue(id,out LastEventListeners listeners))
            {
                var idx = listeners.FindIndex(elem => elem.Listener == listener);
                if(idx > 0)
                {
                    listeners.RemoveAt(idx);
                }
            }
        }

        public void Clear()
        {
            m_Values.Clear();
        }

        public void Notify(T id, IEventArgs args)
        {
            if(m_Values.TryGetValue(id, out LastEventListeners listeners))
            {
                for(int i=0,range = listeners.Count;i<range;++i)
                {
                    listeners.LastEventArgs = args;

                    if(listeners[i].Listener.ReceivableFromEvent())
                    {
                        listeners[i] = new LastEventListenerData(listeners[i].Listener, listeners[i].Action, true);
                        listeners[i].Action.Invoke(args);
                    }
                    else
                    {
                        listeners[i] = new LastEventListenerData(listeners[i].Listener, listeners[i].Action, false);
                    }
                }
            }
            else
            {
                listeners = new LastEventListeners(m_ListenerCapacity);
                listeners.LastEventArgs = args;
                m_Values.Add(id, listeners);
            }
        }

        public bool Verify(T id, IEventNoticeListener listener)
        {
            if(m_Values.TryGetValue(id, out LastEventListeners listeners))
            {
                int idx = listeners.FindIndex(elem => elem.Listener == listener);

                if(idx >= 0)
                {
                    if(!listeners[idx].Recevied)
                    {
                        listeners[idx] = new LastEventListenerData(listeners[idx].Listener, listeners[idx].Action, true);
                        listeners[idx].Action.Invoke(listeners.LastEventArgs);
                        return true;
                    }
                }
            }

            return false;
        }

        static public LastEventNoticer<T> Create(int listenerCapacity = 50)
        {
            if (typeof(T) == typeof(string))
            {
                return new StringLastEventNoticer(listenerCapacity) as LastEventNoticer<T>;
            }
            else if (typeof(T) == typeof(int))
            {
                return new IntLastEventNoticer(listenerCapacity) as LastEventNoticer<T>;
            }

            throw new System.InvalidOperationException("Type not supported");
        }

        internal class StringLastEventNoticer : LastEventNoticer<string>
        {
            internal StringLastEventNoticer(int listenerCapacity = 50) : base(listenerCapacity) { }
        }
        internal class IntLastEventNoticer : LastEventNoticer<int>
        {
            internal IntLastEventNoticer(int listenerCapacity = 50) : base(listenerCapacity) { }
        }
    }
}