// Copyright (c) 2018 Centare

using System;
using Xunit;

namespace Centare.Extensions.Tests
{
    public class DisposableExtensionTests
    {
        [Fact]
        public void UsingActionTest()
        {
            var disposable = new DisposeOfMePlease();
            disposable.Using(d => d.TakeAction(7, "favorite number"));
            Assert.True(disposable.IsDisposed);
        }

        [Fact]
        public void UsingFuncTest()
        {
            var disposable = new DisposeOfMePlease();
            var result = disposable.Using(d => d.Sum(3, 4));
            Assert.Equal(7, result);
            Assert.True(disposable.IsDisposed);
        }
    }

    public class DisposeOfMePlease : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void TakeAction(int num, string value) { }

        public int Sum(int left, int right) => left + right;

        public void Dispose() => IsDisposed = true;
    }
}