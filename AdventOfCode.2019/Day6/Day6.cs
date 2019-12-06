using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;

namespace AdventOfCode._2019.Day6
{
    public static class Day6
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(6).ToList();
            return CountDirectAndIndirectOrbits(lines);
        }
        
        public static int CountDirectAndIndirectOrbits(IEnumerable<string> inputLines)
        {
            inputLines = inputLines.Select(l => l.Trim());
            var celestialBodies = new Dictionary<string, CelestialBody>();

            foreach (var inputLine in inputLines)
            {
                var orbiteeId = inputLine.Split(")").First();
                var orbiterId = inputLine.Split(")").Last();

                if (!celestialBodies.TryGetValue(orbiteeId, out var orbitee))
                {
                    orbitee = new CelestialBody{ Id = orbiteeId};
                    celestialBodies.Add(orbiteeId, orbitee);
                }

                if (!celestialBodies.TryGetValue(orbiterId, out var orbiter))
                {
                    orbiter = new CelestialBody{ Id = orbiterId};
                    celestialBodies.Add(orbiterId, orbiter);
                }
                
                orbitee.Orbiters.Add(orbiter);
                orbiter.Orbits = orbitee;
            }

            return celestialBodies.Values.Sum(c => c.CountOrbits());
        }

        private class CelestialBody
        {
            public string Id { get; set; }
            public IList<CelestialBody> Orbiters { get; } = new List<CelestialBody>();
            public CelestialBody Orbits { get; set; }

            public int CountOrbits() => Orbits == null ? 0 : 1 + Orbiters.Sum(o => o.CountOrbits());
        }
    }
}