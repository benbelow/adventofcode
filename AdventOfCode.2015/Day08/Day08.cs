using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode._2015.Day08
{
    public static class Day08
    {
        private const int Day = 08;
        
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return lines.Sum(s => NumberOfCharactersInCode(s) - NumberOfCharactersInMemory(s));
        }

        public static Dictionary<string, int> ExpectedEscapes = new Dictionary<string, int>
        {
            {"\\\"", 1}, 
            {"\\\\\\\\", 1},
            {"\\x", 3}
        };
        
        public static int NumberOfCharactersInMemory(string s)
        {
            var parsed = Regex.Unescape(s);
            
            var removeStart = s.FirstOrDefault() == '"' ? 1 : 0;
            var removeEnd = s.LastOrDefault() == '"' ? 1 : 0;

            return parsed.Length - removeEnd - removeStart;
        }
        
        public static int NumberOfCharactersInCode(string s) => s.Length;

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return -1;
        }
    }
}