using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace MemoryAntiPatterns
{
    [MemoryDiagnoser]
    public class SortedListVsSortedDictionaryInsertion
    {
        [Benchmark]
        public IDictionary<int, Product> SortedListAdd()
        {
            SortedList<int, Product> products = new();

            products.Add(10, new Product(2, "product", 10));
            products.Add(9, new Product(2, "product", 10));
            products.Add(8, new Product(2, "product", 10));
            products.Add(7, new Product(2, "product", 10));
            products.Add(6, new Product(2, "product", 10));
            products.Add(1, new Product(2, "product", 10));
            products.Add(2, new Product(2, "product", 10));
            products.Add(3, new Product(2, "product", 10));
            products.Add(5, new Product(2, "product", 10));
            products.Add(4, new Product(2, "product", 10));

            return products;
        }

        [Benchmark]
        public IDictionary<int, Product> SortedDictionaryAdd()
        {
            SortedDictionary<int, Product> products = new();

            products.Add(10, new Product(2, "product", 10));
            products.Add(9, new Product(2, "product", 10));
            products.Add(8, new Product(2, "product", 10));
            products.Add(7, new Product(2, "product", 10));
            products.Add(6, new Product(2, "product", 10));
            products.Add(1, new Product(2, "product", 10));
            products.Add(2, new Product(2, "product", 10));
            products.Add(3, new Product(2, "product", 10));
            products.Add(5, new Product(2, "product", 10));
            products.Add(4, new Product(2, "product", 10));

            return products;
        }
    }
}