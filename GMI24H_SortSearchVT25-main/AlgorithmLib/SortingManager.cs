using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLib
{
    /// <summary>
    /// Implements various sorting algorithms for generic lists.
    /// All elements must be comparable using IComparable&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T">The type of elements to be sorted.</typeparam>

    public class SortingManager<T> : ISortingManager<T> where T : IComparable<T>
    {
        /// <summary>
        /// Sorts the list using the Bubble Sort algorithm.
        /// Repeatedly swaps adjacent elements if they are in the wrong order.
        /// </summary>
        /// <param name="collection">The list to sort.</param>
        public void BubbleSort(IList<T> collection)
        {
            bool swapped = true;
            while (swapped)
            {
                swapped = false;
                for (int i = 0; i < collection.Count - 1; i++)
                {
                    if (collection[i].CompareTo(collection[i + 1]) > 0)
                    {
                        T temp = collection[i + 1];
                        collection[i + 1] = collection[i];
                        collection[i] = temp;
                        swapped = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sorts the list using the Merge Sort algorithm.
        /// A divide-and-conquer approach that splits the list and recursively merges sorted sublists.
        /// </summary>
        /// <param name="collection">The list to sort.</param>
        /// <param name="l">Start index of the range to sort.</param>
        /// <param name="r">End index of the range to sort.</param>
        public void MergeSort(IList<T> collection, int l, int r)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;
                MergeSort(collection, l, m);
                MergeSort(collection, m + 1, r);
                Merge(collection, l, m, r);
            }
        }

        /// <summary>
        /// Merges two sorted subarrays within the list.
        /// </summary>
        private void Merge(IList<T> collection, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;

            T[] L = new T[n1];
            T[] R = new T[n2];

            for (int i = 0; i < n1; i++)
                L[i] = collection[l + i];
            for (int j = 0; j < n2; j++)
                R[j] = collection[m + 1 + j];

            int k = l;
            int iIndex = 0;
            int jIndex = 0;
            while (iIndex < n1 && jIndex < n2)
            {
                if (L[iIndex].CompareTo(R[jIndex]) <= 0)
                {
                    collection[k] = L[iIndex];
                    iIndex++;
                }
                else
                {
                    collection[k] = R[jIndex];
                    jIndex++;
                }
                k++;
            }

            while (iIndex < n1)
            {
                collection[k] = L[iIndex];
                iIndex++;
                k++;
            }

            while (jIndex < n2)
            {
                collection[k] = R[jIndex];
                jIndex++;
                k++;
            }
        }

        /// <summary>
        /// Sorts the list using the Heap Sort algorithm.
        /// Converts the list into a max-heap and repeatedly extracts the largest element.
        /// </summary>
        /// <param name="collection">The list to sort.</param>
        public void HeapSort(IList<T> collection)
        {
            int n = collection.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(collection, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                Swap(collection, 0, i);
                Heapify(collection, i, 0);
            }
        }

        /// <summary>
        /// Maintains the heap property for a subtree rooted at index i.
        /// </summary>
        private void Heapify(IList<T> collection, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && collection[left].CompareTo(collection[largest]) > 0)
                largest = left;
            if (right < n && collection[right].CompareTo(collection[largest]) > 0)
                largest = right;
            if (largest != i)
            {
                Swap(collection, i, largest);
                Heapify(collection, n, largest);
            }
        }

        /// <summary>
        /// Sorts the list using the Insertion Sort algorithm.
        /// Efficient for small or nearly sorted lists.
        /// </summary>
        /// <param name="collection">The list to sort.</param>
        public void InsertionSort(IList<T> collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return;
            }

            if (collection.Count == 1)
            {
                return;
            }

            for (int i = 1; i < collection.Count; i++)
            {
                T key = collection[i];
                int j = i - 1;

                if (collection[j].CompareTo(key) > 0)
                {
                    while (j >= 0 && collection[j].CompareTo(key) > 0)
                    {
                        collection[j + 1] = collection[j];
                        j--;
                    }
                    collection[j + 1] = key;
                }
            }
        }

        /// <summary>
        /// Sorts the list using the Quick Sort algorithm.
        /// Uses a pivot to recursively partition and sort sublists.
        /// </summary>
        /// <param name="collection">The list to sort.</param>
        /// <param name="low">Start index of the range to sort.</param>
        /// <param name="high">End index of the range to sort.</param>
        public void QuickSort(IList<T> collection, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(collection, low, high);

                QuickSort(collection, low, pivotIndex - 1);
                QuickSort(collection, pivotIndex + 1, high);
            }
        }

        /// <summary>
        /// Partitions the list and returns the pivot index used by Quick Sort.
        /// </summary>
        private int Partition(IList<T> collection, int low, int high)
        {
            T pivot = collection[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (collection[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    Swap(collection, i, j);
                }
            }

            Swap(collection, i + 1, high);

            return i + 1;
        }

        /// <summary>
        /// Swaps two elements in the list.
        /// </summary>
        private void Swap(IList<T> collection, int i, int j)
        {
            T temp = collection[i];
            collection[i] = collection[j];
            collection[j] = temp;
        }

        /// <summary>
        /// Sorts the list using the Selection Sort algorithm.
        /// Repeatedly selects the smallest remaining element.
        /// </summary>
        /// <param name="collection">The list to sort.</param>
        public void SelectionSort(IList<T> collection)
        {
            throw new NotImplementedException();
        }
    }
}
