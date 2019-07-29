using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace Guaranteed.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Guarantees>();
        }

    }



    public static class BaseLines
    {
        public static void IsNotNull(object value)
        {
            if (value == null)
                throw new ArgumentNullException("");
        }

        public static void StringIsNotNullOrEmpty(string value)
        {
            if (value == null)
                throw new ArgumentNullException("test");

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("", "test");
        }

        public static void StringIsNotNullOrWhiteSpace(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Some message 1.", nameof(value));
        }

        public static void StringIsEqualTo(string value, string expected)
        {
            if (!string.Equals(value, expected))
                throw new ArgumentException("Some message 2.", nameof(value));
        }

        public static void StringsHasItems(List<string> strings)
        {
            if (strings.Count == 0)
                throw new ArgumentException("Some message 3.", nameof(strings));
        }

        public static void IntIs(int value, int expected)
        {
            if (value != expected)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Some message 4.");
        }

        public static void IntIsGt(int value, int limit)
        {
            if (value <= limit)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Some message 5.");
        }

        public static void IntsHasItems(List<int> ints)
        {
            if (ints.Count == 0)
                throw new ArgumentException("Some message 6.", nameof(ints));
        }

        public static void ThingIsNotNullViaThat<T>(T thing) where T : class
        {
            if (thing == null)
                throw new ArgumentNullException("werwer");
        }

        public static void ThingsHasItems<T>(List<T> things) where T : class
        {
            if (things.Count == 0)
                throw new ArgumentException("Some message 7.", nameof(things));
        }

        public static void EnumerableSizeIsGt<T>(List<T> things, int v)
        {
            if (things.Count <= v)
                throw new ArgumentException();
        }

        public static void EnumerableSizeIsGte<T>(List<T> things, int v)
        {
            if (things.Count < v)
                throw new ArgumentException();
        }
    }




    [RankColumn]
    [MemoryDiagnoser]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class Guarantees
    {
        private readonly List<string> _strings = new List<string> { "test1", "TEST2", "Test3" };
        private readonly List<int> _ints = new List<int> { 1, 2, 3 };
        private readonly List<MyThing> _things = new List<MyThing>
        {
            new MyThing
            {
                MyInt = 1,
                MyString = "A"
            },
            new MyThing
            {
                MyInt = 2,
                MyString = "B"
            },
            new MyThing
            {
                MyInt = 3,
                MyString = "C"
            }
        };

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Any.IsNotNull")]
        public void ThingIsNotNull_Baseline()
            => BaseLines.IsNotNull(new MyThing());

        [Benchmark]
        [BenchmarkCategory("Any.IsNotNull")]
        public void ThingIsNotNull()
            => Guarantee.That.IsNotNull(new MyThing(), "test");


        // --------------------------------------

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Any.IsNotNullOrEmpty")]
        public void ThingIsNotNullOrEmpty_Baseline()
            => BaseLines.StringIsNotNullOrEmpty("_");

        [Benchmark]
        [BenchmarkCategory("Any.IsNotNullOrEmpty")]
        public void ThingIsNotNullOrEmpty()
            => Guarantee.That.IsNotNullOrEmpty("_", "test");

        // --------------------------------------

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Enumerable.SizeIsGt")]
        public void EnumerableSizeIsGt_Baseline()
        => BaseLines.EnumerableSizeIsGt(_things, 1);

        [Benchmark]
        [BenchmarkCategory("Enumerable.SizeIsGt")]
        public void EnumerableSizeIsGt()
            => Guarantee.That.SizeIsGt(_things, 1);

        // --------------------------------------

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Enumerable.SizeIsGte")]
        public void EnumerableSizeIsGte_Baseline()
            => BaseLines.EnumerableSizeIsGte(_things, 1);

        [Benchmark]
        [BenchmarkCategory("Enumerable.SizeIsGte")]
        public void EnumerableSizeIsGte()
            => Guarantee.That.SizeIsGte(_things, 1);

        // --------------------------------------


        private class MyThing
        {
            public string MyString { get; set; }
            public int MyInt { get; set; }
        }
    }
}
