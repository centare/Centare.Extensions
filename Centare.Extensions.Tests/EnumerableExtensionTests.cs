// Copyright (c) 2018 Centare

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Centare.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class EnumerableExtensionTests
    {
        public static List<object[]> EnumerableInput 
            => new List<object[]>
               {
                   new object[] {new[] {1, 2, 3, 4, 5}, false},
                   new object[] {null, true},
                   new object[] {Enumerable.Empty<int>(), true}
               };

        [Theory, MemberData(nameof(EnumerableInput))]
        public void TestIsNullOrEmpty(IEnumerable<int> numbers, bool expected) 
            => Assert.Equal(expected, numbers.IsNullOrEmpty());

        [Fact]
        public void TestDistinctBy()
        {
            var list = new[] { 1, 1, 1, 1, 1, 2, 2, 3, 4, 5, 6, 7, 7, 7 };

            var distinct = list.DistinctBy(i => i).ToList();

            Assert.Equal(7, distinct.Count);
        }

        [Fact]
        public void EnqueueTests()
        {
            var q = new Queue<int>();
            q.Enqueue(new[] { 1, 2, 3 });
            Assert.Equal(3, q.Count);

            q.Enqueue(null);
            Assert.Equal(3, q.Count);

            q.Enqueue(new List<int>());
            Assert.Equal(3, q.Count);

            q.Enqueue(new[] { 4, 5, 6 });
            Assert.Equal(6, q.Count);

            for (int i = 1; i <= 6; ++ i)
            {
                Assert.Equal(i, q.Dequeue());
            }
        }

        [Fact]
        public void ConcurrentEnqueueTests()
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(new[] { 1, 2, 3 });
            Assert.Equal(3, q.Count);

            q.Enqueue(null);
            Assert.Equal(3, q.Count);

            q.Enqueue(new List<int>());
            Assert.Equal(3, q.Count);

            q.Enqueue(new[] { 4, 5, 6 });
            Assert.Equal(6, q.Count);

            for (int i = 1; i <= 6; ++ i)
            {
                Assert.True(q.TryDequeue(out var value));
                Assert.Equal(i, value);
            }
        }

        [Fact]
        public void DequeueWhileTests()
        {
            Queue<int> q = null;
            Assert.Equal(q, q.DequeueWhile(i => i % 2 == 0));

            q = new Queue<int>();
            Assert.Equal(q, q.DequeueWhile(i => i != 0));

            q.Enqueue(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })
             .DequeueWhile(i => i < 5);

            Assert.Equal(5, q.Count);

            for (int i = 5; i < 10; ++ i)
            {
                Assert.Equal(i, q.Dequeue());
            }
        }

        [Fact]
        public void ConcurrentDequeueWhileTests()
        {
            ConcurrentQueue<int> q = null;
            Assert.Equal(q, q.DequeueWhile(i => i % 2 == 0));

            q = new ConcurrentQueue<int>();
            Assert.Equal(q, q.DequeueWhile(i => i != 0));

            q.Enqueue(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })
             .DequeueWhile(i => i < 5);

            Assert.Equal(5, q.Count);

            for (int i = 5; i < 10; ++ i)
            {
                Assert.True(q.TryDequeue(out var value));
                Assert.Equal(i, value);
            }
        }

        [Fact]
        public void PeekOrDefaultTests()
        {
            Queue<int> q = null;
            Assert.Equal(0, q.PeekOrDefault());

            q = new Queue<int>(new[] { 1, 2, 3 });
            Assert.Equal(1, q.PeekOrDefault());
            Assert.Equal(1, q.PeekOrDefault());

            q.Dequeue();
            Assert.Equal(2, q.PeekOrDefault());
        }

        [Fact]
        public void ConcurrentPeekOrDefaultTests()
        {
            ConcurrentQueue<int> q = null;
            Assert.Equal(0, q.TryPeekOrDefault());

            q = new ConcurrentQueue<int>(new[] { 1, 2, 3 });
            Assert.Equal(1, q.TryPeekOrDefault());
            Assert.Equal(1, q.TryPeekOrDefault());

            q.TryDequeue(out var value);
            Assert.Equal(2, q.TryPeekOrDefault());
        }

        [Fact]
        public void MergeUniqueLatestTests()
        {
            var startDateTime = DateTime.Now;
            var sourceOne = new(int Id, double Value, DateTime DateTime)[]
            {
                (1, 100.1, startDateTime),
                (2, 100.2, startDateTime),
                (3, 100.3, startDateTime),
                (4, 100.4, startDateTime),
                (5, 100.5, startDateTime),
                (7, 100.7, startDateTime.AddSeconds(5)),
                (9, 100.9, startDateTime)
            };
            startDateTime = startDateTime.AddSeconds(2.5);
            var sourceTwo = new(int Id, double Value, DateTime DateTime)[]
            {
                (1, 200.1, startDateTime),
                (2, 200.2, startDateTime.AddSeconds(-5)),
                (3, 200.3, startDateTime),
                (4, 200.4, startDateTime),
                (5, 200.5, startDateTime),
                (7, 200.7, startDateTime),
                (8, 200.8, startDateTime)
            };

            var result = sourceOne.MergeUniqueLatest(sourceTwo, a => a.Id, a => a.DateTime).ToList();
            Assert.Equal(8, result.Count);

            Assert.Equal(200.1, result.First(a => a.Id == 1).Value);
            Assert.Equal(100.2, result.First(a => a.Id == 2).Value);
            Assert.Equal(200.3, result.First(a => a.Id == 3).Value);
            Assert.Equal(200.4, result.First(a => a.Id == 4).Value);
            Assert.Equal(200.5, result.First(a => a.Id == 5).Value);
            Assert.Equal(100.7, result.First(a => a.Id == 7).Value);
            Assert.Equal(200.8, result.First(a => a.Id == 8).Value);
            Assert.Equal(100.9, result.First(a => a.Id == 9).Value);
        }

        [Fact]
        public void AsListTest()
        {
            Assert.True(52.AsList().Count == 1);
            Assert.True(new DateTime().AsList().Count == 1);
            Assert.True(((object)null).AsList().Count == 1);
        }

        [Fact]
        public void PushRangeTest()
        {
            var stack = new Stack<int>();
            Assert.True(stack.IsNullOrEmpty());
            stack.PushRange(new[] { 1, 2, 3 });
            Assert.Equal(3, stack.Count);
            Assert.Equal(3, stack.Peek());

            stack.PushRange(null);
            Assert.Equal(3, stack.Count);
        }

        [Fact]
        public void MaxByTest()
        {
            var list = new[] { 5, 4, 2, 3, 7, 1 };
            
            Assert.Equal(7, list.MaxBy(i => i));
            Assert.Equal(7, list.MaxBy(i => i, null));
        }

        public static List<object[]> JoinInput
            => new List<object[]>
            {
                new object[] { new[] { 9, 8, 7 }, new[] { 1, 7, 10, 14, 99 }, 1 },
                new object[] { new[] { 9, 8, 7 }, new[] { 8, 7, 9 }, 3 },
                new object[] { new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, 0 }
            };

        [
            Theory,
            MemberData(nameof(JoinInput))
        ]
        public void JoinTest(IEnumerable<int> left, IEnumerable<int> right, int expected)
            => Assert.Equal(expected, left.Join(right, i => i).Count());

        public static List<object[]> NaturalOrderInput
            => new List<object[]>
            {
                new object[]
                {
                    new[] { "a4", "a3", "a2", "a10", "b5", "b4", "b400", "1", "C1d", "c1d2" },
                    new[] { "1", "a2", "a3", "a4", "a10", "b4", "b5", "b400", "C1d", "c1d2" }
                }
            };

        [
            Theory,
            MemberData(nameof(NaturalOrderInput))
        ]
        public void NaturalOrderByTests(IEnumerable<string> input, ICollection<string> expected)
        {
            var actual = input.NaturalOrderBy(str => str).ToList();
            Assert.Equal(expected.Count, actual.Join(expected, s => s, s => s, (_, __) => new { _ }).Count());
        }

        [Fact]
        public void ToDistinctDictionaryTest()
        {
            var names = new [] { "David", "David", "David", "Jen", "Jen" };
            var dictionary = names.ToDistinctDictionary(str => str, str => str);

            Assert.Equal(2, dictionary.Count);
            Assert.Equal("David", dictionary["David"]);
            Assert.Equal("Jen", dictionary["Jen"]);
        }

        [Fact]
        public void ToEnumerableTest()
        {
            var enumerable = 7.ToEnumerable();

            Assert.NotNull(enumerable);
            Assert.Contains(enumerable, n => n == 7);
        }

        [Fact]
        public void AppendTest()
        {
            var list = new[] { "David", "Michael" };
            var appended = list.Append("Pine");

            Assert.Equal(3, appended.Count());
        }
    }
}