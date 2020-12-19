using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<IEnumerable<IEnumerable<T>>> DoubleSplit<T>(this IList<T> collection, T splitOn)
        {
            var currentOuter = new List<T>();
            var skipOne = false;
            for (int i = 0; i < collection.Count; i++)
            {
                if (skipOne)
                {
                    skipOne = false;
                    continue;
                }

                var current = collection[i];
                var last = i + 1 == collection.Count;
                if (last)
                {
                    currentOuter.Add(current);
                    continue;
                }

                var next = collection[i + 1];
                if (current.Equals(splitOn) && next.Equals(splitOn))
                {
                    yield return currentOuter.Split(splitOn);
                    currentOuter = new List<T>();
                    skipOne = true;
                    continue;
                }

                currentOuter.Add(current);
            }

            yield return currentOuter.Split(splitOn);
        }

        public static IList<T> Clone<T>(this IEnumerable<T> listToClone) => listToClone.Select(item => item).ToList();

        public static string CharsToString(this IEnumerable<char> chars) => new string(chars.ToArray());
        public static string StringsToString(this IEnumerable<string> strings) => strings.Aggregate((s2, s) => s2 + s);

        public static IEnumerable<string> AllCombos(this IEnumerable<IEnumerable<string>> options)
        {
            options = options.ToList();
            if (options.Count() == 1)
            {
                return options.Single();
            }

            if (options.Count() == 2)
            {
                var toReturn = new List<string>();
                foreach (var option in options.First())
                {
                    foreach (var option2 in options.Last())
                    {
                        toReturn.Add(option + option2);
                    }
                }

                return toReturn;
            }

            var firstPair = options.Take(2);

            var firstPairCombined = firstPair.AllCombos();
            
            return new [] {firstPairCombined}.Concat(options.Skip(2)).AllCombos();
        } 
    }
}