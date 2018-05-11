// Copyright (c) 2018 Centare

using System.Collections.Generic;

namespace Centare.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue = default)
            => dictionary is null
                ? defaultValue
                : dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    }
}
