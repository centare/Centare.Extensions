// Copyright (c) 2018 Centare

namespace Centare.Extensions
{
    public static class TupleExtensions
    {
        public static (byte result, byte remainder) DividedBy(this byte numerator, byte denominator)
            => (result: (byte)(numerator / denominator), remainder: (byte)(numerator % denominator));

        public static (sbyte result, sbyte remainder) DividedBy(this sbyte numerator, sbyte denominator)
            => (result: (sbyte)(numerator / denominator), remainder: (sbyte)(numerator % denominator));

        public static (short result, short remainder) DividedBy(this short numerator, short denominator)
            => (result: (short)(numerator / denominator), remainder: (short)(numerator % denominator));

        public static (ushort result, ushort remainder) DividedBy(this ushort numerator, ushort denominator)
            => (result: (ushort)(numerator / denominator), remainder: (ushort)(numerator % denominator));

        public static (int result, int remainder) DividedBy(this int numerator, int denominator)
            => (result: (int)(numerator / denominator), remainder: (int)(numerator % denominator));

        public static (uint result, uint remainder) DividedBy(this uint numerator, uint denominator)
            => (result: (uint)(numerator / denominator), remainder: (uint)(numerator % denominator));

        public static (long result, long remainder) DividedBy(this long numerator, long denominator)
            => (result: (long)(numerator / denominator), remainder: (long)(numerator % denominator));

        public static (ulong result, ulong remainder) DividedBy(this ulong numerator, ulong denominator)
            => (result: (ulong)(numerator / denominator), remainder: (ulong)(numerator % denominator));

        public static (float result, float remainder) DividedBy(this float numerator, float denominator)
            => (result: (float)(numerator / denominator), remainder: (float)(numerator % denominator));

        public static (double result, double remainder) DividedBy(this double numerator, double denominator)
            => (result: (double)(numerator / denominator), remainder: (double)(numerator % denominator));

        public static (decimal result, decimal remainder) DividedBy(this decimal numerator, decimal denominator)
            => (result: (decimal)(numerator / denominator), remainder: (decimal)(numerator % denominator));
    }
}