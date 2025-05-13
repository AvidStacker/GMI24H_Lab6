using AlgorithmLib;
using System.Diagnostics;
using System.Net;


namespace GMI24H_VT25_SortSearch_Labb_
{

    static class Program
    {
        static void Main(string[] args)
        {
            //Här är kod som kan användas om man vill jobba med dataströmmar (som ligger i Generator-katalogen och skapas som ström utifrån en given seed). 
            const int numberOfPosts = 10000;
            const int seed = 123;

            var generator = new RandomLogGenerator();
            var logs = generator.GenerateLogs(numberOfPosts, seed).ToList();


            //Skriver ut de fem första posterna i listan med LogEntry-typer. 
            Console.WriteLine("förhandsvisning av loggdata:");
            foreach (var entry in logs.Take(5))
            {
                Console.WriteLine(entry);
            }

            //Eftersom metoderna i SortingManager och SearchingManager-klasserna inte är statiska så behöver vi instansiera objekt av dessa klasser.
            //Eftersom vi gjort våra Sorting- och SearchingManager-klasserna generiska (<T>) behöver vi även ange vilken typ av data det
            //är som vi vill sortera eller söka efter. Vi anger datatyp i "diamanten" <>.
            var stringSorter = new SortingManager<string>();
            var dateSorter = new SortingManager<DateTime>();
            var intSorter = new SortingManager<int>();
            var searcher = new SearchingManager<string>();

            //Välj vilka data som ska plockas ut ur loggarna och jämföras. T.ex. Int eller strängar. Här behöver
            //vi tänka på att välja samma datatyp som vi vill köra våra algoritmer på, dvs. de vi bestämde oss för
            //när vi instansierade SortingManager och SearchingManager. I det här exemplet är det strängar.
            //Därför skapar vi en lista av strängar dit vi kan spara våra ip-adresser.
            //Vi använder LINQ för att selektera ut ip-adress-propertyn från varje enskilt logentry-post i logs-listan. 
            List<string> ipAddresses = logs.Select(entry => entry.IpAddress).ToList();
            List<DateTime> timestamps = logs.Select(entry => entry.Timestamp).ToList();
            List<int> statusCodes = logs.Select(entry => entry.StatusCode).ToList();

            //Från våra objekt, sorter och searcher, kan vi sedan anropa olika metoder där vi skickar in vår data som parametrar.
            //Det finns ingen implementation av bubblesort i SortingManager just nu. Det här metodanropet är
            //enbart en referens för att visa hur ni kan anropa en metod och skicka er sampledata som ni hämtar 
            //med LogParsern från textfilen. 
            //sorter.BubbleSort(ipAddresses); // <-- implementerar metod från SortingManager-classen som jag vill använda...

            Benchmark.MeasureExecutionTime(
                "BubbleSort (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.IpAddress).ToList(), // regenerates from seed
                list => stringSorter.BubbleSort(list),
                runs: 5
            );

            Benchmark.MeasureExecutionTime(
                "BubbleSort (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.StatusCode).ToList(),
                list => intSorter.BubbleSort(list),
                runs: 5
            );

            Benchmark.MeasureExecutionTime(
                "BubbleSort (Timestamps)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.BubbleSort(list),
                runs: 5
            );

            Benchmark.MeasureExecutionTime(
                "MergeSort (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.IpAddress).ToList(),
                list => stringSorter.MergeSort(list, 0, list.Count-1),
                runs: 5
                );

            Benchmark.MeasureExecutionTime(
                "MergeSort (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.StatusCode).ToList(),
                list => intSorter.MergeSort(list, 0, list.Count - 1),
                runs: 5
                );

            Benchmark.MeasureExecutionTime(
                "MergeSort (Timestamps)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.MergeSort(list, 0, list.Count - 1),
                runs: 5
                );

            Benchmark.MeasureExecutionTime(
                "QuickSort (IP Addresses)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.IpAddress).ToList(),
                list => stringSorter.QuickSort(list, 0, list.Count-1),
                runs: 5
            );

            Benchmark.MeasureExecutionTime(
                "QuickSort (Status Codes)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.StatusCode).ToList(),
                list => intSorter.QuickSort(list, 0, list.Count - 1),
                runs: 5
            );

            Benchmark.MeasureExecutionTime(
                "QuickSort (Timestamp)",
                () => generator.GenerateLogs(numberOfPosts, seed).Select(log => log.Timestamp).ToList(),
                list => dateSorter.QuickSort(list, 0, list.Count - 1),
                runs: 5
            );


            Console.WriteLine($"Totalt antal rader inlästa: {logs.Count}");

        }

    }
}
