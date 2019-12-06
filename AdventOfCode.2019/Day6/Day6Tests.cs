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
            Day6.Part1().Should().Be(1);
        }
    }
}