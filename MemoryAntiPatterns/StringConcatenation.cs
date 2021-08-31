using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace MemoryAntiPatterns
{
    [MemoryDiagnoser]
    public class StringConcatenation
    {
        private const int Count = 1000;

        [Benchmark]
        public string ConcatStrings()
        {
            var result = string.Empty;
            for (var i = 0; i < Count; i++)
            {
                result += i;
            }

            return result;
        }

        [Benchmark]
        public string ConcatString2()
        {
            var result = string.Empty;
            for (var i = 0; i < Count; i++)
            {
                result = string.Concat(result, i);
            }

            return result;
        }

        [Benchmark]
        public string InterpolateStrings()
        {
            var result = string.Empty;
            for (var i = 0; i < Count; i++)
            {
                result = $"{result}{i}";
            }

            return result;
        }

        [Benchmark]
        public string BuildStrings()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Count; i++)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }
    }
}