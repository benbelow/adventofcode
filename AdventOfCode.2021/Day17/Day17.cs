using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using NUnit.Framework;

namespace AdventOfCode._2021.Day17
{
    public static class Day17
    {
        private const int Day = 17;

        private static Dictionary<int, int> triangles = new Dictionary<int, int>();
        
        public static int Trianglise(int q)
        {
            if (q == 0)
            {
                return 0;
            }
            
            if (triangles.ContainsKey(q))
            {
                return triangles[q];
            }

            if (q == 1)
            {
                triangles[q] = 1;
            }
            
            triangles[q] = q + Trianglise(q - 1);

            return triangles[q];
        }
        
        public static long Part1(int x1, int x2, int y1, int y2)
        {
            bool TooFar((int, int) pos)
            {
                return pos.Item1 > x2 || pos.Item2 < y1;
            }
            
            bool OnTarget((int, int) pos)
            {
                return pos.Item1 <= x2 && pos.Item1 >= x1 || pos.Item2 <= y1 && pos.Item1 >= y2;
            }
            
            int? GetMaxIfValid((int, int) vel)
            {
                var initialVel = vel;
                var pos = (0, 0);
                var max = 0;
                var everOnTarget = false;
                while (true)
                {
                    pos = (pos.Item1 + vel.Item1, pos.Item2 + vel.Item2);
                    vel = (Math.Max(vel.Item1 - 1, 0), vel.Item2 - 1);
                    if (pos.Item2 > max)
                    {
                        max = pos.Item2;
                    }
                    
                    if (OnTarget(pos))
                    {
                        Console.WriteLine(pos);
                        Console.WriteLine(max);
                        Console.WriteLine("________________");
                        everOnTarget = true;
                    }
                    
                    if (TooFar(pos))
                    {
                        return everOnTarget ? max : null;
                    }
                }
            }
            var totalMax = 0;
            foreach (var x in Enumerable.Range(0, x2).Distinct())
            {
                foreach (var y in Enumerable.Range(0, y1 * -1).Distinct())
                {
                    Console.WriteLine((x, y));
                    var max = GetMaxIfValid((x, y));
                    if (max != null && max > totalMax)
                    {
                        totalMax = max.Value;
                    }
                }
            }

            return totalMax;
        }

        public static IEnumerable<int> XRange(int x1, int x2)
        {
            var tooFast = false;
            var initialMom = 0;
            while (!tooFast)
            {
                initialMom++;
                if (Trianglise(initialMom) < x1)
                {
                    continue;
                }

                var xTrack = 0;
                var mom = initialMom;
                while (true)
                {
                    xTrack += mom;
                    mom -= 1;
                    if (xTrack >= x1 && xTrack <= x2)
                    {
                        yield return initialMom;
                        break;
                    }

                    if (xTrack > x2)
                    {
                        tooFast = true;
                        break;
                    }
                    
                }
            }
        }

        public static IEnumerable<int> YRange(int y1, int y2)
        {
            bool tooFast = false;
            var initialMom = 0;
            while(!tooFast)
            {
                initialMom++;
                var yTrack = 0;
                var mom = initialMom;
                while (true)
                {
                    yTrack += mom;
                    mom -= 1;
                    if (yTrack >= y1 && yTrack <= y2)
                    {
                        yield return initialMom;
                        break;
                    }

                    if (yTrack < y1)
                    {
                        tooFast = true;
                        break;
                    }
                }

            }
        }
        
        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}