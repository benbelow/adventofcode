using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day06
{
    public static class Day06
    {
        private const int Day = 6;
        
public static long Part1()
{
    var groups = FileReader.ReadInputLines(Day).ToList().Split("");
    var aggregated = groups.Select(g => g.Aggregate("", (a, x) => a + x));
    return aggregated.Sum(a => a.GroupBy(c => c).Count());
}

public static long Part2()
{
    var groups = FileReader.ReadInputLines(Day).ToList().Split("");
    var aggregated = groups.Select(g => (g.Aggregate("", (a, x) => a + x), g.Count()));
    return aggregated.Sum(a => a.Item1.GroupBy(c => c).Count(x => x.Count() == a.Item2));
}
    }
}