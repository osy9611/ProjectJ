namespace Module.Core.Systems.Pool
{
    using System;
    using System.Collections.Generic;
    using Module.Core.Systems.Collections.Generic;
    
    public class Pool<T> : IDisposable
    {
        private readonly UnorderedList<T> m_InactiveObjects;
        private readonly UnorderedList<T> m_ActiveObjects;
        private readonly Func<T> m_ObjectGenerator;
        private readonly bool m_CollectionChecks;
        private readonly int m_MaxPoolSize;

        private int m_ActiveCount = 0;

        public int InactiveCount => m_InactiveObjects.Count;
        public int Count => m_InactiveObjects.Count + m_ActiveCount;
        public int ActiveCount => m_ActiveCount;
        public ReadOnlyList<T> ActiveObjects => m_ActiveObjects.AsReadOnly();
        public ReadOnlyList<T> InactiveObjects => m_InactiveObjects.AsReadOnly();

        public Pool(T preCreatedObject, Func<T> objectGenerator, bool collectionChecks, int initGenerateCount, int initialCapacity, int maxPoolSize = 0)
           : this(objectGenerator, collectionChecks, initGenerateCount, initialCapacity, maxPoolSize)
        {
            m_InactiveObjects.Add(preCreatedObject);
        }

        public Pool(Func<T> objectGenerator, bool collectionChecks, int initGenerateCount, int initialCapacity, int maxPoolSize = 0)
        {
            m_ObjectGenerator = objectGenerator;
            m_CollectionChecks = collectionChecks;
            m_MaxPoolSize = maxPoolSize;

            if (m_MaxPoolSize > 0)
            {
                if (initGenerateCount > m_MaxPoolSize)
                {
                    initGenerateCount = m_MaxPoolSize;
                }

                if (initialCapacity > m_MaxPoolSize)
                {
                    initialCapacity = m_MaxPoolSize;
                }
                else if (initialCapacity < initGenerateCount)
                {
                    initialCapacity = initGenerateCount;
                }
            }

            m_InactiveObjects = new UnorderedList<T>(initialCapacity);
            m_ActiveObjects = new UnorderedList<T>(initialCapacity);

            for (int i = 0; i < initGenerateCount; ++i)
                m_InactiveObjects.Add(objectGenerator());
        }

        ~Pool()
        {
            Clear();
        }

        public T Get()
        {
            T item = default(T);

            if (m_InactiveObjects.Count > 0)
            {
                int idx = m_InactiveObjects.Count - 1;
                item = m_InactiveObjects[idx];
                m_InactiveObjects.RemoveAt(idx);
            }
            else
            {
                if (m_MaxPoolSize > 0 && m_MaxPoolSize <= m_ActiveCount)
                {
                    throw new Exception("pool is fulled");
                }

                item = m_ObjectGenerator();
            }

            IPoolable iPool = item as IPoolable;

            if (iPool != null)
            {
                iPool.OnTakeFromPool();
            }

            m_ActiveObjects.Add(item);
            m_ActiveCount++;

            return item;
        }

        public void Return(T item)
        {
            if (m_CollectionChecks)
            {
                if (!m_ActiveObjects.Contains(item))
                {
                    throw new Exception("not found item in active object list");
                }

                if (m_InactiveObjects.Contains(item))
                {
                    throw new Exception("found item in inactive object list");
                }
            }

            m_ActiveObjects.Remove(item);

            IPoolable iPool = item as IPoolable;

            if (iPool != null)
            {
                iPool.OnReturnedToPool();
            }

            m_ActiveCount--;

            m_InactiveObjects.Add(item);
        }

        public void Return(int activeIndex)
        {
            var item = m_ActiveObjects[activeIndex];

            if (m_CollectionChecks)
            {
                if (!m_ActiveObjects.Contains(item))
                {
                    throw new Exception("not found item in active object list");
                }

                if (m_InactiveObjects.Contains(item))
                {
                    throw new Exception("found item in inactive object list");
                }
            }

            IPoolable iPool = item as IPoolable;

            if (iPool != null)
            {
                iPool.OnReturnedToPool();
            }

            m_ActiveObjects.RemoveAt(activeIndex);
            m_ActiveCount--;
            m_InactiveObjects.Add(item);
        }

        public void RemoveAll()
        {
            for (int i = 0; i < m_ActiveObjects.Count; i++)
            {
                Return(m_ActiveObjects[i]);
            }
        }

        public void Clear()
        {
            foreach (var item in m_ActiveObjects)
            {
                IPoolable iPool = item as IPoolable;

                if (iPool != null)
                {
                    iPool.OnReturnedToPool();
                }

                m_InactiveObjects.Add(item);
            }

            m_ActiveObjects.Clear();
            m_ActiveCount = 0;
        }

        public void Dispose()
        {
            Clear();
        }
    }
}