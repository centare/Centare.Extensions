// Copyright (c) 2018 Centare

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Centare.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class DeconstructExtensionTests
    {
        public static IEnumerable<object[]> DeconstructArgs =
            new[]
            {
                new object[] { new DateTime?(new DateTime(625936410000000000)), new DateTime(625936410000000000), true },
                new object[] { new int?(1), 1, true },
                new object[] { new long?(2), 2L, true },
                new object[] { new short?(3), 3, true },
                new object[] { new byte?(4), 4, true },
                new object[] { new float?(5), 5f, true },
                new object[] { new double?(6), 6d, true },
                new object[] { new decimal?(7), 7m, true },
                new object[] { new bool?(true), true, true },
                new object[] { new bool?(false), false, true }
            };

        [Theory, MemberData(nameof(DeconstructArgs))]
        public void NullableDeconstructTests<T>(T? initialValue, T expectedValue, bool expectedToHaveValue) 
            where T : struct
        {
            var (hasValue, value) = initialValue;
            Assert.Equal(expectedToHaveValue, hasValue);
            Assert.Equal(expectedValue, value);
        }

        [Fact]
        public void DateTimeNullDeconstructTests()
        {
            var (hasValue, value) = new DateTime?();
            Assert.False(hasValue);
            Assert.Equal(default, value);
        }

        [Fact]
        public void DateTimeNowDeconstructTests()
        {
            var now = DateTime.Now;
            var dateTime = new DateTime?(now);
            var (hasValue, value) = dateTime;
            Assert.True(hasValue);
            Assert.Equal(now, value);
        }

        [Fact]
        public void ArrayOfOneElementsDeconstructTests()
        {
            var (first, second) = new[] { 0 };

            Assert.Equal(0, first);
            Assert.Equal(default, second);

            (_, second) = new[] { 0, 7 };

            Assert.Equal(7, second);
        }

        [Fact]
        public void ArrayOfTwoElementsDeconstructTests()
        {
            var (first, second, third) = new[] { 0, 1 };

            Assert.Equal(0, first);
            Assert.Equal(1, second);
            Assert.Equal(default, third);

            (_, _, third) = new[] { 0, 1, 7 };

            Assert.Equal(7, third);
        }

        [Fact]
        public void ArrayOfThreeElementsDeconstructTests()
        {
            var (first, second, third, fourth) = new[] { 0, 1, 2 };

            Assert.Equal(0, first);
            Assert.Equal(1, second);
            Assert.Equal(2, third);
            Assert.Equal(default, fourth);

            (_, _, _, fourth) = new[] { 0, 1, 2, 7 };

            Assert.Equal(7, fourth);
        }

        [Fact]
        public void ArrayOfFourElementsDeconstructTests()
        {
            var (first, second, third, fourth, fifth) = new[] { 0, 1, 2, 3 };

            Assert.Equal(0, first);
            Assert.Equal(1, second);
            Assert.Equal(2, third);
            Assert.Equal(3, fourth);
            Assert.Equal(default, fifth);

            (_, _, _, _, fifth) = new[] { 0, 1, 2, 3, 7 };
            
            Assert.Equal(7, fifth);
        }

        [Fact]
        public void ArrayOfFiveElementsDeconstructTests()
        {
            var (first, second, third, fourth, fifth, sixth) = new[] { 0, 1, 2, 3, 4 };

            Assert.Equal(0, first);
            Assert.Equal(1, second);
            Assert.Equal(2, third);
            Assert.Equal(3, fourth);
            Assert.Equal(4, fifth);
            Assert.Equal(default, sixth);

            (_, _, _, _, _, sixth) = new[] { 0, 1, 2, 3, 4, 7 };

            Assert.Equal(7, sixth);
        }
    }
}