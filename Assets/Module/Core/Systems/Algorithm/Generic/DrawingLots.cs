using System.Collections.Generic;
using Module.Core.Systems.Collections.Generic;
using System;

namespace Module.Core.Systems.Algorithm.Generic
{
    public class DrawingLots<T>
    {
        private int m_RemainCount;
        private UnorderedList<T> m_Items;

        public int RemainCount => m_RemainCount;
        public int TotalCount => m_Items.Count;

        public T this[int index]
        {
            get => m_Items[index];
        }

        public DrawingLots(int capacity = 4)
        {
            m_Items = new UnorderedList<T>(capacity);
        }

        public void AddItem(T item)
        {
            m_Items.Add(item);
        }

        public void AddItem(IEnumerable<T> collection)
        {
            m_Items.AddRange(collection);
        }

        public T SelectInOverlap()
        {
            return m_Items[UnityEngine.Random.Range(0, m_Items.Count - 1)];
        }

        public void Start()
        {
            m_RemainCount = m_Items.Count;
        }

        public bool Select(out T outValue)
        {
            if(m_RemainCount <= 0)
            {
                outValue = default(T);
                return false;
            }

            var selIdx = UnityEngine.Random.Range(0, m_RemainCount - 1);
            var item = m_Items[m_RemainCount - 1];
            var selItem = m_Items[selIdx];

            m_Items[selIdx] = item;
            m_Items[m_RemainCount - 1] = selItem;

            m_RemainCount--;

            outValue = selItem;

            return true;
        }
    }
}