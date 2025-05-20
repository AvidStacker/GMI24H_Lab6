using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLib
{
    /// <summary>
    /// Defines a generic interface for sorting algorithms.
    /// Type T must implement IComparable&lt;T&gt; to support element comparison during sorting.
    /// </summary>
    /// <typeparam name="T">The type of elements to sort. Must be comparable.</typeparam>
    internal interface ISortingManager<T> where T : IComparable<T>
    {
        /// <summary>
        /// Sorts the provided list using the Bubble Sort algorithm.
        /// </summary>
        /// <param name="collection">The list of elements to be sorted.</param>
        void BubbleSort(IList<T> collection);

        /// <summary>
        /// Sorts the list using the Insertion Sort algorithm.
        /// Suitable for small or nearly sorted collections.
        /// </summary>
        /// <param name="collection">The list of elements to be sorted.</param>
        void InsertionSort(IList<T> collection);

        /// <summary>
        /// Sorts the list using the Selection Sort algorithm.
        /// </summary>
        /// <param name="collection">The list of elements to be sorted.</param>
        void SelectionSort(IList<T> collection);

        /// <summary>
        /// Sorts the list using the Merge Sort algorithm.
        /// A divide-and-conquer algorithm that recursively splits and merges sorted sublists.
        /// </summary>
        /// <param name="collection">The list of elements to be sorted.</param>
        /// <param name="l">The starting index of the range to sort.</param>
        /// <param name="r">The ending index of the range to sort.</param>
        void MergeSort(IList<T> collection, int l, int r);

        /// <summary>
        /// Sorts the list using the Heap Sort algorithm.
        /// Converts the list into a max heap and repeatedly extracts the maximum element.
        /// </summary>
        /// <param name="collection">The list of elements to be sorted.</param>
        void HeapSort(IList<T> collection);

        /// <summary>
        /// Sorts the list using the Quick Sort algorithm.
        /// A recursive divide-and-conquer approach using a pivot to partition the list.
        /// </summary>
        /// <param name="collection">The list of elements to be sorted.</param>
        /// <param name="low">The starting index of the range to sort.</param>
        /// <param name="high">The ending index of the range to sort.</param>
        void QuickSort(IList<T> collection, int low, int high);
    }
}
