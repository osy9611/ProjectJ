namespace Module.Core.Systems.Pool
{
    using System;
    using System.Collections.Generic;
    public class FixedPool<T> where T : class
    {
        private readonly Stack<T> m_InactiveObjects;
        private readonly T[] m_ActiveObjects;
        private readonly T[] m_ObjectAll;
        private readonly int m_MaxPoolSize;

        private int m_ActiveCount = 0;
        private bool m_CollectionChecks = false;

        public int Count => m_InactiveObjects.Count;
        public int ActiveCount => m_ActiveCount;

        public T[] ObjectAll
        {
            get { return m_ObjectAll; }
        }

        public T[] ActiveObjects
        {
            get { return m_ActiveObjects; }
        }

        public FixedPool(T[] originValues, bool collectionChecks, int maxPoolSize)
           : this(collectionChecks, maxPoolSize)
        {
            for (int i = 0; i < m_MaxPoolSize; i++)
            {
                m_ObjectAll[i] = originValues[i];
                m_InactiveObjects.Push(originValues[i]);
            }
        }

        public FixedPool(Func<T> objectGenerator, bool collectionChecks, int maxPoolSize)
            : this(collectionChecks, maxPoolSize)
        {
            for (int i = 0; i < m_MaxPoolSize; i++)
            {
                T t = objectGenerator();
                m_ObjectAll[i] = t;
                m_InactiveObjects.Push(t);
            }
        }

        public FixedPool(Func<int, T> objectGenerator, bool collectionChecks, int maxPoolSize)
            : this(collectionChecks, maxPoolSize)
        {
            for (int i = 0; i < m_MaxPoolSize; i++)
            {
                T t = objectGenerator(i);
                m_ObjectAll[i] = t;
                m_InactiveObjects.Push(t);
            }
        }

        private FixedPool(bool collectionChecks, int maxPoolSize)
        {
            m_MaxPoolSize = maxPoolSize;
            m_CollectionChecks = collectionChecks;

            m_InactiveObjects = new Stack<T>(m_MaxPoolSize);
            m_ObjectAll = new T[m_MaxPoolSize];
            m_ActiveObjects = new T[m_MaxPoolSize];

            m_ActiveCount = 0;
        }

        public T Get()
        {
            T item = default(T);

            if(m_InactiveObjects.Count >0)
            {
                item = m_InactiveObjects.Pop();
            }
            else
            {
                return default(T);
            }

            m_ActiveObjects[m_ActiveCount++] = item;

            return item;
        }

        public void Return(T item)
        {
            if(m_CollectionChecks)
            {
                if (!Array.Exists<T>(m_ActiveObjects, elem => elem == item))
                {
                    throw new Exception("not found item in active object list");
                }

                if (m_InactiveObjects.Contains(item))
                {
                    throw new Exception("found item in inactive object list");
                }
            }


            int idx = Array.FindIndex(m_ActiveObjects, (elem => elem == item));
            m_ActiveObjects[idx] = m_ActiveObjects[m_ActiveCount - 1];
            m_ActiveCount--;

            m_InactiveObjects.Push(item);
        }
    }
}