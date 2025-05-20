using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI24H_VT25_SortSearch_Labb_
{
    /// <summary>
    /// Represents a single entry in a log file, containing details about an HTTP request.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Gets or sets the timestamp indicating when the request occurred.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Gets or sets the IP address from which the request originated.
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Gets or sets the HTTP method used (e.g., GET, POST, PUT).
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Gets or sets the requested URL path.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets the HTTP response status code (e.g., 200, 404, 500).
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Returns a formatted string representing the log entry.
        /// </summary>
        /// <returns>A string containing all log entry properties in one line.</returns>
        public override string ToString()
        {
            return $"{Timestamp} {IpAddress} {Method} {Path} {StatusCode}";
        }
    }
}
