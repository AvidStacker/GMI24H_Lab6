using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GMI24H_VT25_SortSearch_Labb_
{
    public static class Benchmark
    {
        private const string LogFilePath = "benchmark_results.txt";

        public static void MeasureExecutionTime<T>(
            string algorithmName,
            Func<IList<T>> dataGenerator,        // Generates fresh data per run
            Action<IList<T>> sortAlgorithm,      // Sorting algorithm that modifies the list
            int runs = 5,
            int previewCount = 5)
        {
            List<long> executionTimes = new();

            Console.WriteLine($"\n--- Benchmarking {algorithmName} ({runs} runs) ---");

            for (int i = 0; i < runs; i++)
            {
                IList<T> data = dataGenerator();  // Fresh list each run
                Stopwatch sw = Stopwatch.StartNew();
                sortAlgorithm(data);              // Sort in-place
                sw.Stop();

                long elapsed = sw.ElapsedMilliseconds;
                executionTimes.Add(elapsed);
                Console.WriteLine($"Run {i + 1}: {elapsed} ms");
            }

            double avgTime = executionTimes.Average();
            double stdDev = Math.Sqrt(executionTimes.Average(x => Math.Pow(x - avgTime, 2)));

            string result = $"{DateTime.Now}: {algorithmName} - Avg: {avgTime:F2} ms, StdDev: {stdDev:F2} ms over {runs} runs";
            File.AppendAllText(LogFilePath, result + Environment.NewLine);

            Console.WriteLine(result);
            Console.WriteLine("Exempel på sorterad data:");
            foreach (var item in dataGenerator().Take(previewCount))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Resultatet loggades till '{LogFilePath}'");
            Console.WriteLine($"--- Slut på benchmarking för {algorithmName} ---\n");
        }
    }
}
