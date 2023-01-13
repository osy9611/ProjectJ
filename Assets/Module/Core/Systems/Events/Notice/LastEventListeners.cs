using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Module.Core.Systems.Collections.Generic;

namespace Module.Core.Systems.Events
{
    internal struct LastEventListenerData
    {
        public IEventNoticeListener Listener { get; set; }
        public ListenerDelegate Action { get; set; }
        public bool Recevied { get; set; }

        public LastEventListenerData(IEventNoticeListener listener, ListenerDelegate action)
        {
            Listener = listener;
            Action = action;
            Recevied = false;
        }

        public LastEventListenerData(IEventNoticeListener listener, ListenerDelegate action, bool recevied)
        {
            Listener = listener;
            Action = action;
            Recevied = recevied;
        }
    }

    internal class LastEventListeners : UnorderedList<LastEventListenerData>
    {
        public IEventArgs LastEventArgs { get; set; }
        public LastEventListeners(int capacity) : base(capacity)
        {

        }
    }
}