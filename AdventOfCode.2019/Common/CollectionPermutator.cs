using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode._2019.Common
{
    public static class CollectionPermutator
    {
        public static IEnumerable<IList<T>> AllPermutations<T>(this IList<T> sequence, int count = -1)
        {
            if (count == -1)
            {
                count = sequence.Count;
            }
            if (count == 1)
            {
                yield return sequence;
            }
            else
            {
                for (var i = 0; i < count; i++)
                {
                    foreach (var perm in AllPermutations(sequence, count - 1))
                    {
                        yield return perm;
                    }
                    RotateRight(sequence, count);
                }
            }
        }

        private static void RotateRight<T>(IList<T> sequence, int count)
        {
            var tmp = sequence[count-1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }
    }
}