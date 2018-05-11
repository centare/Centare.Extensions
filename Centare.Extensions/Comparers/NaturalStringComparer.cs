// Copyright (c) 2018 Centare

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Centare.Extensions.Comparers
{
    public class NaturalStringComparer : IComparer<string>
    {
        private static readonly Regex _regex = new Regex(@"(?<=\D)(?=\d)|(?<=\d)(?=\D)", RegexOptions.Compiled);

        public static NaturalStringComparer Default { get; } = new NaturalStringComparer();

        private NaturalStringComparer()
        {
        }

        public int Compare(string x, string y)
        {
            if (string.Compare(x, 0, y, 0, Math.Min(x.Length, y.Length), true) == 0)
            {
                if (x.Length == y.Length) return 0;
                return x.Length < y.Length ? -1 : 1;
            }

            var a = _regex.Split(x);
            var b = _regex.Split(y);

            var i = 0;
            while (true)
            {
                var r = PartCompare(a[i], b[i]);
                if (r != 0) return r;
                ++ i;
            }
        }

        private static int PartCompare(string x, string y) 
            => int.TryParse(x, out var a) && int.TryParse(y, out var b)
                ? a.CompareTo(b)
                : string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
    }
}
