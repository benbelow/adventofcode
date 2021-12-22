using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day22
{
    public static class Day22
    {
        private const int Day = 22;

        public class CuboidRule
        {
            public bool On { get; set; }
            public int x1 { get; set; }
            public int x2 { get; set; }
            public int y1 { get; set; }
            public int y2 { get; set; }
            public int z1 { get; set; }
            public int z2 { get; set; }
            
            public bool IsSmallEnoughForPart1 { get; set; }
            
            public CuboidRule(string line)
            {
                var splitSpace = line.Split(" ");
                On = splitSpace.First() == "on";
                var coords = splitSpace[1].Split(",");
                var x = coords[0].Split("=")[1].Split("..");
                x1 = int.Parse(x.First());
                x2 = int.Parse(x.Last());
                var y = coords[1].Split("=")[1].Split("..");
                y1 = int.Parse(y.First());
                y2 = int.Parse(y.Last());
                var z = coords[2].Split("=")[1].Split("..");
                z1 = int.Parse(z.First());
                z2 = int.Parse(z.Last());

                bool InBounds(int i) => i is >= -50 and <= 50;

                IsSmallEnoughForPart1 = InBounds(x1) && InBounds(x2) && InBounds(y1) & InBounds(y2) && InBounds(z1) && InBounds(z2);
            }
    }
        
        public static long Part1(bool isExample = false)
        {
            var cubes = new Dictionary<(int, int, int), bool>();

            bool GetCube((int, int, int) c) => cubes.ContainsKey(c) && cubes[c];
            void SetCube((int, int, int) c, bool v) => cubes[c] = v;
             
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var cuboids = lines.Select(l => new CuboidRule(l)).ToList();

            foreach (var cuboid in cuboids.Where(c => c.IsSmallEnoughForPart1))
            {
                for (var x = cuboid.x1; x <= cuboid.x2; x++)
                {
                 
                    for (var y = cuboid.y1; y <= cuboid.y2; y++)
                    {
                    
                        for (var z = cuboid.z1; z <= cuboid.z2; z++)
                        {
                            SetCube((x,y,z), cuboid.On);
                        }
                    }   
                }
            }

            return cubes.Count(c => c.Value);
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}