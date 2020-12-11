using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day06
{
    [TestFixture]
    public class Day06Tests
    {
        [TestCase(@"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L", 42)]
        public void CountDirectAndIndirectOrbits(string input, int expectedCount)
        {
            var lines = input.Split('\n');
            
            var count = Day06.CountDirectAndIndirectOrbits(lines);

            count.Should().Be(expectedCount);
        }

        [Test]
        public void Part1()
        {
            Day06.Part1().Should().Be(204521);
        }

        [Test]
        public void Part2()
        {
            Day06.Part2().Should().Be(307);
        }
        
        [TestCase(@"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L
K)YOU
I)SAN", 4)]
        public void CountMinimumOrbitalPath(string input, int expectedPath)
        {
            var lines = input.Split('\n');
            
            var path = Day06.CalculateMinimumOrbitalTransfers(lines, "YOU", "SAN");

            path.Should().Be(expectedPath);
        }
    }
}