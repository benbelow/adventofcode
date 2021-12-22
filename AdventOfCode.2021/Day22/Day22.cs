using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day22
{
    public static class Day22
    {
        private const int Day = 22;

        public enum CubeType
        {
            // takes up space
            PosCube, 
            // cancels original poscube
            NegCube,
            // off without an on 
            TurnOff
        }
        
        public class Cuboid
        {
            public bool On { get; set; }
            public int x1 { get; set; }
            public int x2 { get; set; }
            public int y1 { get; set; }
            public int y2 { get; set; }
            public int z1 { get; set; }
            public int z2 { get; set; }

            public bool IsSmallEnoughForPart1 { get; set; }

            public CubeType CubeType { get; set; }

            public long Width => (x2 - x1) + 1;
            public long Height => (y2 - y1) + 1;
            public long Depth => (z2 - z1) + 1;
            public long Volume => Width * Height * Depth; 

            public Cuboid(string line)
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
                CubeType = On ? CubeType.PosCube : CubeType.TurnOff;
            }

            public Cuboid(CubeType cubeType, int x1, int x2, int y1, int y2, int z1, int z2)
            {
                On = cubeType == CubeType.PosCube;
                CubeType = cubeType;
                this.x1 = x1;
                this.x2 = x2;
                this.y1 = y1;
                this.y2 = y2;
                this.z1 = z1;
                this.z2 = z2;

                bool InBounds(int i) => i is >= -50 and <= 50;

                IsSmallEnoughForPart1 = InBounds(x1) && InBounds(x2) && InBounds(y1) & InBounds(y2) && InBounds(z1) && InBounds(z2);
            }

            public Cuboid BuildIntersection(Cuboid other)
            {
                var newX1 = Math.Max(this.x1, other.x1);
                var newX2 = Math.Min(this.x2, other.x2);
                var xIntersect = newX2 >= newX1;
                
                var newY1 = Math.Max(this.y1, other.y1);
                var newY2 = Math.Min(this.y2, other.y2);
                var yIntersect = newY2 >= newY1;
                
                var newZ1 = Math.Max(this.z1, other.z1);
                var newZ2 = Math.Min(this.z2, other.z2);
                var zIntersect = newZ2 >= newZ1;

                var intersectType = IntersectType(this.CubeType, other.CubeType);
                return xIntersect && yIntersect && zIntersect && intersectType != null
                    ? new Cuboid(intersectType.Value, newX1, newX2, newY1, newY2, newZ1, newZ2)
                    : null;
            }
        }

        public static CubeType? IntersectType(CubeType original, CubeType intersector)
        {
            var pair = (a: original, b: intersector);
            return pair switch
            {
                (CubeType.PosCube, CubeType.PosCube) => CubeType.NegCube,
                (CubeType.NegCube, CubeType.PosCube) => CubeType.PosCube,
                (CubeType.TurnOff, CubeType.PosCube) => null,
                
                (CubeType.PosCube, CubeType.NegCube) => throw new NotImplementedException(),
                (CubeType.NegCube, CubeType.NegCube) => throw new NotImplementedException(),
                (CubeType.TurnOff, CubeType.NegCube) => throw new NotImplementedException(),
                
                (CubeType.PosCube, CubeType.TurnOff) => CubeType.NegCube,
                (CubeType.NegCube, CubeType.TurnOff) => CubeType.PosCube,
                (CubeType.TurnOff, CubeType.TurnOff) => null,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var cuboids = lines.Select(l => new Cuboid(l)).ToList();

            var handledCuboids = new List<Cuboid>();
            
            foreach (var cuboid in cuboids.Where(c => c.IsSmallEnoughForPart1))
            {
                var newBoids = new List<Cuboid>();
                foreach (var placedCuboid in handledCuboids)
                {
                    var intersectCube = placedCuboid.BuildIntersection(cuboid);
                    if (intersectCube != null)
                    {
                        newBoids.Add(intersectCube);
                    }
                }

                handledCuboids.AddRange(newBoids);
                handledCuboids.Add(cuboid);
            }

            var posCubes = handledCuboids.Where(c => c.CubeType == CubeType.PosCube);
            var negCubes = handledCuboids.Where(c => c.CubeType == CubeType.NegCube);
            var turnOffs = handledCuboids.Where(c => c.CubeType == CubeType.TurnOff);
            var posVolume = posCubes.Sum(c => c.Volume);
            var negVolume = negCubes.Sum(c => c.Volume);
            var offVolume = turnOffs.Sum(c => c.Volume);
            
            return posVolume - negVolume;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var cuboids = lines.Select(l => new Cuboid(l)).ToList();

            var handledCuboids = new List<Cuboid>();
            
            foreach (var cuboid in cuboids)
            {
                var newBoids = new List<Cuboid>();
                foreach (var placedCuboid in handledCuboids)
                {
                    var intersectCube = placedCuboid.BuildIntersection(cuboid);
                    if (intersectCube != null)
                    {
                        newBoids.Add(intersectCube);
                    }
                }

                handledCuboids.AddRange(newBoids);
                handledCuboids.Add(cuboid);
            }

            var posCubes = handledCuboids.Where(c => c.CubeType == CubeType.PosCube);
            var negCubes = handledCuboids.Where(c => c.CubeType == CubeType.NegCube);
            return posCubes.Sum(c => c.Volume) - negCubes.Sum(c => c.Volume);
        }
    }
}