using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day25
{
    public static class Day25
    {
        private const int Day = 25;
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var door = long.Parse(lines.First());
            var key = long.Parse(lines.Last());

            var doorLoop = GetLoopSize(door);
            var keyLoop = GetLoopSize(key);
            
            var encryptionKey1 = Transform(key, doorLoop);
            var encryptionKey2 = Transform(door, keyLoop);
            
            return encryptionKey1;
        }

        public static int GetLoopSize(long publicKey)
        {
            var x = 0;
            while (true)
            {
                if (Transform(7, x) == publicKey)
                {
                    return x;
                }

                x++;
            }
        }

        // value, index
        public static List<long> known = new List<long>{7};
        
        public static long Transform(long x, int loopSize)
        {
            if (x == 7)
            {
                if (known.Count > loopSize)
                {
                    return known[loopSize];
                }

                var value = known.Last();
                for (long i = known.Count; i < loopSize; i++)
                {
                    value = x * value;
                    value = value % 20201227;
                    known.Add(value);
                }

                return value;
            }

            else
            {
                
                var value = 1L;
                for (long i = 0; i < loopSize; i++)
                {
                    value = x * value;
                    value = value % 20201227;
                }

                return value;
            }
        }
    }
}