using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Common.IntCode
{
    public static class Parser
    {
        public static List<int> ParseIntCode(string intCode)
        {
            return intCode.Split(',').Select(int.Parse).ToList();
        }
    }
}