using System;
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
            return lines.Sum(GetInvalidScore);
        }

        private static long GetInvalidScore(string line)
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

        private static long GetIncompleteScore(string line)
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
                {')', 1},
                {']', 2},
                {'}', 3},
                {'>', 4},
            };

            var score = 0L;
            
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

            queue.Reverse();
            foreach (var c in queue)
            {
                score *= 5L;
                score += scores[dict[c]];
            }

            return score;
        }

        
        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var valid = lines.Where(l => GetInvalidScore(l) == 0);
            var scores = valid.Select(GetIncompleteScore).ToList();

            var midIndex = scores.Count / 2;
            return scores.OrderBy(x => x).ToList()[midIndex];
        }
    }
}