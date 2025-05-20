using System;

namespace AlgorithmLib
{

    /// <summary>
    /// Defines a generic interface for search algorithms operating on comparable types.
    /// Provides method signatures for linear, binary, exponential, interpolation, and jump search techniques.
    /// </summary>
    /// <typeparam name="T">The data type to search for. Must implement IComparable<T>.</typeparam>
    public interface ISearchingManager<T> where T : IComparable<T>
    {
        /// <summary>
        /// Performs a linear (sequential) search for a target value within a collection.
        /// </summary>
        /// <param name="collection">The list to search through.</param>
        /// <param name="target">The value to locate in the collection.</param>
        /// <returns>
        /// The index of the target if found; otherwise, -1.
        /// </returns>
        int LinearSearch(IList<T> collection, T target);

        /// <summary>
        /// Performs a binary search on a sorted collection to locate the target value.
        /// </summary>
        /// <param name="collection">A list sorted in ascending order.</param>
        /// <param name="target">The value to locate.</param>
        /// <returns>
        /// The index of the target if found; otherwise, -1.
        /// </returns>
        int BinarySearch(IList<T> collection, T target);

        /// <summary>
        /// Executes an exponential search to quickly narrow down the search interval for a value in a sorted collection.
        /// This algorithm is efficient when the target is likely to be near the beginning of the collection.
        /// </summary>
        /// <param name="collection">A list sorted in ascending order.</param>
        /// <param name="target">The value to locate.</param>
        /// <returns>
        /// The index of the target if found; otherwise, -1.
        /// </returns>
        int ExponentialSearch(IList<T> collection, T target);

        /// <summary>
        /// Performs interpolation search on a sorted collection.
        /// This method is best suited for uniformly distributed numeric data.
        /// </summary>
        /// <param name="collection">A list sorted in ascending order.</param>
        /// <param name="target">The target value to locate.</param>
        /// <param name="selector">A function that converts T to a long for numeric interpolation.</param>
        /// <returns>
        /// The index of the target if found; otherwise, -1.
        /// </returns>
        int InterpolationSearch(IList<T> collection, T target, Func<T, long> selector);

        /// <summary>
        /// Executes a jump search on a sorted collection by dividing it into blocks and scanning within relevant blocks.
        /// </summary>
        /// <param name="collection">A list sorted in ascending order.</param>
        /// <param name="target">The value to locate.</param>
        /// <returns>
        /// The index of the target if found; otherwise, -1.
        /// </returns>
        int JumpSearch(IList<T> collection, T target);
    }
}
