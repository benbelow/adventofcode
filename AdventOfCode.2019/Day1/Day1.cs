using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;

namespace AdventOfCode._2019.Day1
{
    public static class Day1
    {
        public static int Part1()
        {
            return GetMasses().Select(CalculateFuelForMass).Sum();
        }

        public static int Part2()
        {
            return GetMasses().Select(CalculateTotalFuel).Sum();
        }

        private static IEnumerable<int> GetMasses()
        {
            var file = FileReader.ReadInputLines(1);
            var masses = file.Select(int.Parse);
            return masses;
        }

        public static int CalculateFuelForMass(int mass) => (mass / 3) - 2;

        public static int CalculateTotalFuel(int mass)
        {
            var fuel = Math.Max(0, CalculateFuelForMass(mass));
            return fuel <= 0 ? fuel : fuel + CalculateTotalFuel(fuel);
        }
    }
}