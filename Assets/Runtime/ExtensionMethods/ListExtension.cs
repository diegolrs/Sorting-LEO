using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, list.Count);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Queue<T> ToQueue<T>(this IList<T> list)
    {
        var queue = new Queue<T>();
        for(int i = 0; i < list.Count; i++)
        {
            queue.Enqueue(list[i]);
        }
        return queue;
    }
}
