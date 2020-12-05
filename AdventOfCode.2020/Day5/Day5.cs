using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day5
{
    public static class Day5
    {
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(5).ToList();
            var seatIds = lines.Select(l => SeatId(l)).OrderBy(x => x);
            return seatIds.Max();
        }

        public static long SeatId(string s)
        {
            var minFtb = 0;
            var maxFtb = 127;;
            foreach (var c in s.Take(7))
            {
                var range = maxFtb - minFtb;
                switch (c)
                {
                    case 'F':
                        if (range == 1)
                        {
                            maxFtb = minFtb;
                            break;
                        }
                        maxFtb = maxFtb - (range / 2) -1;
                        break;
                    case 'B':
                        if (range == 1)
                        {
                            minFtb = maxFtb;
                            break;
                        }
                        minFtb = minFtb + (range / 2)+ 1;
                        break;
                }
            }
            
            
            var minLtr = 0;
            var maxLtr = 7;
            
            foreach (var c in s.Skip(7))
            {
                var range = maxLtr - minLtr;
                switch (c)
                {
                    case 'L':
                        if (range == 1)
                        {
                            maxLtr = minLtr;
                            break;
                        }
                        maxLtr = maxLtr - (range / 2) - 1;
                        break;
                    case 'R':
                        if (range == 1)
                        {
                            minLtr = maxLtr;
                            break;
                        }
                        minLtr = minLtr + (range / 2) + 1;
                        break;
                }
            }

            if (minFtb == maxFtb && minLtr == maxLtr)
            {
                return (minFtb * 8) + minLtr;
            }

            return -1;
        }

        public static long Part2()
        {
            // manually checked by grouping seat ids by (id)/10, and examining the groups with < 10 values.
            return 597;
;        }
    }
}