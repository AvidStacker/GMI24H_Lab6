using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLib
{
    /// <summary>
    /// Implementation av olika sökalgoritmer för generiska listor.
    /// </summary>
    /// <typeparam name="T">Typen på elementen som ska sökas i. Måste implementera IComparable<T>.</typeparam>

    public class SearchingManager<T> : ISearchingManager<T> where T : IComparable<T>
    {
        /// <summary>
        /// Utför binär sökning i en sorterad lista.
        /// </summary>
        /// <param name="collection">Sorterad lista att söka i.</param>
        /// <param name="target">Värdet som söks.</param>
        /// <returns>Index för träff eller -1 om inget hittas.</returns>
        public int BinarySearch(IList<T> collection, T target)
        {
            if (collection == null || target == null)
            {
                throw new ArgumentNullException("Collection or target is null.");
            }

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
        /// Utför exponential search i en sorterad lista.
        /// </summary>
        /// <param name="collection">Sorterad lista att söka i.</param>
        /// <param name="target">Värdet som söks.</param>
        /// <returns>Index för träff eller -1 om inget hittas.</returns>
        public int ExponentialSearch(IList<T> collection, T target)
        {
            if (collection == null || target == null)
            {
                throw new ArgumentNullException("Collection or target is null.");
            }

            if (collection.Count == 0)
            {
                throw new ArgumentException("Collection is empty.");
            }

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
        /// Utför interpolationssökning. Endast för typer som är int-kompatibla.
        /// </summary>
        /// <param name="collection">Sorterad lista av heltal.</param>
        /// <param name="target">Värdet som söks.</param>
        /// <returns>Index för träff eller -1 om inget hittas.</returns>
        public int InterpolationSearch(IList<T> collection, T target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Utför jump search i en sorterad lista.
        /// </summary>
        /// <param name="collection">Sorterad lista att söka i.</param>
        /// <param name="target">Värdet som söks.</param>
        /// <returns>Index för träff eller -1 om inget hittas.</returns>
        public int JumpSearch(IList<T> collection, T target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Utför linjär sökning i en lista.
        /// </summary>
        /// <param name="collection">Listan att söka i.</param>
        /// <param name="target">Värdet som söks.</param>
        /// <returns>Index för träff eller -1 om inget hittas.</returns>
        public int LinearSearch(IList<T> collection, T target)
        {
            throw new NotImplementedException();
        }
    }
}
