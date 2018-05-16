// Copyright (c) 2018 Centare

using System;

namespace Centare.Extensions.Tests.Helpers
{
    public class Disposable : IDisposable
    {
        private Disposable() { }

        public static IDisposable Empty => new Disposable();

        void IDisposable.Dispose() { }
    }
}