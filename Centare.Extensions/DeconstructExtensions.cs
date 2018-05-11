// Copyright (c) 2018 Centare

namespace Centare.Extensions
{
    public static class DeconstructionExtensions
    {
        public static void Deconstruct<T>(
            this T? nullable, 
            out bool hasValue, 
            out T value) where T : struct
        {
            hasValue = nullable.HasValue;
            value = nullable.GetValueOrDefault();
        }

        public static void Deconstruct<T>(
            this T[] array,
            out T first,
            out T second)
        {
            first = array.ValueOrDefault(0);
            second = array.ValueOrDefault(1);
        }

        public static void Deconstruct<T>(
            this T[] array,
            out T first,
            out T second,
            out T third)
        {
            first = array.ValueOrDefault(0);
            second = array.ValueOrDefault(1);
            third = array.ValueOrDefault(2);
        }

        public static void Deconstruct<T>(
            this T[] array,
            out T first,
            out T second,
            out T third,
            out T fourth)
        {
            first = array.ValueOrDefault(0);
            second = array.ValueOrDefault(1);
            third = array.ValueOrDefault(2);
            fourth = array.ValueOrDefault(3);
        }

        public static void Deconstruct<T>(
            this T[] array,
            out T first,
            out T second,
            out T third,
            out T fourth,
            out T fifth)
        {
            first = array.ValueOrDefault(0);
            second = array.ValueOrDefault(1);
            third = array.ValueOrDefault(2);
            fourth = array.ValueOrDefault(3);
            fifth = array.ValueOrDefault(4);
        }

        public static void Deconstruct<T>(
            this T[] array,
            out T first,
            out T second,
            out T third,
            out T fourth,
            out T fifth,
            out T sixth)
        {
            first = array.ValueOrDefault(0);
            second = array.ValueOrDefault(1);
            third = array.ValueOrDefault(2);
            fourth = array.ValueOrDefault(3);
            fifth = array.ValueOrDefault(4);
            sixth = array.ValueOrDefault(5);
        }
    }
}