using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day21
{
    public static class Day21
    {
        private const int Day = 21;

        public class Ingredient
        {
            public readonly string name;
            public ISet<string> knowAllergens;
            public int seenCount = 0;

            public ISet<string> excludedAllergens;

            public Ingredient(string name, List<string> knowAllergens)
            {
                this.name = name;
                this.knowAllergens = Enumerable.ToHashSet(knowAllergens);
                excludedAllergens = new HashSet<string>();
            }

            public ISet<string> Allergens()
            {
                return Enumerable.ToHashSet(knowAllergens.Except(excludedAllergens));
            }
        }


        public static long Part1()
        {
            return int.Parse(Day21Code());
        }

        private static string Day21Code(bool part2 = false)
        {
            var lines = FileReader.ReadInputLines(Day).ToList();

            var allergens = new Dictionary<string, HashSet<string>>();

            var ingredientLines = new List<List<string>>();

            foreach (var line in lines)
            {
                var split = line.Split("(contains");
                var lineIngredients = split.First().Trim().Split().Select(x => x.Trim()).ToList();
                var lineAllergens = split.Count() > 1 ? split[1].Replace(")", "").Split(",").Select(x => x.Trim()) : new List<string>();

                ingredientLines.Add(lineIngredients.ToList());

                foreach (var allergen in lineAllergens)
                {
                    if (!allergens.ContainsKey(allergen))
                    {
                        allergens[allergen] = lineIngredients.ToHashSet();
                    }

                    else
                    {
                        allergens[allergen] = allergens[allergen].Intersect(lineIngredients.ToHashSet()).ToHashSet();
                    }
                }
            }

            while (allergens.Values.Any(x => x.Count != 1))
            {
                var singles = allergens.Where(a => a.Value.Count == 1);
                foreach (var single in singles)
                {
                    allergens = allergens.ToDictionary(
                        a => a.Key,
                        a => a.Key == single.Key ? a.Value : a.Value.Except(new[] {single.Value.Single()}).ToHashSet()
                    );
                }
            }


            var badIngredients = allergens.Values.SelectMany(x => x).ToHashSet();

            if (!part2)
            {
                return ingredientLines.Sum(line => line.Count(i => !badIngredients.Contains(i))).ToString();
            }
            else
            {
                var orderedAllergens = allergens.OrderBy(a => a.Key);
                var orderedBadIngredients = orderedAllergens.Select(a => a.Value.Single());

                return string.Join(",", orderedBadIngredients);
            }
        }

        public static string Part2()
        {
            return Day21Code(true);
        }
    }
}