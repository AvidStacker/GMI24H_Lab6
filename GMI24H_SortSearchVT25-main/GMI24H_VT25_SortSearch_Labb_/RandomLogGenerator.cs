using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI24H_VT25_SortSearch_Labb_
{
    /// <summary>
    /// Provides an implementation of <see cref="ILogGenerator"/> that produces a sequence of log entries
    /// using pseudo-random values based on a provided seed.
    /// This enables reproducible test scenarios with controlled randomness.
    /// </summary>
    public class RandomLogGenerator : ILogGenerator
    {
        /// <summary>
        /// Generates a specified number of <see cref="LogEntry"/> instances using pseudo-random values.
        /// The sequence is repeatable for a given <paramref name="seed"/>.
        /// </summary>
        /// <param name="count">The number of log entries to generate.</param>
        /// <param name="seed">An integer seed to ensure reproducible randomness.</param>
        /// <returns>An enumerable collection of generated <see cref="LogEntry"/> objects.</returns>
        public IEnumerable<LogEntry> GenerateLogs(int count, int seed)
        {
            var rand = new Random(seed);
            var startTime = new DateTime(2025, 5, 1, 8, 0, 0);

            string[] ipAddresses = { "192.168.1.10", "10.0.0.5", "127.0.0.1" };
            string[] methods = { "GET", "POST", "PUT" };
            string[] paths = { "/", "/login", "/api" };
            int[] statusCodes = { 200, 401, 500 };

            for (int i = 0; i < count; i++)
            {
                yield return new LogEntry
                {
                    Timestamp = startTime.AddSeconds(rand.Next(0, 86400)),
                    IpAddress = ipAddresses[rand.Next(ipAddresses.Length)],
                    Method = methods[rand.Next(methods.Length)],
                    Path = paths[rand.Next(paths.Length)],
                    StatusCode = statusCodes[rand.Next(statusCodes.Length)]
                };
            }
        }
    }
}
