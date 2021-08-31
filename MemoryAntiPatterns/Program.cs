using System;
using System.Diagnostics;
using BenchmarkDotNet.Running;

namespace MemoryAntiPatterns
{
    internal static class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run<SortedListVsSortedDictionaryInsertion>();

            //var hsVsList = new HashSetVsList();
            //hsVsList.Setup();

            //var sw = new Stopwatch();
            //sw.Start();

            //Console.WriteLine(hsVsList.HashSetContains());
            //Console.WriteLine(sw.ElapsedMilliseconds);

            //sw.Stop();

            //sw.Start();

            //Console.WriteLine(hsVsList.ListContains());
            //Console.WriteLine(sw.ElapsedMilliseconds);

            //sw.Stop();
        }
    }
}