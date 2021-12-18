using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day18
{
    public static class Day18
    {
        private const int Day = 18;

        public class SnailNumber
        {
            public SnailNumber Item1 { get; set; }
            public SnailNumber Item2 { get; set; }

            public int? RawValue { get; set; }

            public SnailNumber(string rawData)
            {
                if (int.TryParse(rawData, out var actualNumber))
                {
                    RawValue = actualNumber;
                    return;
                }
                
                var nestingLevel = 0;
                int commaIndex = 0;
                for (int i = 1; i < rawData.Length - 1; i++)
                {
                    var c = rawData[i];
                    switch (c)
                    {
                        case '[':
                            nestingLevel++;
                            break;
                        case ']':
                            nestingLevel--;
                            break;
                        case ',':
                            if (nestingLevel == 0)
                            {
                                commaIndex = i;
                            }
                            break;
                    }
                }

                var rawItem1 = rawData.Substring(1, commaIndex - 1);
                var rawItem2 = rawData.Substring(commaIndex + 1, rawData.Length - rawItem1.Length - 3);
                Item1 = new SnailNumber(rawItem1);
                Item2 = new SnailNumber(rawItem2);
            }
        }

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var snailNumbers = lines.Select(l => new SnailNumber(l)).ToList();
            return -1;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}