using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day03
{
    public static class Day03
    {
        private const int Day = 03;
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var bitArrays = lines.Select(l => l.ToList()).ToList();

            var total = bitArrays.Count;
            var itemSize = bitArrays.First().Count;
            var onesAtPositions = Enumerable.Range(0, itemSize).Select(i => bitArrays.Count(bit => bit[i] == '1'));
            var isMajorities = onesAtPositions.Select(x => (total - x) < x).ToList();

            var gammaRateString = isMajorities.Select(x => x.ToBit()).Aggregate("", (s, s1) => s + s1);
            var gammaRate = Convert.ToInt32(gammaRateString, 2);
            
            var epsilonRateString = isMajorities.Select(x => (!x).ToBit()).Aggregate("", (s, s1) => s + s1);
            var epsilonRate = Convert.ToInt32(epsilonRateString, 2);
            
            return epsilonRate * gammaRate;
        }

        public static string ToBit(this bool b) => b switch
        {
            true => "1",
            false => "0"
        };
        
        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}