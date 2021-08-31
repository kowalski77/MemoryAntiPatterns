using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace MemoryAntiPatterns
{
    [MemoryDiagnoser]
    public class MaterializedQuery
    {
        private IEnumerable<Product> products;

        [GlobalSetup]
        public void Setup()
        {
            this.products = GetProducts();
        }

        [Benchmark]
        public List<Product> NonMaterializeList()
        {
            var finalList = new List<Product>();
            var productList = this.products.Where(x => x.Id % 2 == 0);
            foreach (var product in productList)
            {
                product.Price = 20;
                finalList.Add(product);
            }

            return finalList;
        }

        [Benchmark]
        public List<Product> MaterializeList()
        {
            var finalList = new List<Product>();
            var productList = this.products.Where(x => x.Id % 2 == 0).ToList();
            foreach (var product in productList)
            {
                product.Price = 20;
                finalList.Add(product);
            }

            return finalList;
        }

        private static IEnumerable<Product> GetProducts()
        {
            for (var i = 0; i < 10000; i++)
                yield return new Product(i, $"product{i}", 10);
        }
    }
}