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
            var fish = lines.First().Split(",").Select(long.Parse).ToList();

            var countsByNumber = fish.GroupBy(f => f).ToDictionary(x => x.Key, x => ((long) x.Count(), 0l));
            for (var i = 0; i < 9; i++)
            {
                if (!countsByNumber.ContainsKey(i))
                {
                    countsByNumber[i] = (0, 0);
                }
            }

            for (var day = 0; day < iterations; day++)
            {
                var period = 7;
                var periodicity = (day) % period;

                // new fish start at 8. so will take 8 turns to make a new fish.
                // but old fish only take 6 days to make a baby, so the real periodicity is the one in two additional days time
                var newBabyPeriodicity = (day + 2) % period;
                var newBabyCount = countsByNumber[periodicity].Item1;

                countsByNumber[newBabyPeriodicity] = (
                    countsByNumber[newBabyPeriodicity].Item1,
                    countsByNumber[newBabyPeriodicity].Item2 + newBabyCount
                );

                var maturingBabyPeriodicity = day > 1 ? (day - 2) % period : 0;

                countsByNumber[maturingBabyPeriodicity] = (
                    countsByNumber[maturingBabyPeriodicity].Item1 + countsByNumber[maturingBabyPeriodicity].Item2,
                    0
                );
            }

            return countsByNumber.Sum(x => x.Value.Item1 + x.Value.Item2);
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
            return CountFishEfficient(lines, 256);
        }
    }
}