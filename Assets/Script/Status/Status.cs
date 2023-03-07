using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Status<T>
{
    private T[] values;

    public T[] Valuse { get => values; }

    public int Count { get => values.Length; }

    public Status(int count)
    {
        values = new T[count];
    }

    public T this[int index]
    {
        get => values[index];
        set=> values[index] = value;
    }

    public void Clear()
    {
        System.Array.Clear(values, 0, values.Length);
    }
}