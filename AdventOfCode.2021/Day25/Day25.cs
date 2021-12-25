using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using NUnit.Framework;

namespace AdventOfCode._2021.Day25
{
    public static class Day25
    {
        private const int Day = 25;

        public class Cucumber
        {
            public bool isRight { get; set; }

            public Cucumber(char c, (int, int) coord)
            {
                if (c == '>')
                {
                    isRight = true;
                }
                else
                {
                    isRight = false;
                }

                this.Coord = coord;
            }

            public Cucumber()
            {
            }

            public (int, int) Coord { get; set; }
        }

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var height = lines.Count;
            var width = lines.First().Length;

            var cucs = new List<Cucumber>();
            var dict = new Dictionary<(int, int), Cucumber>();

            void CalculateDict()
            {
                dict = cucs.ToDictionary(c => c.Coord, c => c);
            }
            
            Cucumber GetCucAt((int, int) coord)
            {
                return dict.ContainsKey(coord) ? dict[coord] : null;
            }

            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var c = lines[y][x];
                    if (c != '.')
                    {
                        cucs.Add(new Cucumber(c, (x, y)));
                    }
                }
            }
            CalculateDict();

            (int, int) Next(bool isRight, (int, int) coord)
            {
                if (isRight)
                {
                    var nextX = coord.Item1 + 1 < width ? coord.Item1 + 1 : 0;
                    var nextY = coord.Item2;
                    return (nextX, nextY);
                }

                var nextX2 = coord.Item1;
                var nextY2 = coord.Item2 + 1 < height ? coord.Item2 + 1 : 0;
                return (nextX2, nextY2);
            }

            bool ShouldMove(Cucumber c)
            {
                var nextC = Next(c.isRight, c.Coord);
                if (nextC == (2, 2))
                {
                    var x = 0;
                }
                var cucAt = GetCucAt(nextC);
                return cucAt == null;
            }

            var tick = 0;
            var moved = 0;

            do
            {
                tick++;
                moved = 0;
                var updated = new List<Cucumber>();
                var toMoveRight = cucs
                    .Where(c => c.isRight)
                    .Where(ShouldMove).ToList();
                var stillRight = cucs
                    .Where(c => c.isRight)
                    .Where(c => !ShouldMove(c)).ToList();
                var down = cucs.Where(c => !c.isRight);
                foreach (var cucumber in toMoveRight)
                {
                        moved++;
                        updated.Add(
                            new Cucumber
                            {
                                isRight = cucumber.isRight,
                                Coord = Next(cucumber.isRight, cucumber.Coord)
                            });
                }

                updated.AddRange(stillRight);
                updated.AddRange(down);
                cucs = updated;
                CalculateDict();
                
                updated = new List<Cucumber>();
                
                var toMoveRDown = cucs
                    .Where(c => !c.isRight)
                    .Where(ShouldMove).ToList();
                var stillRDown = cucs
                    .Where(c => !c.isRight)
                    .Where(c => !ShouldMove(c)).ToList();
                var right = cucs.Where(c => c.isRight);
                foreach (var cucumber in toMoveRDown)
                {
                    moved++;
                    updated.Add(
                        new Cucumber
                        {
                            isRight = cucumber.isRight,
                            Coord = Next(cucumber.isRight, cucumber.Coord)
                        });
                }

                updated.AddRange(stillRDown);
                updated.AddRange(right);
                cucs = updated;
                CalculateDict();
                
            } while (moved != 0);


            return tick;
        }

    }
}