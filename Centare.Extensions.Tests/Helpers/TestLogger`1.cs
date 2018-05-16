// Copyright (c) 2018 Centare

using Microsoft.Extensions.Logging;
using System;

namespace Centare.Extensions.Tests.Helpers
{
    public class TestLogger<T> : ILogger<T>
    {
        private readonly LogLevel? _maxLogLevel;

        private TestLogger(LogLevel? maxLogLevel = null) => _maxLogLevel = maxLogLevel;

        public static ILogger<T> Create(LogLevel? maxLogLevel = null) => new TestLogger<T>(maxLogLevel);

        IDisposable ILogger.BeginScope<TState>(TState state) => Disposable.Empty;

        bool ILogger.IsEnabled(LogLevel logLevel) => !_maxLogLevel.HasValue || _maxLogLevel >= logLevel;

        void ILogger.Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
        }
    }
}