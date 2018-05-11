// Copyright (c) 2018 Centare

namespace Centare.Extensions
{
    public static class ArrayExtensions
    {
        public static T ValueOrDefault<T>(this T[] array, int index)
            => array?.Length > index ? array[index] : default;
    }
}