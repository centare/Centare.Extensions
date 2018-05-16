// Copyright (c) 2018 Centare

using Microsoft.Extensions.Logging;
using System;

namespace Centare.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception ToAggregateInnerException(this Exception exception)
            => exception is AggregateException ae 
                ? ae.Flatten().InnerException 
                : exception;

        public static bool TryLogException(this Exception exception, ILogger logger)
        {
            if (logger is null || !logger.IsEnabled(LogLevel.Error))
            {
                return false;
            }

            var inner = exception.ToAggregateInnerException();
            logger.LogError(inner, inner.Message);

            return true;
        }
    }
}