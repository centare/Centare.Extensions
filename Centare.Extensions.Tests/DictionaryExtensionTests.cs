// Copyright (c) 2018 Centare

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Centare.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class DictionaryExtensionTests
    {
        [Fact]
        public void GetValueOrDefaultTests()
        {
            var dictionary = new Dictionary<int, string>
            {
                [0] = "Zero",
                [1] = "One",
                [2] = "Two"
            };

            Assert.Null(dictionary.GetValueOrDefault(3));
            Assert.Equal("One", dictionary.GetValueOrDefault(1));
            Assert.Equal("Pickles", dictionary.GetValueOrDefault(9, "Pickles"));
        }
    }
}