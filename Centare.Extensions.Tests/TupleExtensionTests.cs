// Copyright (c) 2018 Centare

using Centare.Extensions.Tests.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
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

        [Fact]
        public async Task TryAwaitOrLogAsyncReturns500Test()
            => Assert.Equal(
                500,
                await (Task.FromResult(500), TestLogger<Disposable>.Create()).TryAwaitOrLogAsync());

        [Fact]
        public async Task TryAwaitOrLogAsyncReturnsExpectedNullDefaultTest()
            => Assert.Equal(
                default,
                await (task: Task.Delay(1).ContinueWith<TimeSpan>(_ => throw new Exception("Oops...")),
                       logger: TestLogger<Disposable>.Create())
                    .TryAwaitOrLogAsync());

        [Fact]
        public async Task TryAwaitOrLogAsyncReturnsExpectedDefaultTest() 
            => Assert.Equal(
                7, 
                await (task: Task.Delay(1).ContinueWith<int>(_ => throw new Exception("Oops...")),
                       logger: TestLogger<Disposable>.Create())
                    .TryAwaitOrLogAsync(7));

        [Fact]
        public async Task TryAwaitOrLogAsyncReturnsWithNullLoggerTest()
            => Assert.Equal(
                new DateTime(1984, 7, 7),
                await (Task.FromResult(new DateTime(1984, 7, 7)), null as ILogger).TryAwaitOrLogAsync());

        [Fact]
        public async Task TryAwaitOrLogAsyncThrowsWithoutLoggerTest()
            => await Assert.ThrowsAsync<Exception>(
                async () => await (
                    Task.Delay(1).ContinueWith(_ => throw new Exception("OMG...LOL")), 
                    null as ILogger)
                .TryAwaitOrLogAsync());

        [Fact]
        public async Task TryAwaitOrLogAsyncThrowsWithInsufficientLogLevelTest()
            => await Assert.ThrowsAsync<AggregateException>(
                async () => await (
                    Task.Delay(1)
                        .ContinueWith(_ => throw new AggregateException(new Exception("ROFL!"))),
                    TestLogger<Disposable>.Create(LogLevel.Warning))
                .TryAwaitOrLogAsync());
    }    
}