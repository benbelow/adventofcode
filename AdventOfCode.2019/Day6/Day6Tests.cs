using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day6
{
    [TestFixture]
    public class Day6Tests
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
            
            var count = Day6.CountDirectAndIndirectOrbits(lines);

            count.Should().Be(expectedCount);
        }

        [Test]
        public void Part1()
        {
            Day6.Part1().Should().Be(204521);
        }

        [Test]
        public void Part2()
        {
            Day6.Part2().Should().Be(307);
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
            
            var path = Day6.CalculateMinimumOrbitalTransfers(lines, "YOU", "SAN");

            path.Should().Be(expectedPath);
        }
    }
}