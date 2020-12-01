using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day1
{
    public static class Day1
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(1);
            var ints = lines.Select(int.Parse).ToList();

            foreach (var i in ints)
            {
                var other = 2020 - i;
                if (ints.Contains(other))
                {
                    return i * other;
                }
            }

            return 0;
        }

        public static int Part2()
        {
            var lines = FileReader.ReadInputLines(1);
            var ints = lines.Select(int.Parse).ToList();

            foreach (var i in ints)
            {
                var other1 = 2020 - i;
                if (other1 == -1)
                {
                    continue;
                }
                foreach (var j in ints.Except(new [] { i}))
                {
                    var other2 = other1 - j;
                    if (ints.Contains(other2))
                    {
                        return i * j * other2;
                    }
                }
            }

            return -1;
        }

        public static int GetOther(IList<int> ints, int target)
        {
            foreach (var i in ints)
            {
                var other = target - i;
                if (ints.Contains(other))
                {
                    return i * other;
                }
            }

            return -1;
        }
    }
}