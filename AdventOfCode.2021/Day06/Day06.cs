using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day06
{
    public static class Day06
    {
        private const int Day = 06;

        public static long Part1(bool isExample = false, int iterations = 80)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            // return CountFishNaive(lines, 80);
            return CountFishEfficient(lines, iterations);
        }
        
        private static long CountFishEfficient(List<string> lines, int iterations)
        {
            var fish = lines.First().Split(",").Select(int.Parse).ToList();

            var countsByNumber = fish.GroupBy(f => f).ToDictionary(x => x.Key, x => x.Count());
            for (var i = 0; i < 9; i++)
            {
                if (!countsByNumber.ContainsKey(i))
                {
                    countsByNumber[i] = 0;
                }
            }

            var babyQueue = new Queue<(int, int)>();
            babyQueue.Enqueue((0,0));
            babyQueue.Enqueue((0,0));
            babyQueue.Enqueue((0,0));
            babyQueue.Enqueue((0,0));
            babyQueue.Enqueue((0,0));
            babyQueue.Enqueue((0,0));
            babyQueue.Enqueue((0,0));
            babyQueue.Enqueue((0,0));
            
            for (var i = 1; i <= iterations; i++)
            {
                var periodicity = i % 8;
                // new fish start at 8. so will take 8 turns to make a new fish.
                // but old fish only take 6 days to make a baby, so the real periodicity is the one in two days time

                var newBabyPeriodicity = (i + 2) % 8;

                var (queuedBabyPeriodicity, queuedBabyCount) = babyQueue.Dequeue();
                babyQueue.Enqueue((newBabyPeriodicity, countsByNumber[periodicity]));

                countsByNumber[queuedBabyPeriodicity] += queuedBabyCount;
            }

            foreach (var queuedBaby in babyQueue)
            {
                var finalPeriodicity = iterations % 8;
                var (queuedBabyPeriodicity, queuedBabyCount) = queuedBaby;

                if (finalPeriodicity >= queuedBabyPeriodicity)
                {
                    countsByNumber[queuedBabyPeriodicity] += queuedBabyCount;
                }
            }
            

            return countsByNumber.Sum(x => x.Value);
        }

        private static long CountFishNaive(List<string> lines, int iterations)
        {
            var fish = lines.First().Split(",").Select(int.Parse).ToList();

            for (var day = 0; day < iterations; day++)
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