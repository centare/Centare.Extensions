// Copyright (c) 2018 Centare

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Centare.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class StringExtensionTests
    {
        [Fact]
        public void ConvertOrDefaultTests()
        {
            Assert.Equal(1, "This is clearly not an integer!".ConvertOrDefault(1));
            Assert.Equal(TimeSpan.FromHours(1), "This is clearly not a TimeSpan!".ConvertOrDefault(TimeSpan.FromHours(1)));

            Assert.Equal(1.23456789m, "   ".ConvertOrDefault(1.23456789m));
            Assert.Equal(9.8765, string.Empty.ConvertOrDefault(9.8765));
            Assert.Equal(Guid.Empty, ((string)null).ConvertOrDefault(Guid.Empty));

            Assert.Equal(77, "77".ConvertOrDefault(400));
            Assert.Equal(Math.PI, "3.14159265358979323846".ConvertOrDefault(19.1));
            Assert.Equal(new DateTime(2016, 1, 1), "1/1/2016".ConvertOrDefault(DateTime.MaxValue));
            Assert.Equal(new Guid("3862B125-424E-472F-9916-8F30E64204E2"), "3862B125-424E-472F-9916-8F30E64204E2".ConvertOrDefault(Guid.Empty));
        }

        public static List<object[]> ToEnumInput
            => new List<object[]>
               {
                   new object[] { string.Empty, default(DayOfWeek) },
                   new object[] { "   ", default(DayOfWeek) },
                   new object[] { nameof(DayOfWeek.Friday), DayOfWeek.Friday },
                   new object[] { ((int)DayOfWeek.Friday).ToString(), DayOfWeek.Friday },
                   new object[] { "77", (DayOfWeek)77 }
               };

        [Theory, MemberData(nameof(ToEnumInput))]
        public void ToEnumTests<TEnum>(string from, TEnum expected)
            where TEnum : struct, IComparable, IFormattable, IConvertible 
            => Assert.Equal(expected, from.ToEnum<TEnum>());

        [Fact]
        public void ToEnumTest()
            => Assert.Equal(default, default(string).ToEnum<DayOfWeek>());

        [
            Theory,
            InlineData(null, null, StringComparison.Ordinal, false),
            InlineData("", " ", StringComparison.Ordinal, false),
            InlineData("Pickles", "www.google.com", StringComparison.Ordinal, false),
            InlineData(" ", " ", StringComparison.OrdinalIgnoreCase, true),
            InlineData("Somewhere over the rainbow", " ", StringComparison.OrdinalIgnoreCase, true),
            InlineData("Pickles", "ick", StringComparison.OrdinalIgnoreCase, true)
        ]
        public void StringContainsTest(string value, string match, StringComparison comparison, bool expected)
            => Assert.Equal(expected, value.Contains(match, comparison));
    }
}