﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Random = UnityEngine.Random;

namespace Com.Jschiff.UnityExtensions {
    public static class CollectionExtensions {
        private static readonly Dictionary<Type, object> emptyLists = new Dictionary<Type, object>();

        public static IList<T> EmptyList<T>() {
            object list;
            if (!emptyLists.TryGetValue(typeof(T), out list)) {
                list = new ReadOnlyCollection<T>(new List<T>());
            }
            return (IList<T>)list;
        }

        public static TValue GetValueOrDefault<TKey, TValue> (this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }
        
        public static V Compute<K, V>(this Dictionary<K, V> dict, K key, Func<K, V, V> fun) {
            if (dict.TryGetValue(key, out V value)) {
                V newVal = fun(key, value);
                dict[key] = newVal;
                return newVal;
            }
            else {
                V v = default;
                v = fun(key, v);
                dict[key] = v;
                return v;
            }
        }

        public static V ComputeIfAbsent<K, V>(this Dictionary<K, V> dict, K key, Func<K, V> newInstanceFunction) {
            if (dict.TryGetValue(key, out V value)) {
                return value;
            }
            else {
                var instance = newInstanceFunction(key);
                dict[key] = instance;
                return instance;
            }
        }

        public static V ComputeIfAbsent<K, V>(this Dictionary<K, V> dict, K key, Func<V> newInstanceFunction) {
            if (dict.TryGetValue(key, out V value)) {
                return value;
            }
            else {
                var instance = newInstanceFunction();
                dict[key] = instance;
                return instance;
            }
        }

        private static int BinarySearch<T>(this IList<T> list, T value) {
            if (list == null)
                throw new ArgumentNullException("list");
            var comp = Comparer<T>.Default;
            int lo = 0, hi = list.Count - 1;
            while (lo < hi) {
                int m = (hi + lo) / 2;  // this might overflow; be careful.
                if (comp.Compare(list[m], value) < 0) lo = m + 1;
                else hi = m - 1;
            }
            if (comp.Compare(list[lo], value) < 0) lo++;
            return lo;
        }

        public static int FindFirstIndexGreaterThanOrEqualTo<T, U>
                                  (this SortedList<T, U> sortedList, T key) {
            return sortedList.Keys.BinarySearch(key);
        }
        public static T GetRandomElement<T>(this IReadOnlyList<T> list) {
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }

        static void Shuffle<T>(this T[] array) {
            int n = array.Length;
            for (int i = 0; i < (n - 1); i++) {
                int r = i + Random.Range(0, n - i);
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }

        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            for (int i = 0; i < (n - 1); i++) {
                int r = i + Random.Range(0, n - i);
                T t = list[r];
                list[r] = list[i];
                list[i] = t;
            }
        }
        
        public static int IndexOf<T>(this IReadOnlyList<T> self, T elementToFind) {
            int i = 0;
            foreach(T element in self)
            {
                if(Equals(element, elementToFind))
                    return i;
                i++;
            }
            return -1;
        }
    }
}
