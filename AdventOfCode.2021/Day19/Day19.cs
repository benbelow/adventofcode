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

            public Scanner(List<string> rawData)
            {
                Id = int.Parse(rawData.First().Split("scanner ").Last().Split(" -").First());
                var beaconLines = rawData.Skip(1);
                var ints = beaconLines.Select(line => line.Split(',').Select(int.Parse).ToList()).ToList();
                Beacons = ints.Select(q => (q[0], q[1], q[2])).ToList();

                BeaconsByOrientation = new Dictionary<Orientation, List<(int, int, int)>>();
                foreach (var orientation in PossibleOrientations)
                {
                    BeaconsByOrientation[orientation] = Beacons.Select(b => TransformCoord(b, orientation)).ToList();
                }
            }

            public Dictionary<Orientation, List<(int, int, int)>> BeaconsByOrientation;

            public (int, int, int) TransformCoord((int, int, int) original, Orientation orientation)
            {
                return orientation switch
                {
                    Orientation.ZUp_XPos_YPos_ZPos => (original.Item1,      original.Item2,      original.Item3     ),
                    Orientation.ZUp_XPos_YPos_ZNeg => (original.Item1,      original.Item2,      original.Item3 * -1),
                    Orientation.ZUp_XPos_YNeg_ZPos => (original.Item1,      original.Item2 * -1, original.Item3     ),
                    Orientation.ZUp_XNeg_YPos_ZPos => (original.Item1 * -1, original.Item2,      original.Item3     ),
                    Orientation.ZUp_XPos_YNeg_ZNeg => (original.Item1,      original.Item2 * -1, original.Item3 * -1),
                    Orientation.ZUp_XNeg_YPos_ZNeg => (original.Item1 * -1, original.Item2,      original.Item3 * -1),
                    Orientation.ZUp_XNeg_YNeg_ZPos => (original.Item1 * -1, original.Item2 * -1, original.Item3     ),
                    Orientation.ZUp_XNeg_YNeg_ZNeg => (original.Item1 * -1, original.Item2 * -1, original.Item3 * -1),
                    
                    Orientation.XUp_XPos_YPos_ZPos => (original.Item3     , original.Item2     , original.Item1      ),
                    Orientation.XUp_XPos_YPos_ZNeg => (original.Item3 * -1, original.Item2     , original.Item1      ),
                    Orientation.XUp_XPos_YNeg_ZPos => (original.Item3     , original.Item2 * -1, original.Item1      ),
                    Orientation.XUp_XNeg_YPos_ZPos => (original.Item3     , original.Item2     , original.Item1 * -1 ),
                    Orientation.XUp_XPos_YNeg_ZNeg => (original.Item3 * -1, original.Item2 * -1, original.Item1      ),
                    Orientation.XUp_XNeg_YPos_ZNeg => (original.Item3 * -1, original.Item2     , original.Item1 * -1 ),
                    Orientation.XUp_XNeg_YNeg_ZPos => (original.Item3     , original.Item2 * -1, original.Item1 * -1 ),
                    Orientation.XUp_XNeg_YNeg_ZNeg => (original.Item3 * -1, original.Item2 * -1, original.Item1 * -1 ),
                    
                    Orientation.YUp_XPos_YPos_ZPos => (original.Item1     , original.Item3     , original.Item2     ),
                    Orientation.YUp_XPos_YPos_ZNeg => (original.Item1     , original.Item3 * -1, original.Item2     ),
                    Orientation.YUp_XPos_YNeg_ZPos => (original.Item1     , original.Item3     , original.Item2 * -1),
                    Orientation.YUp_XNeg_YPos_ZPos => (original.Item1 * -1, original.Item3     , original.Item2     ),
                    Orientation.YUp_XPos_YNeg_ZNeg => (original.Item1     , original.Item3 * -1, original.Item2 * -1),
                    Orientation.YUp_XNeg_YPos_ZNeg => (original.Item1 * -1, original.Item3 * -1, original.Item2     ),
                    Orientation.YUp_XNeg_YNeg_ZPos => (original.Item1 * -1, original.Item3     , original.Item2 * -1),
                    Orientation.YUp_XNeg_YNeg_ZNeg => (original.Item1 * -1, original.Item3 * -1, original.Item2 * -1),
                    _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
                };
            }
            
            public List<(int, int, int)> BeaconsInOrientation(Orientation orientation)
            {
                return BeaconsByOrientation[orientation];
            }

            public int OverlappingBeacons(Orientation orientation, Scanner other)
            {
                return 0;
            }
        }
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var rawDatas = lines.DoubleSplit("");
            var scanners = rawDatas.First().Select(q => new Scanner(q.ToList())).ToList();
            
            return -1;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}