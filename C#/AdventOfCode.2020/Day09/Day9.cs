using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day09
{
    public static class Day09
    {
        private const int Day = 9;
        
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var ints = lines.Select(long.Parse).ToList();

            var preamble = ints.Take(25).ToList();
            var others = ints.Skip(25).ToList();

            foreach (var i in others)
            {
                if (!IsSumOf(preamble, i))
                {
                    return i;
                }
                preamble.RemoveAt(0);
                preamble.Add(i);
            }
            
            return -1;
        }

        public static bool IsSumOf(List<long> preamble, long x)
        {
            foreach (var i in preamble)
            {
                if (preamble.Contains(x - i))
                {
                    return true;
                }
            }

            return false;
        }
        
        public static long Part2(long target = 1398413738L)
        {
            var lines = FileReader.ReadInputLines(Day).ToList();

            var ints = lines.Select(long.Parse);

            var contiguousNumbers = new List<long>();
            
            foreach (var i in ints)
            {
                if (contiguousNumbers.Sum() == target && contiguousNumbers.Count > 1)
                {
                    return contiguousNumbers.Min() + contiguousNumbers.Max(); 
                }
                
                contiguousNumbers.Add(i);
                if (contiguousNumbers.Sum() == target && contiguousNumbers.Count > 1)
                {
                    break;
                }

                while (contiguousNumbers.Sum() > target)
                {
                    contiguousNumbers.RemoveAt(0);
                    if (contiguousNumbers.Sum() == target)
                    {
                        break;
                    }
                }
            }
            
            
            return contiguousNumbers.Min() + contiguousNumbers.Max();
        }
    }
}