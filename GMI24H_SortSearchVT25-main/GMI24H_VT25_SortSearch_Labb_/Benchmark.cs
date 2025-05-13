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

            Console.WriteLine($"\n--- Benchmarking Sort: {algorithmName} ({runs} runs) ---");

            // Skapa CSV-fil med header om den inte finns
            bool newFile = !File.Exists(CsvFilePath);
            using (var writer = new StreamWriter(CsvFilePath, append: true))
            {
                if (newFile)
                {
                    writer.WriteLine("Timestamp,Algorithm,Run,TimeMs");
                }

                for (int i = 0; i < runs; i++)
                {
                    IList<T> data = dataGenerator();
                    Stopwatch sw = Stopwatch.StartNew();
                    sortAlgorithm(data);
                    sw.Stop();

                    long elapsed = sw.ElapsedMilliseconds;
                    executionTimes.Add(elapsed);

                    Console.WriteLine($"Run {i + 1}: {elapsed} ms");

                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{algorithmName},Run {i + 1},{elapsed}");
                }
            }

            double avgTime = executionTimes.Average();
            double stdDev = Math.Sqrt(executionTimes.Average(x => Math.Pow(x - avgTime, 2)));

            Console.WriteLine($"Avg: {avgTime:F2} ms, StdDev: {stdDev:F2} ms över {runs} körningar");
            Console.WriteLine("Exempel på sorterad data:");
            foreach (var item in dataGenerator().Take(previewCount))
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

                long elapsed = sw.ElapsedMilliseconds;
                executionTimes.Add(elapsed);

                Console.WriteLine($"Run {i + 1}: {elapsed} ms, Resultatindex: {result}");
            }

            double avg = executionTimes.Average();
            double stdDev = Math.Sqrt(executionTimes.Average(x => Math.Pow(x - avg, 2)));

            Console.WriteLine($"Avg: {avg:F2} ms, StdDev: {stdDev:F2} ms över {runs} körningar");
            Console.WriteLine($"--- Slut på benchmarking för {algorithmName} ---\n");
        }
    }
}
