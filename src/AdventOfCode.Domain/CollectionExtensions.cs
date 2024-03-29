﻿using System.Drawing;

namespace AdventOfCode;

public static class CollectionExtensions
{
    public static bool TryAddAndMaintainUnique<T, U>(this IDictionary<T, U> dictionary, T key, U value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary.Remove(key);
            return false;
        }

        dictionary[key] = value;
        return true;
    }

    public static U TryAdd<T, U>(this IDictionary<T, U> dictionary, T key, Func<T, U> initialiser)
    {
        if (dictionary.TryGetValue(key, out U? val)) return val;

        dictionary[key] = initialiser(key);

        return dictionary[key];
    }

    public static IList<T> Swap<T>(this IEnumerable<T> coll, int i1, int i2) => coll.ToArray().Swap(i1, i2);

    public static IList<T> Swap<T>(this IList<T> coll, int i1, int i2)
    {
        (coll[i1], coll[i2]) = (coll[i2], coll[i1]);
        return coll;
    }

    public static IList<T> QuickSort<T>(this IEnumerable<T> coll, Func<T, T, bool> currIsLessThanPivot) =>
        coll.ToArray().QuickSort(currIsLessThanPivot);

    public static IList<T> QuickSort<T>(this IList<T> coll, Func<T, T, bool> currIsLessThanPivot)
    {
        QuickSort(coll, 0, coll.Count - 1, currIsLessThanPivot);
        return coll;
    }
    
    private static void QuickSort<T>(IList<T> arr, int start, int end, Func<T, T, bool> currIsSmallerThanPivot)
    {
        if (start < end)
        {
            var p = Partition();
            QuickSort(arr, start, p - 1, currIsSmallerThanPivot);
            QuickSort(arr, p + 1, end, currIsSmallerThanPivot);
        }
        
        int Partition()
        {
            var pivot = arr[end];
            var i = start - 1;
            for (var j = start; j < end; j++)
            {
                if (currIsSmallerThanPivot(arr[j], pivot))
                {
                    arr.Swap(++i, j);
                }
            }

            arr.Swap(++i, end);
            return i;
        }
    }
}