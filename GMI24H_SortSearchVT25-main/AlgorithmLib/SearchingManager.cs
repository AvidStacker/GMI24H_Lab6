using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLib
{
    /// <summary>
    /// Provides generic implementations of various search algorithms for lists.
    /// All algorithms operate on types that implement IComparable&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T">The type of elements to search. Must be comparable.</typeparam>

    public class SearchingManager<T> : ISearchingManager<T> where T : IComparable<T>
    {
        /// <summary>
        /// Performs a binary search on a sorted list.
        /// </summary>
        /// <param name="collection">A list sorted in ascending order.</param>
        /// <param name="target">The element to search for.</param>
        /// <returns>The index of the found element; otherwise, -1.</returns>
        public int BinarySearch(IList<T> collection, T target)
        {
            if (collection == null || target == null)
                throw new ArgumentNullException("Collection or target is null.");
            if (collection.Count == 0)
                throw new ArgumentException("Collection is empty.");

            int left = 0;
            int right = collection.Count - 1;

            while (left <= right)
            {
                int middle = left + (right - left) / 2;
                int comparison = target.CompareTo(collection[middle]);

                if (comparison == 0)
                {
                    return middle;
                }
                else if (comparison < 0)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }

            return -1; // Target not found
        }


        /// <summary>
        /// Performs exponential search on a sorted list.
        /// Efficient for data where the target is likely to be located near the start of the list.
        /// </summary>
        /// <param name="collection">A list sorted in ascending order.</param>
        /// <param name="target">The element to search for.</param>
        /// <returns>The index of the found element; otherwise, -1.</returns>
        public int ExponentialSearch(IList<T> collection, T target)
        {
            if (collection == null || target == null)
                throw new ArgumentNullException("Collection or target is null.");
            if (collection.Count == 0)
                throw new ArgumentException("Collection is empty.");

            if (collection[0].Equals(target))
            {
                return 0;
            }

            int bound = 1;
            while (bound < collection.Count && collection[bound].CompareTo(target) < 0)
            {
                bound *= 2;
            }

            return BinarySearch(collection, target);
        }

        /// <summary>
        /// Performs interpolation search on a sorted list of numeric data.
        /// Best suited for uniformly distributed data types such as integers.
        /// </summary>
        /// <param name="collection">A list sorted in ascending order.</param>
        /// <param name="target">The element to search for.</param>
        /// <param name="selector">A function that maps the elements to numeric (long) values for interpolation calculation.</param>
        /// <returns>The index of the found element; otherwise, -1.</returns>
        public int InterpolationSearch(IList<T> collection, T target, Func<T, long> selector)
        {
            if (collection == null || target == null)
                throw new ArgumentNullException("Collection or target is null.");
            if (collection.Count == 0)
                throw new ArgumentException("Collection is empty.");

            if (collection[0].Equals(target))
            {
                return 0;
            }

            long targetVal = selector(target);
            int low = 0;
            int high = collection.Count - 1;

            while (low <= high && selector(collection[low]) != selector(collection[high]))
            {
                long lowVal = selector(collection[low]);
                long highVal = selector(collection[high]);

                int pos = low + (int)((targetVal - lowVal) * (high - low) / (highVal - lowVal));

                if (pos < low || pos > high)
                    break;

                long posVal = selector(collection[pos]);

                if (posVal == targetVal)
                    return pos;
                else if (posVal < targetVal)
                    low = pos + 1;
                else
                    high = pos - 1;
            }

            if (selector(collection[low]) == targetVal)
                return low;

            return -1;
        }

        /// <summary>
        /// Performs jump search on a sorted list by dividing it into blocks.
        /// Offers improved performance over linear search for larger sorted datasets.
        /// </summary>
        /// <param name="collection">A list sorted in ascending order.</param>
        /// <param name="target">The element to search for.</param>
        /// <returns>The index of the found element; otherwise, -1.</returns>
        public int JumpSearch(IList<T> collection, T target)
        {
            if (collection == null || target == null)
                throw new ArgumentNullException("Collection or target is null.");
            if (collection.Count == 0)
                throw new ArgumentException("Collection is empty.");

            int n = collection.Count;
            if (n == 0)
            {
                throw new ArgumentException("Collection is empty.");
            }

            if (collection[0].Equals(target))
            {
                return 0;
            }

            int step = (int)Math.Floor(Math.Sqrt(n));
            int prev = 0;

            while (prev < n && collection[Math.Min(step, n) - 1].CompareTo(target) < 0)
            {
                prev = step;
                step += (int)Math.Floor(Math.Sqrt(n));
                if (prev >= n)
                {
                    return -1;
                }
            }

            for (int i = prev; i < Math.Min(step, n); i++)
            {
                if (collection[i].CompareTo(target) == 0)
                {
                    return i;
                }
            }

            return -1; // Target not found
        }

        /// <summary>
        /// Performs a linear (sequential) search over the entire list.
        /// </summary>
        /// <param name="collection">The list of elements to search.</param>
        /// <param name="target">The element to locate.</param>
        /// <returns>The index of the found element; otherwise, -1.</returns>
        public int LinearSearch(IList<T> collection, T target)
        {
            if (collection == null || target == null)
                throw new ArgumentNullException("Collection or target is null.");
            if (collection.Count == 0)
                throw new ArgumentException("Collection is empty.");

            if (collection[0].Equals(target))
            {
                return 0;
            }

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].CompareTo(target) == 0)
                {
                    return i;
                }
            }

            return -1; // Target not found
        }
    }
}
