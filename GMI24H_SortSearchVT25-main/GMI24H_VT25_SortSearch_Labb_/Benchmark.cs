using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GMI24H_VT25_SortSearch_Labb_
{
    public static class Benchmark
    {
        private const string CsvFilePath = "benchmark_results.csv";

        // 🔁 Benchmark för sorteringsalgoritmer
        public static void MeasureSortExecutionTime<T>(
    string algorithmName,
    Func<IList<T>> dataGenerator,
    Action<IList<T>> sortAlgorithm,
    int runs = 5,
    int previewCount = 5)
        {
            List<long> executionTimes = new();
            IList<T> originalUnsorted = null;
            IList<T> lastSortedRun = null;

            Console.WriteLine($"\n--- Benchmarking Sort: {algorithmName} ({runs} runs) ---");

            // Prepare CSV file
            bool newFile = !File.Exists(CsvFilePath);
            if (newFile)
            {
                using var headerWriter = new StreamWriter(CsvFilePath, append: true);
                headerWriter.WriteLine("Timestamp,Algorithm,Run,TimeMs");
            }

            for (int i = 0; i < runs; i++)
            {
                IList<T> data = dataGenerator();

                // Save unsorted data before sorting (only for last run)
                if (i == runs - 1)
                {
                    originalUnsorted = new List<T>(data); // Make a copy
                }

                Stopwatch sw = Stopwatch.StartNew();
                sortAlgorithm(data);
                sw.Stop();

                if (i == runs - 1)
                {
                    lastSortedRun = data;
                }

                long elapsed = sw.ElapsedMilliseconds;
                executionTimes.Add(elapsed);

                Console.WriteLine($"Run {i + 1}: {elapsed} ms");

                // Append result to CSV
                using var writer = new StreamWriter(CsvFilePath, append: true);
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{algorithmName},Run {i + 1},{elapsed}");
            }

            // Final stats
            double avgTime = executionTimes.Average();
            double stdDev = Math.Sqrt(executionTimes.Average(x => Math.Pow(x - avgTime, 2)));

            Console.WriteLine($"Avg: {avgTime:F2} ms, StdDev: {stdDev:F2} ms över {runs} körningar");

            // Show data preview
            Console.WriteLine("\nExempel på dom första 5 raderna av den sorterad listan:");
            foreach (var item in originalUnsorted.Take(previewCount))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nExempel på dom första 5 raderna av den sorterad listan:");
            foreach (var item in lastSortedRun.Take(previewCount))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"Resultatet loggades till '{CsvFilePath}'");
            Console.WriteLine($"--- Slut på benchmarking för {algorithmName} ---\n");
        }


        // 🔍 Benchmark för sökalgoritmer
        public static void MeasureSearchExecutionTime<T>(
            string algorithmName,
            Func<IList<T>> dataGenerator,
            Func<IList<T>, T, int> searchAlgorithm,
            Func<IList<T>, T> targetSelector,
            int runs = 5)
        {
            List<long> executionTimes = new();

            Console.WriteLine($"\n--- Benchmarking Search: {algorithmName} ({runs} runs) ---");

            for (int i = 0; i < runs; i++)
            {
                IList<T> data = dataGenerator().OrderBy(x => x).ToList(); // Sortera för binärsökning etc.
                T target = targetSelector(data);

                Stopwatch sw = Stopwatch.StartNew();
                int result = searchAlgorithm(data, target);
                sw.Stop();

                long ticks = sw.ElapsedTicks;
                double microseconds = (double)ticks / Stopwatch.Frequency * 1_000_000;
                executionTimes.Add((long)microseconds);

                Console.WriteLine($"Run {i + 1}: {microseconds:F2} µs, Resultatindex: {result}");
            }

            double avg = executionTimes.Average();
            double stdDev = Math.Sqrt(executionTimes.Average(x => Math.Pow(x - avg, 2)));

            Console.WriteLine($"Avg: {avg:F2} ms, StdDev: {stdDev:F2} ms över {runs} körningar");
            Console.WriteLine($"--- Slut på benchmarking för {algorithmName} ---\n");
        }
    }
}
