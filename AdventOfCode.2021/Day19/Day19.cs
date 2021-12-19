using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

// ReSharper disable InconsistentNaming

namespace AdventOfCode._2021.Day19
{
    public static class Day19
    {
        private const int Day = 19;

        // 24 different orientations 
        public enum Orientation
        {
            FacePosX_UpPosY,
            FacePosX_UpNegY,
            FacePosX_UpPosZ,
            FacePosX_UpNegZ,
            
            FaceNegX_UpPosY,
            FaceNegX_UpNegY,
            FaceNegX_UpPosZ,
            FaceNegX_UpNegZ,
            
            FacePosY_UpPosX,
            FacePosY_UpNegX,
            FacePosY_UpPosZ,
            FacePosY_UpNegZ,
            
            FaceNegY_UpPosX,
            FaceNegY_UpNegX,
            FaceNegY_UpPosZ,
            FaceNegY_UpNegZ,
            
            FacePosZ_UpPosY,
            FacePosZ_UpNegY,
            FacePosZ_UpPosX,
            FacePosZ_UpNegX,
            
            FaceNegZ_UpPosY,
            FaceNegZ_UpNegY,
            FaceNegZ_UpPosX,
            FaceNegZ_UpNegX,
        }

        public class Scanner
        {
            public int Id { get; set; }

            // Default Orientation = Zup, XYZ all Pos
            public List<(int, int, int)> Beacons { get; set; }

            public HashSet<Orientation> PossibleOrientations = Enum.GetValues(typeof(Orientation)).Cast<Orientation>().ToHashSet();

            public Dictionary<Orientation, List<(int, int, int)>> BeaconsByOrientation = new Dictionary<Orientation, List<(int, int, int)>>();

            // Manhattan distances from this beacon to all others in scanner  
            public Dictionary<(int, int, int), List<int>> DistancesPerBeacon = new Dictionary<(int, int, int), List<int>>();

            public Scanner(List<string> rawData)
            {
                Id = int.Parse(rawData.First().Split("scanner ").Last().Split(" -").First());
                var beaconLines = rawData.Skip(1);
                var ints = beaconLines.Select(line => line.Split(',').Select(int.Parse).ToList()).ToList();
                Beacons = ints.Select(q => (q[0], q[1], q[2])).ToList();

                // foreach (var orientation in PossibleOrientations)
                // {
                //     BeaconsByOrientation[orientation] = Beacons.Select(b => TransformCoord(b, orientation)).ToList();
                // }

                CalculateDistancesPerBeacon();
            }

            private void CalculateDistancesPerBeacon()
            {
                DistancesPerBeacon = new Dictionary<(int, int, int), List<int>>();
                foreach (var beacon in Beacons)
                {
                    var beaconDists = new List<int>();
                    foreach (var otherBeacon in Beacons.Except(new[] { beacon }))
                    {
                        beaconDists.Add(ManhattanDistance(beacon, otherBeacon));
                    }

                    DistancesPerBeacon[beacon] = beaconDists;
                }
            }

            public int ManhattanDistance((int, int, int) c1, (int, int, int) c2)
            {
                return Math.Abs(c2.Item1 - c1.Item1) + Math.Abs(c2.Item2 - c1.Item2) + Math.Abs(c2.Item3 - c1.Item3);
            }

            public (int, int, int) Multiply((int, int, int) c1, (int, int, int) c2)
            {
                return (c1.Item1 * c2.Item1, c1.Item2 * c2.Item2, c1.Item3 * c2.Item3);
            }
            
            public (int, int, int) TransformCoord((int, int, int) original, Orientation orientation)
            {
                var (x, y, z) = original;
                return orientation switch
                {
                    Orientation.FacePosX_UpPosY => Multiply((z, y, x), (-1, +1, +1)),
                    Orientation.FacePosX_UpNegY => Multiply((z, y, x), (+1, -1, +1)),
                    Orientation.FaceNegX_UpPosY => Multiply((z, y, x), (+1, +1, -1)),
                    Orientation.FaceNegX_UpNegY => Multiply((z, y, x), (-1, -1, -1)),
                    Orientation.FacePosX_UpPosZ => Multiply((y, z, x), (+1, +1, +1)),
                    Orientation.FacePosX_UpNegZ => Multiply((y, z, x), (-1, -1, +1)),
                    Orientation.FaceNegX_UpPosZ => Multiply((y, z, x), (-1, +1, -1)),
                    Orientation.FaceNegX_UpNegZ => Multiply((y, z, x), (+1, -1, -1)),
                    
                    Orientation.FacePosY_UpPosX => Multiply((z, x, y), (+1, +1, +1)),
                    Orientation.FacePosY_UpNegX => Multiply((z, x, y), (-1, -1, +1)),
                    Orientation.FaceNegY_UpPosX => Multiply((z, x, y), (-1, +1, -1)),
                    Orientation.FaceNegY_UpNegX => Multiply((z, x, y), (+1, -1, -1)),
                    Orientation.FacePosY_UpPosZ => Multiply((x, z, y), (-1, +1, +1)),
                    Orientation.FacePosY_UpNegZ => Multiply((x, z, y), (+1, -1, +1)),
                    Orientation.FaceNegY_UpPosZ => Multiply((x, z, y), (+1, +1, -1)),
                    Orientation.FaceNegY_UpNegZ => Multiply((x, z, y), (-1, -1, -1)),
                    
                    Orientation.FacePosZ_UpPosY => Multiply((x, y, z), (+1, +1, +1)),
                    Orientation.FacePosZ_UpNegY => Multiply((x, y, z), (-1, -1, +1)),
                    Orientation.FaceNegZ_UpPosY => Multiply((x, y, z), (-1, +1, -1)),
                    Orientation.FaceNegZ_UpNegY => Multiply((x, y, z), (+1, -1, -1)),
                    Orientation.FacePosZ_UpPosX => Multiply((y, x, z), (-1, +1, +1)),
                    Orientation.FacePosZ_UpNegX => Multiply((y, x, z), (+1, -1, +1)),
                    Orientation.FaceNegZ_UpPosX => Multiply((y, x, z), (+1, +1, -1)),
                    Orientation.FaceNegZ_UpNegX => Multiply((y, x, z), (-1, -1, -1)),
                    _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
                };
            }

