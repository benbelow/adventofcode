using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;
using MoreLinq;

namespace AdventOfCode._2021.Day19
{
    public static class Day19
    {
        private const int Day = 19;

        public class Scanner
        {
            public int Id { get; set; }
            public List<(int, int, int)> Beacons { get; set; }
            
            public Scanner(List<string> rawData)
            {
                Id = int.Parse(rawData.First().Split("scanner ").Last().Split(" -").First());
                var beaconLines = rawData.Skip(1);
                var ints = beaconLines.Select(line => line.Split(',').Select(int.Parse).ToList()).ToList();
                Beacons = ints.Select(q => (q[0], q[1], q[2])).ToList();
            }
        }
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var rawDatas = lines.DoubleSplit("");
            var scanners = rawDatas.First().Select(q => new Scanner(q.ToList())).ToList();
            
            return -1;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}