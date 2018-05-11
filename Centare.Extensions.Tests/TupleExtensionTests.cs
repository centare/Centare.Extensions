// Copyright (c) 2018 Centare

using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Centare.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class TupleExtensionTests
    {
        [Fact]
        public void ByteDivideByTest()
        {
            var (result, remainder) = ((byte)7).DividedBy(3);
            Assert.Equal(2, result);
            Assert.Equal(1, remainder);
        }

        [Fact]
        public void SignedByteDivideByTest()
        {
            var (result, remainder) = ((sbyte)7).DividedBy(3);
            Assert.Equal(2, result);
            Assert.Equal(1, remainder);
        }

        [Fact]
        public void ShortDivideByTest()
        {
            var (result, remainder) = ((short)7).DividedBy(3);
            Assert.Equal(2, result);
            Assert.Equal(1, remainder);
        }

        [Fact]
        public void UnsignedShortDivideByTest()
        {
            var (result, remainder) = ((ushort)7).DividedBy(3);
            Assert.Equal(2, result);
            Assert.Equal(1, remainder);
        }

        [Fact]
        public void Int32ShortDivideByTest()
        {
            var (result, remainder) = 7.DividedBy(3);
            Assert.Equal(2, result);
            Assert.Equal(1, remainder);
        }

        [Fact]
        public void UnsignedInt32ShortDivideByTest()
        {
            var (result, remainder) = ((uint)7).DividedBy(3);
            Assert.Equal((uint)2, result);
            Assert.Equal((uint)1, remainder);
        }

        [Fact]
        public void Int64ShortDivideByTest()
        {
            var (result, remainder) = 7L.DividedBy(3);
            Assert.Equal(2, result);
            Assert.Equal(1, remainder);
        }

        [Fact]
        public void UnsignedInt64ShortDivideByTest()
        {
            var (result, remainder) = ((ulong)7).DividedBy(3);
            Assert.Equal((ulong)2, result);
            Assert.Equal((ulong)1, remainder);
        }

        [Fact]
        public void SingleDivideByTest()
        {
            var (result, remainder) = 7.7F.DividedBy(3.9F);
            Assert.Equal(1.97435892F, result);
            Assert.Equal(3.79999971F, remainder);
        }

        [Fact]
        public void DoubleDivideByTest()
        {
            var (result, remainder) = 7.7D.DividedBy(3.9D);
            Assert.Equal(1.9743589743589745D, result);
            Assert.Equal(3.8000000000000003D, remainder);
        }

        [Fact]
        public void DecimalDivideByTest()
        {
            var (result, remainder) = 7.7M.DividedBy(3.9M);
            Assert.Equal(1.9743589743589743589743589744M, result);
            Assert.Equal(3.8M, remainder);
        }
    }
}