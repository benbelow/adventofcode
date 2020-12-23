using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day23
{
    public static class Day23
    {
        private const int Day = 23;

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var cups = lines.Single().Select(x => int.Parse(x.ToString())).ToList();

            var result = PlayCrabCubs(cups, 0, 100);

            var indexOf1 = result.IndexOf(1);
            var cupsFrom1 = result.Skip(indexOf1 + 1).Concat(result.Take(indexOf1));
            
            return CupsToLong(cupsFrom1.ToList());
        }

        public static long CupsToLong(List<int> cups) => long.Parse(cups.Select(x => x.ToString()).ToList().StringsToString());
        
        public static List<int> PlayCrabCubs(List<int> cups, int initialCup, int numberOfRounds)
        {
            var numberOfCups = cups.Count;
            var currentIndex = initialCup;

            int WrappedIndex(int index, int toAdd = 0)
            {
                return (index + toAdd) % cups.Count;
            }

            void PlayRound()
            {
                var currentValue = cups[WrappedIndex(currentIndex)];

                var indexesToRemove = new[]
                {
                    WrappedIndex(currentIndex, 1),
                    WrappedIndex(currentIndex, 2),
                    WrappedIndex(currentIndex, 3)
                };
                var removedCups = indexesToRemove.Select(i => cups[i]).ToList();
                foreach (var removedCup in removedCups)
                {
                    cups.Remove(removedCup);
                }

                var destinationValue = currentValue - 1;
                if (destinationValue == 0)
                {
                    destinationValue = numberOfCups;
                }
                while (removedCups.Contains(destinationValue))
                {
                    destinationValue = (numberOfCups + destinationValue - 1) % numberOfCups;
                    if (destinationValue == 0)
                    {
                        destinationValue = numberOfCups;
                    }
                }

                // Reverse so we always add at same index
                foreach (var removedCup in ((IEnumerable<int>) removedCups).Reverse())
                {
                    var destinationIndex = cups.IndexOf(destinationValue) + 1;
                    cups.Insert(destinationIndex, removedCup);
                }

                currentIndex = cups.IndexOf(currentValue);
                currentIndex = WrappedIndex(currentIndex + 1);
            }

            for (var round = 0; round < numberOfRounds; round++)
            {
                PlayRound();
            }

            return cups;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}