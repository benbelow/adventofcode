using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode._2015.Day4
{
    public static class Day4
    {
        private const int Day = 4;
        
        private static MD5 md5 = MD5.Create();
        
        public static long Part1()
        {
            var input = FileReader.ReadInputLines(Day).ToList().Single();
            var seed = 346386L;
            while (true)
            {
                var hash = Hash(input, seed);
                if (hash.StartsWith("00000"))
                {
                    return seed;
                }

                seed++;
            }
        }

        public static string Hash(string input, long seed)
        {
            var inputBytes = Encoding.ASCII.GetBytes($"{input}{seed}");
            var hashBytes = md5.ComputeHash(inputBytes);
            
            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static long Part2()
        {
            var input = FileReader.ReadInputLines(Day).ToList().Single();
            var seed = 0;
            while (true)
            {
                var hash = Hash(input, seed);
                if (hash.StartsWith("000000"))
                {
                    return seed;
                }

                seed++;
            }
        }
    }
}