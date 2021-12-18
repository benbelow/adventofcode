using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using NUnit.Framework;

namespace AdventOfCode._2021.Day18
{
    public static class Day18
    {
        private const int Day = 18;

        public class SnailNumber
        {
            private const int ExplosionCutoffNestingLevel = 4;
            private const int SplitMaxValueCutoff = 10;
            public SnailNumber Item1 { get; set; }
            public SnailNumber Item2 { get; set; }

            public int? RegularNumberValue { get; set; }
            public bool IsRegular => RegularNumberValue != null;
            public string RawData { get; set; }

            public int NestingLvl { get; set; }
            public SnailNumber Parent { get; }

            public bool IsItem1 => Parent.Item1 == this;
            public bool IsItem2 => Parent.Item2 == this;

            public SnailNumber(string rawData, int nestingLevel = 0, SnailNumber parent = null)
            {
                RawData = rawData;
                NestingLvl = nestingLevel;
                Parent = parent;

                if (int.TryParse(rawData, out var actualNumber))
                {
                    RegularNumberValue = actualNumber;
                    return;
                }

                var bracketNestingLevel = 0;
                int commaIndex = 0;
                for (int i = 1; i < rawData.Length - 1; i++)
                {
                    var c = rawData[i];
                    switch (c)
                    {
                        case '[':
                            bracketNestingLevel++;
                            break;
                        case ']':
                            bracketNestingLevel--;
                            break;
                        case ',':
                            if (bracketNestingLevel == 0)
                            {
                                commaIndex = i;
                            }

                            break;
                    }
                }

                var rawItem1 = rawData.Substring(1, commaIndex - 1);
                var rawItem2 = rawData.Substring(commaIndex + 1, rawData.Length - rawItem1.Length - 3);
                Item1 = new SnailNumber(rawItem1, nestingLevel + 1, this);
                Item2 = new SnailNumber(rawItem2, nestingLevel + 1, this);
            }

            public override string ToString()
            {
                if (IsRegular)
                {
                    return RegularNumberValue.Value.ToString();
                }
                else
                {
                    return $"[{Item1},{Item2}]";
                }
            }

            public List<SnailNumber> RegularNumbersList()
            {
                if (IsRegular)
                {
                    return new List<SnailNumber> { this };
                }

                return Item1.RegularNumbersList().Concat(Item2.RegularNumbersList()).ToList();
            }

            public List<SnailNumber> NonRegularNumbersList()
            {
                if (IsRegular)
                {
                    return new List<SnailNumber>();
                }

                if (Item1.IsRegular || Item2.IsRegular)
                {
                    return new List<SnailNumber> { this }.Concat(Item1.NonRegularNumbersList()).Concat(Item2.NonRegularNumbersList()).ToList();
                }

                return Item1.NonRegularNumbersList().Concat(Item2.NonRegularNumbersList()).ToList();
            }

            public long Magnitude()
            {
                if (RegularNumberValue != null)
                {
                    return RegularNumberValue.Value;
                }

                else
                {
                    var item1Magnitude = Item1.Magnitude();
                    var item2Magnitude = Item2.Magnitude();
                    // Console.WriteLine(RawData);
                    // Console.WriteLine("" + item1Magnitude + " | " + item2Magnitude);

                    return item1Magnitude * 3 + item2Magnitude * 2;
                }
            }

            public IEnumerable<int> RegularNumbers()
            {
                if (RegularNumberValue != null)
                {
                    return new[] { RegularNumberValue.Value };
                }

                return Item1.RegularNumbers().Concat(Item2.RegularNumbers());
            }

            public int MaxNestingLevel()
            {
                return IsRegular ? NestingLvl : Math.Max(Item1.MaxNestingLevel(), Item2.MaxNestingLevel());
            }

            public bool ShouldExplode() => MaxNestingLevel() >= ExplosionCutoffNestingLevel;

            public bool ShouldSplit() => RegularNumbers().Any(x => x >= SplitMaxValueCutoff);

            public SnailNumber Reduce()
            {
                var result = this;
                var periodicityShouldExplode = true;

                while (result.ShouldExplode() || result.ShouldSplit())
                {
                    if (periodicityShouldExplode)
                    {
                        result = result.Explode();
                        periodicityShouldExplode = periodicityShouldExplode;
                    }
                    else
                    {
                        result = result.Split();
                        periodicityShouldExplode = !periodicityShouldExplode;
                    }
                }

                return result;
            }

            public SnailNumber Split()
            {
                var regularNumbers = RegularNumbersList();

                var toSplit = regularNumbers.First(r => r.RegularNumberValue >= SplitMaxValueCutoff);

                var val1 = toSplit.RegularNumberValue.Value / 2;
                var val2 = (toSplit.RegularNumberValue.Value + 1) / 2;

                toSplit.Item1 = new SnailNumber(val1.ToString(), toSplit.NestingLvl, toSplit.Parent);
                toSplit.Item2 = new SnailNumber(val2.ToString(), toSplit.NestingLvl, toSplit.Parent);
                toSplit.RegularNumberValue = null;

                return this;
            }

            public SnailNumber Explode()
            {
                var nonRegularNumbersList = NonRegularNumbersList();
                var regularNumbers = RegularNumbersList();

                var toExplode = nonRegularNumbersList.First(x => x.NestingLvl >= ExplosionCutoffNestingLevel);

                var explodeIndex = nonRegularNumbersList.IndexOf(toExplode);
                var regularExplodeIndexes = new[] { regularNumbers.IndexOf(toExplode.Item1), regularNumbers.IndexOf(toExplode.Item2) };
                var regularExplodeIndexLeft = regularExplodeIndexes.Min();
                var regularExplodeIndexRight = regularExplodeIndexes.Max();

                // All examples has val1 and val2 being regular numbers - not sure what to do if this isn't correct.
                var val1 = toExplode.Item1.RegularNumberValue.Value;
                var val2 = toExplode.Item2.RegularNumberValue.Value;

                if (toExplode.IsItem1)
                {
                    // right
                    toExplode.Parent.Item2.RegularNumbersList().First().RegularNumberValue += val2;

                    // left
                    var prev = regularExplodeIndexLeft != 0 ? regularNumbers[regularExplodeIndexLeft - 1] : null;
                    if (prev != null)
                    {
                        prev.RegularNumberValue += val1;
                    }
                }

                if (toExplode.IsItem2)
                {
                    // left
                    toExplode.Parent.Item1.RegularNumbersList().Last().RegularNumberValue += val1;

                    // right
                    var prev = regularExplodeIndexRight < regularNumbers.Count - 1 ? regularNumbers[regularExplodeIndexRight + 1] : null;
                    if (prev != null)
                    {
                        prev.RegularNumberValue += val2;
                    }
                }

                toExplode.Item1 = null;
                toExplode.Item2 = null;
                toExplode.RegularNumberValue = 0;

                return this;
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