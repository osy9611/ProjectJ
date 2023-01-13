using System.Collections;
using System.Collections.Generic;
using System;

namespace Module.Core.Systems.Pool
{
    static public class ObjectPoolHelper<T> where T : class, new()
    {
        static Dictionary<int, Pool<T>> s_Pools;

        public static Dictionary<int, Pool<T>> Pools
        {
            get => s_Pools;
        }

        static public Pool<T> Create(int uid, bool collectionChecks, int initGenerateCount, int initialCapacity, int maxPoolSize = 0)
        {
            return Create(uid, null, collectionChecks, initGenerateCount, initialCapacity, maxPoolSize);
        }

        static public Pool<T> Create(int uid, Func<T> objectGenerator, bool collectionChecks, int initGenerateCount, int initialCapacity, int maxPoolSize = 0)
        {
            if(s_Pools ==null)
            {
                s_Pools = new Dictionary<int, Pool<T>>();
            }

            Pool<T> pool = null;

            if(objectGenerator == null)
            {
                pool = new Pool<T>(() =>
                {
                    return new T();
                }, collectionChecks, initGenerateCount, initialCapacity, maxPoolSize);
            }
            else
            {
                pool = new Pool<T>(objectGenerator, collectionChecks, initGenerateCount, initialCapacity, maxPoolSize);
            }

            s_Pools.Add(uid, pool);

            return pool;
        }

        //Create와의 차이점은 uid로 데이터를 관리하는지에 대한 차이점이다
        static public Pool<T> CreateInUnmanaged(bool collectionChecks, int initGenerateCount, int initialCapacity, int maxPoolSize =0)
        {
            return CreateInUnmanaged(null, collectionChecks, initGenerateCount, initialCapacity, maxPoolSize);
        }

        static public Pool<T> CreateInUnmanaged(Func<T> objectGenerator, bool collectionChecks, int initGenerateCount, int initialCapacity, int maxPoolSize = 0)
        {
            Pool<T> pool = null;

            if (objectGenerator == null)
            {
                pool = new Pool<T>(() =>
                {
                    return new T();
                }, collectionChecks, initGenerateCount, initialCapacity, maxPoolSize);
            }
            else
            {
                pool = new Pool<T>(objectGenerator, collectionChecks, initGenerateCount, initialCapacity, maxPoolSize);
            }

            return pool;
        }

        static public Pool<T> Get(int uid)
        {
            return s_Pools[uid];
        }

        static public void Remove(int uid)
        {
            if(s_Pools.TryGetValue(uid,out Pool<T> pool))
            {
                pool.RemoveAll();
                s_Pools.Remove(uid);
            }
        }

        static public void Clear()
        {

        }
    }
}