            public (int, int, int) NormaliseCoord((int, int, int) original, (int, int, int) newOrigin)
            {
                return (
                    original.Item1 - newOrigin.Item1,
                    original.Item2 - newOrigin.Item2,
                    original.Item3 - newOrigin.Item3
                );
            }

            public List<(int, int, int)> BeaconsInOrientation(Orientation orientation)
            {
                return BeaconsByOrientation[orientation];
            }


            public bool DoesOverlap(Scanner other, int overlapNeeded = +12)
            {
                var possibleOverlaps = PossibleOverlaps(other).Where(o => o >= overlapNeeded -1).ToList();
                // Console.WriteLine($"{Id} vs {other.Id}: OVERLAPS: {string.Join(",", possibleOverlaps)}");
                return possibleOverlaps.Any() && possibleOverlaps.Max() >= overlapNeeded - 1;
            }

            public IEnumerable<int> PossibleOverlaps(Scanner other)
            {
                foreach (var myBeacon in Beacons)
                {
                    foreach (var theirBeacon in other.Beacons)
                    {
                        var overlap = DistancesPerBeacon[myBeacon].Intersect(other.DistancesPerBeacon[theirBeacon]).Count();
                        if (overlap > 0)
                        {
                            yield return overlap;
                        }
                    }
                }
            }
            
            public List<Scanner> PairedScanners { get; set; }

            public (int, int, int) Minus((int, int, int) c1, (int, int, int) c2)
            {
                return (c1.Item1 - c2.Item1, c1.Item2 - c2.Item2, c1.Item3 - c2.Item3);
            }
            
            public void OrientTo(Scanner origin)
            {
                Console.WriteLine($"Orienting {Id} to {origin.Id}...");
                var newPossible = new List<Orientation>();

                var beaconPairs = DistancesPerBeacon.SelectMany(kvp =>
                {
                    var myDistances = kvp.Value;
                    var theirKvp = origin.DistancesPerBeacon
                        .SingleOrDefault(d => d.Value.Intersect(myDistances).Count() >= 11);
                    if (theirKvp.Key != (0, 0, 0))
                    {
                        return new[] { (kvp.Key, theirKvp.Key) }.ToList();
                    }
                    else return new List<((int, int, int), (int, int, int))>();
                }).ToList();

                Console.WriteLine(beaconPairs.Count);
                
                foreach (var beaconPair in beaconPairs)
                {
                    // Console.WriteLine("" + beaconPair.Item1 + beaconPair.Item2);
                }
                
                // Console.WriteLine("_____________");
                
                foreach (var orientation in PossibleOrientations)
                {
                    // Console.WriteLine(orientation + ":");
                    var orientatedPairs = beaconPairs.Select(p => (TransformCoord(p.Item1, orientation), p.Item2));
                    var pairDiffs = orientatedPairs.Select(p => Minus(p.Item1, p.Item2)).ToList();

                    // Console.WriteLine(pairDiffs.Distinct().Count());

                    if (pairDiffs.Distinct().Count() < 3)
                    {
                        Console.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    
                    if (pairDiffs.Distinct().Count() == 1)
                    {
                        newPossible.Add(orientation);
                    }
                }

                if (newPossible.Distinct().Count() != 1)
                {
                    Console.WriteLine("FAILED TO ORIENTATE");
                }
                else
                {
                    PossibleOrientations = newPossible.ToHashSet();
                    Beacons = Beacons.Select(b => TransformCoord(b, PossibleOrientations.Single())).ToList();
                    CalculateDistancesPerBeacon();
                    Console.WriteLine($"Oriented {Id} to {origin.Id}: New orientation: {PossibleOrientations.Single()}");
                }
            }
        }

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var rawDatas = lines.DoubleSplit("");
            var scanners = rawDatas.First().Select(q => new Scanner(q.ToList())).ToList();
            
            var scannerPairs = new List<(Scanner, Scanner)>();
            foreach (var scanner in scanners)
            {
                foreach (var otherScanner in scanners.Where(s=> s.Id != scanner.Id))
                {
                    var overlaps = scanner.DoesOverlap(otherScanner);
                    var emoji = overlaps ? "✔" : "❌";
                    Console.WriteLine($"{scanner.Id} vs {otherScanner.Id}: {emoji}");
                    if (!scannerPairs.Contains((otherScanner, scanner)) && overlaps)
                    {
                        scannerPairs.Add((scanner, otherScanner));
                    }
                }
            }

            var originScanner = scannerPairs.First().Item1;
            foreach (var g in scannerPairs.GroupBy(p => p.Item1))
            {
                g.Key.PairedScanners = g.Select(q => q.Item2).ToList();
            }

            foreach (var scannerPair in scannerPairs)
            {
                scannerPair.Item2.OrientTo(scannerPair.Item1);
            }

            return scanners.Sum(s => s.Beacons.Count);
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}