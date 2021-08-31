using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace MemoryAntiPatterns
{
    [MemoryDiagnoser]
    public class ConcurrentDictionaryPerformance
    {
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
        public ConcurrentDictionary<int, string> WithConcurrentDictionary()
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
        public ReadOnlyDictionary<int, string> WithReadOnlyDictionary()
        {
            var dictionary = new ConcurrentDictionary<int, string>();
            Parallel.ForEach(this.products, product =>
            {
                if (product.Id % 2 == 0)
                {
                    dictionary.TryAdd(product.Id, product.Name);
                }
            });

            return new ReadOnlyDictionary<int, string>(dictionary);
        }

        [Benchmark]
        public ImmutableDictionary<int, string> WithImmutableDictionary()
        {
            var dictionary = new ConcurrentDictionary<int, string>();
            Parallel.ForEach(this.products, product =>
            {
                if (product.Id % 2 == 0)
                {
                    dictionary.TryAdd(product.Id, product.Name);
                }
            });

            return dictionary.ToImmutableDictionary();
        }
    }
}