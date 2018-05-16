// Copyright (c) 2018 Centare

using Centare.Extensions.Tests.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;

namespace Centare.Extensions.Tests
{
    public class ExceptionExtensionTests
    {
        public static IEnumerable<object[]> TryLogExceptionInputs =
            new List<object[]>
            {
                new object[] { null as ILogger<Disposable> },
                new object[] { TestLogger<Disposable>.Create(LogLevel.Information) }
            };

        [
            Theory,
            MemberData(nameof(TryLogExceptionInputs))
        ]
        public void ExceptionIsThrownWhenNullOrLogLevelIsInsufficient<T>(ILogger<T> logger)
            => Assert.Throws<Exception>(
                () =>
                {
                    try
                    {
                        throw new Exception("Something bad happened!");
                    }
                    catch (Exception ex) when (ex.TryLogException(logger))
                    {
                    }
                });

        [Fact]
        public void ToAggregateInnerExceptionTest()
        {
            var inner = new Exception("Inner...");
            Assert.Equal(
                inner.Message,
                new AggregateException(inner).ToAggregateInnerException().Message);
        }
    }
}