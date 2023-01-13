using System.Collections.Generic;
using System;

namespace Module.Core.Systems.Algorithm.Generic
{
    using Module.Core.Systems.Collections.Generic;

    public class RouletteInfo<T>
    {
        private T m_Item;
        private int m_Weight;

        public T Item
        {
            get => m_Item;
        }

        public int Weight
        {
            get => m_Weight;
        }

        public RouletteInfo(T item, int weight)
        {
            m_Item = item;
            m_Weight = weight;
        }
    }

    public class Roulette<T>
    {
        private UnorderedList<RouletteInfo<T>> m_Datas;
        private int m_TotalWeight;
        private bool m_IsMultipleSelection = true;

        public IReadOnlyList<RouletteInfo<T>> Datas
        {
            get => m_Datas;
        }

        public int TotalWeight
        {
            get => m_TotalWeight;
        }

        public bool IsMultipleSelection
        {
            get => m_IsMultipleSelection;
        }

        public Roulette(bool isMultipleSelection = true)
        {
            m_Datas = new UnorderedList<RouletteInfo<T>>();
            m_IsMultipleSelection = isMultipleSelection;
        }

        public T Select()
        {
            if (this.m_Datas.Count == 1)
                return m_Datas[0].Item;

            int weight = UnityEngine.Random.Range(1, this.m_TotalWeight + 1);
            int curWeight = 0;

            if (!m_IsMultipleSelection)
            {
                if (m_TotalWeight <= 0)
                    return default(T);

                for (int i = 0; i < m_Datas.Count; i++)
                {
                    curWeight += m_Datas[i].Weight;

                    if (curWeight >= weight)
                    {
                        RouletteInfo<T> info = m_Datas[i];
                        m_TotalWeight -= info.Weight;
                        m_Datas.RemoveAt(i);
                        return info.Item;
                    }
                }
            }
            else
            {
                for (int i = 0, range = m_Datas.Count; i < range; ++i)
                {
                    curWeight += m_Datas[i].Weight;

                    if (curWeight >= weight)
                        return m_Datas[i].Item;
                }
            }

            return default(T);
        }

        public void Add(RouletteInfo<T> t)
        {
            m_Datas.Add(t);
            m_TotalWeight += t.Weight;
        }
    }

    public class Roulettes<T>
    {
        private Dictionary<int, Roulette<T>> m_Datas = new Dictionary<int, Roulette<T>>();

        public void Add(int id, int weight, T item)
        {
            if (m_Datas.ContainsKey(id))
            {
                m_Datas[id].Add(new RouletteInfo<T>(item, weight));
            }
            else
            {
                Roulette<T> r = new Roulette<T>();
                r.Add(new RouletteInfo<T>(item, weight));
                m_Datas.Add(id, r);
            }
        }

        public Roulette<T> Get(int id)
        {
            if (m_Datas.ContainsKey(id))
                return m_Datas[id];

            return null;
        }

        public T Select(int id)
        {
            if (m_Datas.ContainsKey(id))
                return m_Datas[id].Select();

            return default(T);
        }
    }

    public class RouletteGroup<T>
    {
        private Dictionary<int, Roulette<T>> m_Datas = new Dictionary<int, Roulette<T>>();

        public IReadOnlyDictionary<int, Roulette<T>> Datas
        {
            get => m_Datas;
        }

        public void Add(int id, int weight, T item)
        {
            if (m_Datas.ContainsKey(id))
            {
                m_Datas[id].Add(new RouletteInfo<T>(item, weight));
            }
            else
            {
                Roulette<T> r = new Roulette<T>();
                r.Add(new RouletteInfo<T>(item, weight));
                m_Datas.Add(id, r);
            }
        }
    }

    public class RoulettGroups<T>
    {
        private Dictionary<int, RouletteGroup<T>> m_Datas = new Dictionary<int, RouletteGroup<T>>();

        public void Add(int id, int groupId, int weight, T item)
        {
            if (m_Datas.ContainsKey(id))
            {
                m_Datas[id].Add(groupId, weight, item);
            }
            else
            {
                RouletteGroup<T> r = new RouletteGroup<T>();
                r.Add(groupId, weight, item);
                m_Datas.Add(id, r);
            }
        }

        public RouletteGroup<T> Get(int id)
        {
            if (m_Datas.ContainsKey(id))
            {
                return m_Datas[id];
            }

            return null;
        }
    }
}

