// Copyright (c) 2018 Centare

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Centare.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class TypeExtensionTests
    {
        internal abstract class Abstract { }

        public static List<object[]> IsInstantiableInput => new List<object[]>
        {
            new object[] { typeof(Dictionary<string, string>), true },
            new object[] { typeof(Dictionary<string, string>).GetGenericTypeDefinition(), false },
            new object[] { typeof(Abstract), false },
            new object[] { typeof(IAsyncResult), false },
        };

        [Theory, MemberData(nameof(IsInstantiableInput))]
        public void IsInstantiableTests(Type type, bool isInstantiable)
        {
            var result = type.IsInstantiable();

            Assert.Equal(isInstantiable, result);
        }

        [Theory, InlineData(typeof(ITestInterface), typeof(TestImplementor))]
        public void GetImplementorsTest(Type type, Type implementor)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var result = type.GetImplementors(assembly).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal(implementor, result.FirstOrDefault(r => !r.IsInterface));
        }

        public interface ITestInterface
        {
            Lazy<Task> WakeUpAsync();
        }

        public class TestImplementor : ITestInterface
        {
            public Lazy<Task> WakeUpAsync() => new Lazy<Task>(() => Task.CompletedTask);
        }

        public static List<object[]> InstantiableImplementorsInput => new List<object[]>
        {
            new object[] { typeof(ITestInterface), typeof(TestImplementor), true },
            new object[] { typeof(IAsyncResult), typeof(int), false }
        };

        [Theory, MemberData(nameof(InstantiableImplementorsInput))]
        public void GetInstantiableImplementors(Type type, Type implementor, bool isInstantiable)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var result = type.GetInstantiableImplementors(assembly).ToList();

            if (isInstantiable)
            {
                Assert.Single(result);
                Assert.Equal(implementor, result[0]);
            }
            else
            {
                Assert.Empty(result);
            }
        }

        public static List<object[]> ToDictionaryInput => new List<object[]>
        {
            new object[]
            {
                typeof(DayOfWeek),
                new Dictionary<string, DayOfWeek>
                {
                    [nameof(DayOfWeek.Sunday)] = DayOfWeek.Sunday,
                    [nameof(DayOfWeek.Monday)] = DayOfWeek.Monday,
                    [nameof(DayOfWeek.Tuesday)] = DayOfWeek.Tuesday,
                    [nameof(DayOfWeek.Wednesday)] = DayOfWeek.Wednesday,
                    [nameof(DayOfWeek.Thursday)] = DayOfWeek.Thursday,
                    [nameof(DayOfWeek.Friday)] = DayOfWeek.Friday,
                    [nameof(DayOfWeek.Saturday)] = DayOfWeek.Saturday
                }
            },
            new object[]
            {
                typeof(DateTimeKind),
                new Dictionary<string, DateTimeKind>
                {
                    [nameof(DateTimeKind.Unspecified)] = DateTimeKind.Unspecified,
                    [nameof(DateTimeKind.Local)] = DateTimeKind.Local,
                    [nameof(DateTimeKind.Utc)] = DateTimeKind.Utc
                }
            }
        };

        [Theory, MemberData(nameof(ToDictionaryInput))]
        public void ToDictionaryTests<TEnum>(Type type, Dictionary<string, TEnum> expected)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var actual = type.ToDictionary<TEnum>();
            Assert.Equal(expected.Count, actual.Count);
            foreach (var kvp in expected)
            {
                Assert.Equal(kvp.Value, actual[kvp.Key]);
            }
        }
    }
}