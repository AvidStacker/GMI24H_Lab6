using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GMI24H_VT25_SortSearch_Labb_
{
    /// <summary>
    /// Provides benchmarking functionality for sorting and searching algorithms.
    /// Results are printed to console and optionally written to a CSV file.
    /// </summary>
    public static class Benchmark
    {
        private const string CsvFilePath = "benchmark_results.csv";

        /// <summary>
        /// Benchmarks a sorting algorithm by executing it multiple times,
        /// measuring execution time, and logging the results.
        /// </summary>
        /// <typeparam name="T">The type of items to be sorted.</typeparam>
        /// <param name="algorithmName">A descriptive name of the algorithm.</param>
        /// <param name="dataGenerator">A delegate that returns a fresh data set for each run.</param>
        /// <param name="sortAlgorithm">The sorting method to benchmark.</param>
        /// <param name="runs">Number of repetitions to perform (default is 100).</param>
        /// <param name="previewCount">Number of items to preview before/after sorting (default is 5).</param>
        public static void MeasureSortExecutionTime<T>(
            string algorithmName,
            Func<IList<T>> dataGenerator,
            Action<IList<T>> sortAlgorithm,
            int runs = 100,
            int previewCount = 5)
        {
            List<long> executionTimes = new();
            IList<T> originalUnsorted = null;
            IList<T> lastSortedRun = null;

            Console.WriteLine($"\n--- Benchmarking Sort: {algorithmName} ({runs} runs) ---");

            // Create CSV header if it doesn't exist
            bool newFile = !File.Exists(CsvFilePath);
            if (newFile)
            {
                using var headerWriter = new StreamWriter(CsvFilePath, append: true);
                headerWriter.WriteLine("Timestamp,Algorithm,Run,TimeMs");
            }

            for (int i = 0; i < runs; i++)
            {
                IList<T> data = dataGenerator();

                // Store original data only on the last run
                if (i == runs - 1)
                {
                    originalUnsorted = new List<T>(data);
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

                using var writer = new StreamWriter(CsvFilePath, append: true);
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{algorithmName},Run {i + 1},{elapsed}");
            }

            double avgTime = executionTimes.Average();
            double stdDev = Math.Sqrt(executionTimes.Average(x => Math.Pow(x - avgTime, 2)));

            Console.WriteLine($"Avg: {avgTime:F2} ms, StdDev: {stdDev:F2} ms över {runs} körningar");

            // Preview original and sorted data
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


        /// <summary>
        /// Benchmarks a search algorithm by executing it multiple times on generated data,
        /// measuring time in microseconds, and printing the result to the console.
        /// </summary>
        /// <typeparam name="T">The type of elements to search within.</typeparam>
        /// <param name="algorithmName">A descriptive name of the algorithm.</param>
        /// <param name="dataGenerator">A function that generates the searchable data.</param>
        /// <param name="searchAlgorithm">The algorithm function that takes data and a target and returns the index.</param>
        /// <param name="targetSelector">A function to select a target value from the data set.</param>
        /// <param name="runs">Number of runs to perform (default is 5).</param>
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
                IList<T> data = dataGenerator().OrderBy(x => x).ToList();
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

            Console.WriteLine($"Avg: {avg:F2} µs, StdDev: {stdDev:F2} µs över {runs} körningar");
            Console.WriteLine($"--- Slut på benchmarking för {algorithmName} ---\n");
        }
    }
}
