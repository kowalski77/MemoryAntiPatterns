using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace MemoryAntiPatterns
{
    [MemoryDiagnoser]
    public class HashSetVsListLinq
    {
        private const int Id = 520007;

        private readonly List<Product> listOfProducts = new();
        private HashSet<Product> hashSetOfProducts = new();

        [GlobalSetup]
        public void Setup()
        {
            for (var i = 0; i < 1000000; i++)
            {
                this.listOfProducts.Add(new Product(i, $"product{i}", 10));
            }

            this.hashSetOfProducts = new HashSet<Product>(this.listOfProducts);
        }

        [Benchmark]
        public bool ListFirstOrDefaultExisting()
        {
            return this.listOfProducts.FirstOrDefault(x => x.Id == Id) != null;
        }

        [Benchmark]
        public bool SingleFirstOrDefaultExisting()
        {
            return this.listOfProducts.SingleOrDefault(x => x.Id == Id) != null;
        }

        [Benchmark]
        public bool HashSetFirstOrDefaultExisting()
        {
            return this.hashSetOfProducts.FirstOrDefault(x => x.Id == Id) != null;
        }

        [Benchmark]
        public bool HashSetSingleOrDefaultExisting()
        {
            return this.hashSetOfProducts.SingleOrDefault(x => x.Id == Id) != null;
        }
    }
}