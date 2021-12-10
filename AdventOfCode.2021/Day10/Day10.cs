using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day10
{
    public static class Day10
    {
        private const int Day = 10;
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var score1 = 0L;
            
            foreach (var line in lines)
            {
                var score = GetScore(line);
                score1 += score;
            }
            
            return score1;
        }

        private static long GetScore(string line)
        {
            var queue = new List<char>();

            var dict = new Dictionary<char, char>()
            {
                {'(', ')'},
                {'<', '>'},
                {'[', ']'},
                {'{', '}'},
            };

            var scores = new Dictionary<char, long>
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137},
            };
            
            foreach (var c in line)
            {
                if (dict.Keys.Contains(c))
                {
                    queue.Add(c);
                }
                else
                {
                    var pair = queue.Last();
                    queue.RemoveAt(queue.Count - 1);
                    var expected = dict[pair];

                    if (expected != c)
                    {
                        return scores[c];
                    }
                }
            }

            return 0;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}