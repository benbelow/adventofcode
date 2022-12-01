using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day02
{
    public static class Day02
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(2);
            var passwords = lines.Select(l => new Password(l));
            return passwords.Count(p => p.IsValid1());

        }

        public static int Part2()
        {
            var lines = FileReader.ReadInputLines(2);
            var passwords = lines.Select(l => new Password(l));
            return passwords.Count(p => p.IsValid2());
        }

        public class Password
        {
            public Password(string line)
            {
                var s = line.Split(" ");
                
                Min = int.Parse(s[0].Split("-")[0]);
                Max = int.Parse(s[0].Split("-")[1]);
                Char = s[1].Split(":").First().First();
                Pwd = s[2];
            }
            
            public int Min { get; set; }
            public int Max { get; set; }
            public char Char { get; set; }
            public string Pwd { get; set; }

            public bool IsValid1()
            {
                var count = Pwd.Count(c => c == Char);
                return count >= Min && count <= Max;
            }

            public bool IsValid2()
            {
                return Pwd[Min-1] == Char ^ Pwd[Max-1] == Char;
            }
        }
    }
}