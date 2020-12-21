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
                        a => a.Key == single.Key ? a.Value : a.Value.Except(new [] {single.Value.Single()}).ToHashSet()
                    );
                }
            }


            var badIngredients = allergens.Values.SelectMany(x => x).ToHashSet();
            
            return ingredientLines.Sum(line => line.Count(i => !badIngredients.Contains(i)));
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return -1;
        }
    }
}