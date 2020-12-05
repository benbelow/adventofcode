using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day5
{
    public static class Day5
    {
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(5).ToList();
            var seatIds = lines.Select(SeatId).OrderBy(x => x);
            return seatIds.Max();
        }

        public static long SeatId(string s)
        {
            var binary = s.Replace("F", "0").Replace("B", "1").Replace("L", "0").Replace("R", "1");
            return Convert.ToInt32(binary, 2);
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(5).ToList();
            var seatIds = lines.Select(SeatId).OrderBy(x => x).ToHashSet();
            var possibleNumbers = Enumerable.Range(0, (int) seatIds.Max());
            return possibleNumbers.Single(id => !seatIds.Contains(id) && seatIds.Contains(id + 1) && seatIds.Contains(id - 1));
;        }
    }
}