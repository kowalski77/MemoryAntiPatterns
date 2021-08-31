using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace MemoryAntiPatterns
{
    [MemoryDiagnoser]
    public class DictionaryPerformance
    {
        private static readonly object DictionaryLock = new();

        private readonly List<Product> products = new();

        [Params(1000, 10000)] 
        public int count;

        [GlobalSetup]
        public void Setup()
        {
            for (var i = 0; i < this.count; i++)
            {
                this.products.Add(new Product(i, $"product{i}", 10));
            }
        }

        [Benchmark]
        public IDictionary<int, string> WithForeachAndDictionary()
        {
            var dictionary = new Dictionary<int, string>();
            foreach (var product in this.products)
            {
                if (product.Id % 2 == 0)
                {
                    dictionary.Add(product.Id, product.Name);
                }
            }

            return dictionary;
        }

        [Benchmark]
        public IDictionary<int, string> WithLinqAndToDictionary()
        {
            return this.products
                .Where(product => product.Id % 2 == 0)
                .ToDictionary(product => product.Id, product => product.Name);
        }

        [Benchmark]
        public IDictionary<int, string> WithParallelForeachAndLockAndDictionary()
        {
            var dictionary = new Dictionary<int, string>();
            Parallel.ForEach(this.products, product =>
            {
                if (product.Id % 2 == 0)
                {
                    lock (DictionaryLock)
                    {
                        dictionary.Add(product.Id, product.Name);
                    }
                }
            });

            return dictionary;
        }

        [Benchmark]
        public IDictionary<int, string> WithParallelForeachAndConcurrentDictionary()
        {
            var dictionary = new ConcurrentDictionary<int, string>();
            Parallel.ForEach(this.products, product =>
            {
                if (product.Id % 2 == 0)
                {
                    dictionary.TryAdd(product.Id, product.Name);
                }
            });

            return dictionary;
        }

        [Benchmark]
        public IDictionary<int, string> WithParallelForeachAndConcurrentDictionaryAndInitialCapacityAndConcurrencyLevel()
        {
            var concurrencyLevel = Environment.ProcessorCount * 2;
            var dictionary = new ConcurrentDictionary<int, string>(concurrencyLevel, this.count / 2);
            Parallel.ForEach(this.products, product =>
            {
                if (product.Id % 2 == 0)
                {
                    dictionary.TryAdd(product.Id, product.Name);
                }
            });

            return dictionary;
        }
    }
}