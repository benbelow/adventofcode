using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Common
{
    public static class CollectionExtensions
    {
        public static int WrappedIndex<T>(this IEnumerable<T> collection, int index)
        {
            return index % collection.Count();
        }
        
        public static T ElementAtWrapped<T>(this IEnumerable<T> collection, int index)
        {
            collection = collection.ToList();
            return collection.ElementAt(collection.WrappedIndex(index));
        }
        
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator) {
            while ( enumerator.MoveNext() ) {
                yield return enumerator.Current;
            }
        }

        public static Queue<T> WithValues<T>(this Queue<T> queue, params T[] initialValues)
        {
            foreach (var value in initialValues)
            {
                queue.Enqueue(value);
            }

            return queue;
        }

        public static TV FirstMatchingValue<TK, TV>(this IDictionary<TK, TV> dictionary, IEnumerable<TK> potentialKeys)
        {
            foreach (var potentialKey in potentialKeys)
            {
                if (dictionary.ContainsKey(potentialKey))
                {
                    return dictionary[potentialKey];
                }
            }
            throw new KeyNotFoundException("none of the provided keys have values in the dictionary");
        }
    }
}