using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode._2015.Day04
{
    public static class Day04
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

        public static byte[] Hash2(IEnumerable<byte> rawInputBytes, long seed)
        {
            var seedBytes = Encoding.ASCII.GetBytes(seed.ToString());
            var inputBytes = rawInputBytes.Concat(seedBytes).ToArray();
            var hashBytes = md5.ComputeHash(inputBytes);
            return hashBytes;
        }

        public static long Part2(int seed = 0)
        {
            var input = FileReader.ReadInputLines(Day).ToList().Single();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            while (true)
            {
                var hashBytes = Hash2(inputBytes, seed);
                if (hashBytes[0] == 0 && hashBytes[1] == 0 && hashBytes[2] == 0)
                {
                    return seed;
                }

                seed++;
            }
        }
    }
}