using System.Collections.Generic;

namespace AdventOfCode.Common.Utils
{
    public static class CollectionUtils
    {
        /// <summary>
        /// Splits an collection as one might a string - instances of the target value are not returned, and the collection split at that point.
        /// e.g. [1, 2, 3, 4, 2, 5] split on 2 => [[1], [3, 4], [5]] 
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> collection, T splitOn)
        {
            var current = new List<T>();
            foreach (var item in collection)
            {
                if (item.Equals(splitOn))
                {
                    yield return current;
                    current = new List<T>();
                }
                else
                {
                    current.Add(item);
                }
            }

            yield return current;
        }
    }
}