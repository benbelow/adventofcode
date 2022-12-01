using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day03
{
    public static class Day03
    {
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(3).ToList();

            return NumberOfTrees(lines, 3, 1);
        }

        private static long NumberOfTrees(IList<string> lines, int  rightSkip, int downSkip)
        {
            var count = 0;
            var x = 0;

            for (var y = downSkip; y < lines.Count; y+= downSkip)
            {
                var line = lines[y];
                x += rightSkip;
                // assume all lines are same length
                x %= line.Length;

                var isTree = line[x] == '#';
                if (isTree)
                {
                    count++;
                }
            }

            return count;
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(3).ToList();
            
            return NumberOfTrees(lines, 1, 1) *
                   NumberOfTrees(lines, 3, 1) *
                   NumberOfTrees(lines, 5, 1) *
                   NumberOfTrees(lines, 7, 1) *
                   NumberOfTrees(lines, 1, 2);;
        }
    }
}