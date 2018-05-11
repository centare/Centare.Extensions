// Copyright (c) 2018 Centare

using Centare.Extensions.Comparers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Centare.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
            => enumerable == null || !enumerable.Any();

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
        {
            var keys = new HashSet<TKey>(EqualityComparer<TKey>.Default);
            return enumerable.Where(value => keys.Add(keySelector(value)));
        }

        public static IDictionary<TKey, TElement> ToDistinctDictionary<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            var dictionary = new Dictionary<TKey, TElement>();
            foreach (TSource current in source)
            {
                dictionary[keySelector(current)] = elementSelector(current);
            }

            return dictionary;
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
            => source.MaxBy(keySelector, Comparer<TKey>.Default);

        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            comparer = comparer ?? Comparer<TKey>.Default;
            return source.Select(item => (key: keySelector(item), value: item))
                         .Aggregate((key: default(TKey), value: default(T)), (x, y) => comparer.Compare(x.key, y.key) > 0 ? x : y)
                         .value;
        }

        public static Queue<T> Enqueue<T>(this Queue<T> queue, IEnumerable<T> enumerable)
        {
            if (enumerable == null) return queue;
            foreach (var item in enumerable)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        public static Queue<T> DequeueWhile<T>(this Queue<T> queue, Func<T, bool> predicate)
        {
            if (queue == null || queue.Count == 0 || predicate == null) return queue;

            T value = queue.PeekOrDefault();
            while (predicate(value))
            {
                queue.Dequeue();
                value = queue.PeekOrDefault();
            }

            return queue;
        }

        public static ConcurrentQueue<T> Enqueue<T>(this ConcurrentQueue<T> queue, IEnumerable<T> enumerable)
        {
            if (enumerable == null) return queue;
            foreach (var item in enumerable)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        public static ConcurrentQueue<T> DequeueWhile<T>(this ConcurrentQueue<T> queue, Func<T, bool> predicate)
        {
            if (queue == null || queue.IsEmpty || predicate == null) return queue;

            T value = queue.TryPeekOrDefault();
            while (predicate(value))
            {
                queue.TryDequeue(out var dequeued);
                value = queue.TryPeekOrDefault();
            }

            return queue;
        }

        public static T PeekOrDefault<T>(this Queue<T> queue)
            => queue?.Count > 0 ? queue.Peek() : default;

        public static T TryPeekOrDefault<T>(this ConcurrentQueue<T> queue)
            => queue is null ? default : queue.TryPeek(out var result) ? result : default;

        public static IEnumerable<T> ToEnumerable<T>(this T value)
        {
            yield return value;
        }

        public static IList<T> AsList<T>(this T value)
            => new[] { value };

        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> items)
        {
            foreach (var item in items ?? Enumerable.Empty<T>())
            {
                stack.Push(item);
            }
        }

        public static IEnumerable<T> Join<T, TKey>(
            this IEnumerable<T> source,
            IEnumerable<T> other,
            Func<T, TKey> keySelector,
            IEqualityComparer<TKey> comparer = null)
            => source.Join(other, keySelector, keySelector, (t, _) => t, comparer ?? EqualityComparer<TKey>.Default);

        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T item)
            => source.Concat(new[] { item });

        public static IOrderedEnumerable<TSource> NaturalOrderBy<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, string> keySelector)
            => source.OrderBy(keySelector, NaturalStringComparer.Default);

        public static IEnumerable<T> MergeUniqueLatest<T, TKey>(
            this IEnumerable<T> source,
            IEnumerable<T> other,
            Func<T, TKey> keySelector,
            Func<T, DateTime> dateTimeSelector)
            => source.Concat(other)
                     .GroupBy(keySelector)
                     .Select(grp => grp.MaxBy(dateTimeSelector));
    }
}