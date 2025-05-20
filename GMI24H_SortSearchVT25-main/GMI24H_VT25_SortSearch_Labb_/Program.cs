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
            Benchmark.MeasureSortExecutionTime(
                "BubbleSort (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.IpAddress).ToList(),
                list => stringSorter.BubbleSort(list),
                runs: 5,
                previewCount: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "BubbleSort (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.StatusCode).ToList(),
                list => intSorter.BubbleSort(list),
                runs: 5,
                previewCount: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "BubbleSort (Timestamps)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.BubbleSort(list),
                runs: 5,
                previewCount: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "MergeSort (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.IpAddress).ToList(),
                list => stringSorter.MergeSort(list, 0, list.Count-1),
                runs: 5,
                previewCount: 5
                );

            Benchmark.MeasureSortExecutionTime(
                "MergeSort (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.StatusCode).ToList(),
                list => intSorter.MergeSort(list, 0, list.Count - 1),
                runs: 5,
                previewCount: 5
                );

            Benchmark.MeasureSortExecutionTime(
                "MergeSort (Timestamps)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.MergeSort(list, 0, list.Count - 1),
                runs: 5,
                previewCount: 5
                );

            Benchmark.MeasureSortExecutionTime(
                "QuickSort (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.IpAddress).ToList(),
                list => stringSorter.QuickSort(list, 0, list.Count-1),
                runs: 5,
                previewCount: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "QuickSort (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.StatusCode).ToList(),
                list => intSorter.QuickSort(list, 0, list.Count - 1),
                runs: 5,
                previewCount: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "QuickSort (Timestamp)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.QuickSort(list, 0, list.Count - 1),
                runs: 5,
                previewCount: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "HeapSort (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.IpAddress).ToList(),
                list => stringSorter.HeapSort(list),
                runs: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "HeapSort (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.StatusCode).ToList(),
                list => intSorter.HeapSort(list),
                runs: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "HeapSort (Timestamps)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.HeapSort(list),
                runs: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "InsertionSort (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.IpAddress).ToList(),
                list => stringSorter.InsertionSort(list),
                runs: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "InsertionSort (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.StatusCode).ToList(),
                list => intSorter.InsertionSort(list),
                runs: 5
            );

            Benchmark.MeasureSortExecutionTime(
                "InsertionSort (Timestamps)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.InsertionSort(list),
                runs: 5
            );

            // Search benchmarks
            // Each data set is sorted beforehand as required by some search algorithms
            Benchmark.MeasureSearchExecutionTime(
                "BinarySearch (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.IpAddress)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => stringSearcher.BinarySearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );
            Benchmark.MeasureSearchExecutionTime(
                "BinarySearch (Status Code)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.StatusCode)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => intSearcher.BinarySearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "BinarySearch (Timestamp)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.Timestamp)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => dateSearcher.BinarySearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "ExponentialSearch (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.IpAddress)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => stringSearcher.ExponentialSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "ExponentialSearch (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.StatusCode)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => intSearcher.ExponentialSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "ExponentialSearch (Timestamp)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.Timestamp)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => dateSearcher.ExponentialSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "InterpolationSearch (IP Adresses)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.IpAddress)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => stringSearcher.InterpolationSearch(list, target, ip => ip.GetHashCode()),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "InterpolationSearch (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.StatusCode)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => intSearcher.InterpolationSearch(list, target, code => code),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "InterpolationSearch (Timestamp)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.Timestamp)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => dateSearcher.InterpolationSearch(list, target, timestamp => timestamp.Ticks),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "JumpSearch (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.IpAddress)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => stringSearcher.JumpSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "JumpSearch (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.StatusCode)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => intSearcher.JumpSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "JumpSearch (Timestamp)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.Timestamp)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => dateSearcher.JumpSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "LinearSearch (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.IpAddress)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => stringSearcher.LinearSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "LinearSearch (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.StatusCode)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => intSearcher.LinearSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );

            Benchmark.MeasureSearchExecutionTime(
                "LinearSearch (Timestamp)",
                () => generator.GenerateLogs(numberOfPosts, seed)
                            .Select(log => log.Timestamp)
                            .OrderBy(ip => ip)
                            .ToList(),
                (list, target) => dateSearcher.LinearSearch(list, target),
                list => list[list.Count / 2],
                runs: 5
            );
        }
    }
}
