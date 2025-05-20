namespace GMI24H_VT25_SortSearch_Labb_
{
    /// <summary>
    /// Defines a contract for log generators capable of producing a sequence of log entries.
    /// </summary>
    public interface ILogGenerator
    {
        /// <summary>
        /// Generates a specified number of log entries using a deterministic random seed.
        /// </summary>
        /// <param name="count">The number of log entries to generate.</param>
        /// <param name="seed">An integer seed to ensure repeatable pseudo-random generation.</param>
        /// <returns>An enumerable sequence of <see cref="LogEntry"/> objects.</returns>
        IEnumerable<LogEntry> GenerateLogs(int count, int seed);
    }
}
