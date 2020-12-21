using System;
using System.Collections.Generic;
using System.Linq;
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
                this.knowAllergens = knowAllergens.ToHashSet();
                excludedAllergens = new HashSet<string>();
            }

            public ISet<string> Allergens()
            {
                return knowAllergens.Except(excludedAllergens).ToHashSet();
            }
        }
        
        
        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();

            var ingredientDict = new Dictionary<string, Ingredient>();
            
            var allAllergens = new HashSet<string>();

            allAllergens = lines.Select(foodLine => foodLine.Split("(contains "))
                .Select(allergenSplit => allergenSplit.Length > 1
                    ? allergenSplit[1].Split(")")[0].Split(",").Select(x => x.Trim()).ToList()
                    : new List<string>())
                .Aggregate(allAllergens, (current, knownAllergens) => current.Concat(knownAllergens).ToHashSet());


            var allergenDict = new Dictionary<string, HashSet<string>>();
            var finalAllergenDict = new Dictionary<string, string>();
            
            foreach (var allergen in allAllergens)
            {
                var facts = lines.Where(l =>
                {
                    var split = l.Split("(");
                    var ingredientSection = split.Length > 1 ? split[1] : "";
                    return ingredientSection.Contains(allergen);
                });
                var options = facts.Select(f => f.Split("(").First().Trim().Split().Select(x => x.Trim()));

                var ingredientOptions = options.Aggregate(options.First().ToHashSet(), (agg, x) => 
                    agg.Intersect(x).ToHashSet()
                );

                allergenDict[allergen] = ingredientOptions;
            }

            while (allergenDict.Values.Any(x => x.Count == 1))
            {
                var allergenPair = allergenDict.First(x => x.Value.Count == 1);
                var ingredient = allergenPair.Value.Single();
                finalAllergenDict[allergenPair.Key] = ingredient;
                allergenDict.Remove(allergenPair.Key);
                foreach (var ad in allergenDict)
                {
                    ad.Value.Remove(ingredient);
                }
            }

            if (allergenDict.Any(x => x.Value.Count == 0))
            {
                throw new Exception("Found allergen that cannot be assigned.");
            }
            
            foreach (var foodLine in lines)
            {
                var allergenSplit = foodLine.Split("(contains ");
                var knownAllergens = allergenSplit.Length > 1
                    ? allergenSplit[1].Split(")")[0].Split(",").Select(x => x.Trim()).ToList()
                    : new List<string>();

                foreach (var allergen in knownAllergens)
                {
                    allAllergens.Add(allergen);
                }
                
                var ingredients = allergenSplit[0].Trim().Split().Select(x => x.Trim());

                foreach (var rawIngredient in ingredients)
                {
                    var ingredient = ingredientDict.GetValueOrDefault(rawIngredient);
                    if (ingredient == null)
                    {
                        ingredient = new Ingredient(rawIngredient, knownAllergens);
                        ingredientDict[rawIngredient] = ingredient;
                    }

                    ingredient.seenCount++;

                    ingredient.knowAllergens = ingredient.knowAllergens.Concat(knownAllergens).Distinct().ToHashSet();
                    // var newExcluded = existingAllergens.Except(knownAllergens);
                    // ingredient.excludedAllergens = ingredient.excludedAllergens.Concat(newExcluded).ToHashSet();
                }
            }
            
            var unassignedAllergens = allergenDict.Keys;
            var badIngredients = finalAllergenDict.Values.ToHashSet();
            var potentiallyBadIngredients = allergenDict.Values.SelectMany(x => x).ToHashSet();
            
            bool IsSafe(Ingredient ingredient)
            {
                return !badIngredients.Contains(ingredient.name) && !potentiallyBadIngredients.Contains(ingredient.name);
            }

            var safeIngredients = ingredientDict.Values.Where(IsSafe);
            
            
            var assignedAllergens = new HashSet<string>();

            foreach (var allergen in allAllergens)
            {
                var possibles = ingredientDict.Where(i => i.Value.knowAllergens.Contains(allergen));
                if (possibles.Count() == 1)
                {
                    throw new Exception("HAHA");
                }
            }

            var repeats = 0;
            while (ingredientDict.Values.Any(i => i.knowAllergens.Count == 1) && repeats != 2)
            {
                var ingredientWithKnownAllergen = ingredientDict.Values.First(i => i.knowAllergens.Count == 1);

                var oldLength = assignedAllergens.Count;
                assignedAllergens.Add(ingredientWithKnownAllergen.knowAllergens.Single());

                if (assignedAllergens.Count == oldLength)
                {
                    repeats++;
                }
                else
                {
                    repeats = 0;
                }
                
                foreach (var ingredient in ingredientDict)
                {
                    if (ingredient.Key == ingredientWithKnownAllergen.name)
                    {
                        continue;
                    }

                    var i2 = ingredientDict[ingredient.Key];
                    i2.knowAllergens = i2.knowAllergens.Where(a => !assignedAllergens.Contains(a)).ToHashSet();
                }
            }

            // var safeIngredients = ingredientDict.Values.Where(i => i.Allergens().Count == 0);
            return safeIngredients.Sum(i => i.seenCount);
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return -1;
        }
    }
}