using AlgorithmLib;
using System.Diagnostics;
using System.Net;


namespace GMI24H_VT25_SortSearch_Labb_
{
    /// <summary>
    /// Entry point of the application. Runs a series of benchmarks on sorting and searching algorithms
    /// using generated HTTP log data.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Main method that executes all sorting and searching benchmarks.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Configuration
            const int numberOfPosts = 10000;
            const int seed = 123;

            // Initialize log data generator
            var generator = new RandomLogGenerator();
            var logs = generator.GenerateLogs(numberOfPosts, seed).ToList();

            // Preview a few log entries
            Console.WriteLine("förhandsvisning av loggdata:");
            foreach (var entry in logs.Take(5))
            {
                Console.WriteLine(entry);
            }

            // Instantiate sorting and searching managers for different data types
            var stringSorter = new SortingManager<string>();
            var intSorter = new SortingManager<int>();
            var dateSorter = new SortingManager<DateTime>();
            var stringSearcher = new SearchingManager<string>();
            var intSearcher = new SearchingManager<int>();
            var dateSearcher = new SearchingManager<DateTime>();

            // Extract fields from logs to benchmark
            List<string> ipAddresses = logs.Select(entry => entry.IpAddress).ToList();
            List<int> statusCodes = logs.Select(entry => entry.StatusCode).ToList();
            List<DateTime> timestamps = logs.Select(entry => entry.Timestamp).ToList();

            // Sorting benchmarks
            // Each benchmark re-generates data from the same seed to ensure consistency

            // --- RANDOM DATA ---
            Benchmark.MeasureSortExecutionTime(
                "BubbleSort - Random",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.BubbleSort(list),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "MergeSort - Random",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.MergeSort(list, 0, list.Count - 1),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "InsertionSort - Random",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.InsertionSort(list),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "SelectionSort - Random",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.SelectionSort(list),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "HeapSort - Random",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.HeapSort(list),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "QuickSort - Random",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.QuickSort(list, 0, list.Count - 1),
                runs: 100
            );

            // --- SORTED DATA ---
            Benchmark.MeasureSortExecutionTime(
                "BubbleSort - Sorted",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).OrderBy(t => t).ToList(),
                list => dateSorter.BubbleSort(list),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "MergeSort - Sorted",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).OrderBy(t => t).ToList(),
                list => dateSorter.MergeSort(list, 0, list.Count - 1),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "InsertionSort - Sorted",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).OrderBy(t => t).ToList(),
                list => dateSorter.InsertionSort(list),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "SelectionSort - Sorted",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).OrderBy(t => t).ToList(),
                list => dateSorter.SelectionSort(list),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "HeapSort - Sorted",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).OrderBy(t => t).ToList(),
                list => dateSorter.HeapSort(list),
                runs: 100
            );

            Benchmark.MeasureSortExecutionTime(
                "QuickSort - Sorted",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).OrderBy(t => t).ToList(),
                list => dateSorter.QuickSort(list, 0, list.Count - 1),
                runs: 100
            );

            // Search benchmarks
            // Each data set is sorted beforehand as required by some search algorithms
            Benchmark.MeasureSearchExecutionTime(
                "BinarySearch (Status Code) - first item",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.BinarySearch(list, target),
                list => list[0],
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "ExponentialSearch (Status Code) - first item",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.ExponentialSearch(list, target),
                list => list[0],
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "InterpolationSearch (Status Code) - not found",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.InterpolationSearch(list, target, code => code),
                _ => -1,
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "JumpSearch (Status Code) - not found",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.JumpSearch(list, target),
                _ => -1,
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "LinearSearch (Status Code) - not found",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .ToList(),
                (list, target) => intSearcher.LinearSearch(list, target),
                _ => -1,
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "LinearSearch (Status Code) - first item",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.LinearSearch(list, target),
                list => list[0],
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "LinearSearch (Status Code) - last item",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.LinearSearch(list, target),
                list => list[^1],
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "LinearSearch (Status Code) - not found (duplicate check)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .ToList(),
                (list, target) => intSearcher.LinearSearch(list, target),
                _ => -999,
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "BinarySearch (Status Code) - first item (duplicate)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.BinarySearch(list, target),
                list => list[0],
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "BinarySearch (Status Code) - last item",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.BinarySearch(list, target),
                list => list[^1],
                runs: 100
            );

            Benchmark.MeasureSearchExecutionTime(
                "BinarySearch (Status Code) - not found",
                () => generator.GenerateLogs(numberOfPosts, seed)
                    .Select(log => log.StatusCode)
                    .OrderBy(code => code)
                    .ToList(),
                (list, target) => intSearcher.BinarySearch(list, target),
                _ => -1,
                runs: 100
            );
        }
    }
}
