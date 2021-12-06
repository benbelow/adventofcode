using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day06
{
    public static class Day06
    {
        private const int Day = 06;
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var fish = lines.First().Split(",").Select(int.Parse).ToList();

            for (var day = 0; day < 80; day++)
            {
                var newFish = 0;
                fish = fish.Select(f =>
                {
                    if (f == 0)
                    {
                        newFish++;
                        return 6;
                    }

                    return f - 1;
                }).ToList();

                for (var i = 0; i < newFish; i++)
                {
                    fish.Add(8);
                }
            }
            
            return fish.Count;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}