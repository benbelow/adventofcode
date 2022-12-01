using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Common.IntCode
{
    public static class Parser
    {
        public static List<long> ParseIntCode(string intCode)
        {
            return intCode.Split(',').Select(long.Parse).ToList();
        }
    }
}