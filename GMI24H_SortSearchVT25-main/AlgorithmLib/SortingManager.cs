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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sorterar listan med Merge Sort-algoritmen.
        /// </summary>
        /// <param name="collection">Listan som ska sorteras.</param>
        /// <param name="l">Listan som ska sorteras.</param>        BEHÖVER KOMMENTERAS!!!
        /// <param name="r">Listan som ska sorteras.</param>        BEHÖVER KOMMENTERAS!!!
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sorterar listan med Insertion Sort-algoritmen.
        /// </summary>
        /// <param name="collection">Listan som ska sorteras.</param>
        public void InsertionSort(IList<T> collection)
        {
            throw new NotImplementedException();
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
