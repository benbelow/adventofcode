using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day14
{
    public static class Day14
    {
        public static double CountRequiredOre(string rawData, string desiredProduct)
        {
            var recipes = ParseRecipes(rawData.Split("\n"));
            return Math.Ceiling(recipes.CountOre(desiredProduct));
        }

        private static double CountOre(this IList<Recipe> recipes, string desiredProduct, int desiredAmount = 1)
        {
            return recipes.CountOre(new List<ChemicalAmount> {new ChemicalAmount {Name = desiredProduct, Amount = desiredAmount}});
        }

        private static double CountOre(this IList<Recipe> recipes, IReadOnlyCollection<ChemicalAmount> desiredProducts)
        {
            const string ore = "ORE";

            if (desiredProducts.All(p => p.Name == ore))
            {
                return desiredProducts.Sum(d => d.Amount);
            }

            var nextLevel = desiredProducts.SelectMany(d =>
            {
                if (d.Name == ore)
                {
                    return new List<ChemicalAmount> {d};
                }

                var recipe = recipes.GetRecipe(d.Name);
                return recipe.Inputs.Select(i =>
                {
                    if (i.Name == ore)
                    {
                        var recipeCountNeeded = (d.Amount + recipe.Output.Amount - 1) / recipe.Output.Amount;
                        return new ChemicalAmount
                        {
                            Amount = recipeCountNeeded * i.Amount,
                            Name = ore
                        };
                    }

                    return new ChemicalAmount
                    {
                        Amount = i.Amount * d.Amount,
                        Name = i.Name
                    };
                });
            });

            return recipes.CountOre(nextLevel.Aggregate(new List<ChemicalAmount>(), (list, amount) =>
            {
                var existing = list.SingleOrDefault(x => x.Name == amount.Name);
                if (existing != null)
                {
                    existing.Amount += amount.Amount;
                }
                else
                {
                    list.Add(amount);
                }

                return list;
            }));
        }

        private static Recipe GetRecipe(this IEnumerable<Recipe> recipes, string target)
        {
            return recipes.Single(r => r.Output.Name == target);
        }

        private static IList<Recipe> ParseRecipes(IEnumerable<string> lines)
        {
            return lines.Select(l =>
            {
                var lhs = l.Split("=>")[0];
                var rhs = l.Split("=>")[1];
                return new Recipe
                {
                    Output = ParseChemicalAmount(rhs),
                    Inputs = lhs.Split(",").Select(ParseChemicalAmount).ToList()
                };
            }).ToList();
        }

        private static ChemicalAmount ParseChemicalAmount(string pair)
        {
            return new ChemicalAmount
            {
                Amount = int.Parse(pair.Trim().Split(" ").First()),
                Name = pair.Trim().Split(" ").Last()
            };
        }

        private class Recipe
        {
            public ChemicalAmount Output { get; set; }
            public List<ChemicalAmount> Inputs { get; set; }
        }

        private class ChemicalAmount
        {
            public int Amount { get; set; }
            public string Name { get; set; }
        }
    }
}