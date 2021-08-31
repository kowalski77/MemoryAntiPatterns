using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace MemoryAntiPatterns
{
    [MemoryDiagnoser]
    public class HashSetVsList
    {
        private readonly List<Product> listOfProducts = new();
        private HashSet<Product> hashSetOfProducts = new();

        [Params("product520007", "product520007a")]
        public string productName;

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
        public bool ListContains()
        {
            return this.listOfProducts.Contains(new Product(520007, this.productName, 10));
        }

        [Benchmark]
        public bool HashSetContains()
        {
            return this.hashSetOfProducts.Contains(new Product(520007, this.productName, 10));
        }
    }
}