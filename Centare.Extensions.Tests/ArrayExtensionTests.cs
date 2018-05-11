// Copyright (c) 2018 Centare

using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Centare.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class ArrayExtensionTests
    {
        [Fact]
        public void ValueOrDefaultGetsDefaultWhenNull()
        {
            int[] @null = null;
            Assert.Equal(default, @null.ValueOrDefault(7));
        }

        [Fact]
        public void ValueOrDefaultGetsValueWhenPresent()
        {
            var values = new[] { 0, 1, 2 };
            Assert.Equal(2, values.ValueOrDefault(2));
        }
    }
}