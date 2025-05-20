using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLib
{
    /// <summary>
    /// <summary>
    /// Implementation av olika sorteringsalgoritmer för generiska listor.
    /// </summary>
    /// <typeparam name="T">Typen på elementen som ska sorteras. Måste implementera IComparable<T>.</typeparam>

    public class SortingManager<T> : ISortingManager<T> where T : IComparable<T>
    {
        /// <summary>
        /// Sorterar listan med Bubble Sort-algoritmen.
        /// </summary>
        /// <param name="collection">Listan som ska sorteras.</param>
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
        /// Sorterar listan med Merge Sort-algoritmen.
        /// </summary>
        /// <param name="collection">Listan som ska sorteras.</param>
        /// <param name="l">Startindex för sorteringen.</param>
        /// <param name="r">Slutindex för sorteringen.</param>
        public void MergeSort(IList<T> collection, int l, int r)
        {
            if (l < r)
            {
                // Beräkna mittpunkten
                int m = l + (r - l) / 2;
                // Sortera första och andra halvan
                MergeSort(collection, l, m);
                MergeSort(collection, m + 1, r);
                // Slå ihop de sorterade halvorna
                Merge(collection, l, m, r);
            }
        }

        private void Merge(IList<T> collection, int l, int m, int r)
        {
            // Beräkna storleken på de två delarna
            int n1 = m - l + 1;
            int n2 = r - m;
            // Skapa temporära arrayer
            T[] L = new T[n1];
            T[] R = new T[n2];
            // Kopiera data till temporära arrayer
            for (int i = 0; i < n1; i++)
                L[i] = collection[l + i];
            for (int j = 0; j < n2; j++)
                R[j] = collection[m + 1 + j];
            // Slå ihop de temporära arrayerna
            // Initiala index för första och andra delarna
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
            // Kopiera resterande element från L[]
            while (iIndex < n1)
            {
                collection[k] = L[iIndex];
                iIndex++;
                k++;
            }
            // Kopiera resterande element från R[]
            while (jIndex < n2)
            {
                collection[k] = R[jIndex];
                jIndex++;
                k++;
            }
        }

        /// <summary>
        /// Sorterar listan med Heap Sort-algoritmen.
        /// </summary>
        /// <param name="collection">Listan som ska sorteras.</param>
        public void HeapSort(IList<T> collection)
        {
            int n = collection.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(collection, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                Swap(collection, 0, i);
                // Heapify den reducerade heapen
                Heapify(collection, i, 0);
            }
        }

        private void Heapify(IList<T> collection, int n, int i)
        {
            int largest = i; // Ställ in största som rot
            int left = 2 * i + 1; // vänster = 2*i + 1
            int right = 2 * i + 2; // höger = 2*i + 2

            if (left < n && collection[left].CompareTo(collection[largest]) > 0)
                largest = left;
            if (right < n && collection[right].CompareTo(collection[largest]) > 0)
                largest = right;
            if (largest != i)
            {
                Swap(collection, i, largest);
                // Rekursivt heapify den påverkade subträd
                Heapify(collection, n, largest);
            }
        }

        /// <summary>
        /// Sorterar listan med Insertion Sort-algoritmen.
        /// </summary>
        /// <param name="collection">Listan som ska sorteras.</param>
        public void InsertionSort(IList<T> collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return; // Ingen sortering behövs
            }

            if (collection.Count == 1)
            {
                return; // Ingen sortering behövs
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
        /// Sorterar listan med Quick Sort-algoritmen.
        /// </summary>
        /// <param name="collection">Listan som ska sorteras.</param>
        /// <param name="low">Startindex för sorteringen.</param>
        /// <param name="high">Slutindex för sorteringen.</param>
        public void QuickSort(IList<T> collection, int low, int high)
        {
            if (low < high)
            {
                // Partitionera arrayen och få pivotindex
                int pivotIndex = Partition(collection, low, high);

                // Sortera elementen före och efter partitionen
                QuickSort(collection, low, pivotIndex - 1);
                QuickSort(collection, pivotIndex + 1, high);
            }
        }

        private int Partition(IList<T> collection, int low, int high)
        {
            T pivot = collection[high]; // Välj det sista elementet som pivot
            int i = low - 1; // Index för det mindre elementet

            for (int j = low; j < high; j++)
            {
                // Om det aktuella elementet är mindre än eller lika med pivot
                if (collection[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    // Byt plats på collection[i] och collection[j]
                    Swap(collection, i, j);
                }
            }

            // Byt plats på collection[i + 1] och collection[high] (eller pivot)
            Swap(collection, i + 1, high);

            return i + 1;
        }

        private void Swap(IList<T> collection, int i, int j)
        {
            T temp = collection[i];
            collection[i] = collection[j];
            collection[j] = temp;
        }

        /// <summary>
        /// Sorterar listan med Selection Sort-algoritmen.
        /// </summary>
        /// <param name="collection">Listan som ska sorteras.</param>
        public void SelectionSort(IList<T> collection)
        {
            throw new NotImplementedException();
        }
    }
}
