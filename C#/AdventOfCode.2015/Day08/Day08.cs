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

        public static int NumberOfCharactersInDoubleEscaped(string s)
        {
            var matches = Regex.Matches(s, "[0-9a-zA-Z]");
            return s.Length + (s.Length -  matches.Count) + 2;
        } 
        
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
            return lines.Sum(s => NumberOfCharactersInDoubleEscaped(s) - NumberOfCharactersInCode(s));
        }
    }
}