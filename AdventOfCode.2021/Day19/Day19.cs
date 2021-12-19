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
            // Z-UP
            // 0 Neg
            ZUp_XPos_YPos_ZPos,

            // 1 Neg
            ZUp_XPos_YPos_ZNeg,
            ZUp_XPos_YNeg_ZPos,
            ZUp_XNeg_YPos_ZPos,

            // 2 Neg
            ZUp_XPos_YNeg_ZNeg,
            ZUp_XNeg_YPos_ZNeg,
            ZUp_XNeg_YNeg_ZPos,

            // 3 Neg
            ZUp_XNeg_YNeg_ZNeg,

            // Y-UP
            // 0 Neg
            YUp_XPos_YPos_ZPos,

            // 1 Neg
            YUp_XPos_YPos_ZNeg,
            YUp_XPos_YNeg_ZPos,
            YUp_XNeg_YPos_ZPos,

            // 2 Neg
            YUp_XPos_YNeg_ZNeg,
            YUp_XNeg_YPos_ZNeg,
            YUp_XNeg_YNeg_ZPos,

            // 3 Neg
            YUp_XNeg_YNeg_ZNeg,

            // X-UP
            // 0 Neg
            XUp_XPos_YPos_ZPos,

            // 1 Neg
            XUp_XPos_YPos_ZNeg,
            XUp_XPos_YNeg_ZPos,
            XUp_XNeg_YPos_ZPos,

            // 2 Neg
            XUp_XPos_YNeg_ZNeg,
            XUp_XNeg_YPos_ZNeg,
            XUp_XNeg_YNeg_ZPos,

            // 3 Neg
            XUp_XNeg_YNeg_ZNeg,
            
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
                    Orientation.ZUp_XPos_YPos_ZPos => Multiply((x,y,z), (1,1,1)),
                    Orientation.ZUp_XPos_YPos_ZNeg => Multiply((x,y,z), (1,1,-1)),
                    Orientation.ZUp_XPos_YNeg_ZPos => Multiply((x,y,z), (1,-1,1)),
                    Orientation.ZUp_XNeg_YPos_ZPos => Multiply((x,y,z), (-1,1,1)),
                    Orientation.ZUp_XPos_YNeg_ZNeg => Multiply((x,y,z), (1,-1,-1)),
                    Orientation.ZUp_XNeg_YPos_ZNeg => Multiply((x,y,z), (-1,1,-1)),
                    Orientation.ZUp_XNeg_YNeg_ZPos => Multiply((x,y,z), (-1,-1,1)),
                    Orientation.ZUp_XNeg_YNeg_ZNeg => Multiply((x,y,z), (-1,-1,-1)),

                    Orientation.XUp_XPos_YPos_ZPos => Multiply((z,y,x), (1,1,1)),
                    Orientation.XUp_XPos_YPos_ZNeg => Multiply((z,y,x), (1,1,-1)),
                    Orientation.XUp_XPos_YNeg_ZPos => Multiply((z,y,x), (1,-1,1)),
                    Orientation.XUp_XNeg_YPos_ZPos => Multiply((z,y,x), (-1,1,1)),
                    Orientation.XUp_XPos_YNeg_ZNeg => Multiply((z,y,x), (1,-1,-1)),
                    Orientation.XUp_XNeg_YPos_ZNeg => Multiply((z,y,x), (-1,1,-1)),
                    Orientation.XUp_XNeg_YNeg_ZPos => Multiply((z,y,x), (-1,-1,1)),
                    Orientation.XUp_XNeg_YNeg_ZNeg => Multiply((z,y,x), (-1,-1,-1)),
                    
                    Orientation.YUp_XPos_YPos_ZPos => Multiply((x,z,y), (1,1,1)),
                    Orientation.YUp_XPos_YPos_ZNeg => Multiply((x,z,y), (1,1,-1)),
                    Orientation.YUp_XPos_YNeg_ZPos => Multiply((x,z,y), (1,-1,1)),
                    Orientation.YUp_XNeg_YPos_ZPos => Multiply((x,z,y), (-1,1,1)),
                    Orientation.YUp_XPos_YNeg_ZNeg => Multiply((x,z,y), (1,-1,-1)),
                    Orientation.YUp_XNeg_YPos_ZNeg => Multiply((x,z,y), (-1,1,-1)),
                    Orientation.YUp_XNeg_YNeg_ZPos => Multiply((x,z,y), (-1,-1,1)),
                    Orientation.YUp_XNeg_YNeg_ZNeg => Multiply((x,z,y), (-1,-1,-1)),
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


            public bool DoesOverlap(Scanner other, int overlapNeeded = 12)
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