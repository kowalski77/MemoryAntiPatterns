using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace MemoryAntiPatterns
{
    [MemoryDiagnoser]
    public class ListCapacity
    {
        private const int Count = 10000;

        [Benchmark]
        public List<Product> NoInitializedCapacity()
        {
            var list = new List<Product>();
            for (var i = 0; i < Count; i++)
            {
                list.Add(new Product(i, $"product{i}", i + 10));
            }

            return list;
        }

        [Benchmark]
        public List<Product> InitializedCapacity()
        {
            var list = new List<Product>(Count);
            for (var i = 0; i < Count; i++)
            {
                list.Add(new Product(i, $"product{i}", i + 10));
            }

            return list;
        }
    }
}