using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day07
{
    public static class Day07
    {
        private const int Day = 7;

        public static long Part1()
        {
            var bags = ParseBags();
            var goldBag = bags["shiny gold"];

            return goldBag.ParentColors().ToHashSet().Count;
        }

        private static Dictionary<string, Bag> ParseBags()
        {
            var bags = new Dictionary<string, Bag>();

            var lines = FileReader.ReadInputLines(Day).ToList();
            foreach (var line in lines)
            {
                var split1 = line.Split("bags contain");
                var color = split1[0].Trim();
                var contains = split1[1].Split(",").Select(x => x.Trim());

                var bag = bags.GetValueOrDefault(color) ?? new Bag {Color = color};
                if (!bags.ContainsKey(color))
                {
                    bags[color] = bag;
                }

                foreach (var contained in contains)
                {
                    if (contained == "no other bags.")
                    {
                        continue;
                    }

                    var amountString = contained.Split()[0];
                    var amount = int.Parse(amountString);
                    var containedColor = contained.Replace(amountString, "").Split("bag")[0].Trim();

                    var otherBag = bags.GetValueOrDefault(containedColor) ?? new Bag {Color = containedColor};
                    if (!bags.ContainsKey(containedColor))
                    {
                        bags[containedColor] = otherBag;
                    }

                    otherBag.Parents.Add(bag);
                    bag.Children.Add((otherBag, amount));
                }
            }

            return bags;
        }

        public static long Part2()
        {
            var bags = ParseBags();
            var goldBag = bags["shiny gold"];

            return goldBag.CountBags();
        }

        public class Bag
        {
            public string Color { get; set; }
            public IList<(Bag, int)> Children { get; set; } = new List<(Bag, int)>();
            public IList<Bag> Parents { get; set; } = new List<Bag>();

            public List<string> ParentColors() => Parents.SelectMany(p => p.ParentColors().Concat(new[] {p.Color})).ToList();

            public long CountBags() => Children.Sum(c => c.Item2) + Children.Sum(c => c.Item1.CountBags() * c.Item2);
        }
    }
}