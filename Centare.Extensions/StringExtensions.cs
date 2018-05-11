// Copyright (c) 2018 Centare

using System;
using System.ComponentModel;

namespace Centare.Extensions
{
    public static class StringExtensions
    {
        public static T ConvertOrDefault<T>(
            this string value, 
            T defaultValue = default) where T : struct
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            try
            {
                var result = TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
                return (T?)result ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static TEnum ToEnum<TEnum>(
            this string value) where TEnum : struct, IComparable, IFormattable, IConvertible
            => !string.IsNullOrWhiteSpace(value) &&
                   Enum.TryParse(value, true, out TEnum result)
                       ? result
                       : default;

        public static bool Contains(
            this string source, 
            string value, 
            StringComparison comp = StringComparison.OrdinalIgnoreCase)
            => source?.IndexOf(value ?? string.Empty, comp) >= 0;

    }
}