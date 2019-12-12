using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day12
{
    [TestFixture]
    public class Day12Tests
    {
        [Test]
        public void Part1Example1()
        {
            const string moons = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            var energy = Day12.EnergyAfterXSteps(moons.Split("\n"), 10);

            energy.Should().Be(179);
        }
        
        [Test]
        public void Part2Example1()
        {
            const string moons = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            var steps = Day12.StepsUntilRepeat(moons.Split("\n"));

            steps.Should().Be(2772);
        }
        
        [Test]
        public void Part1Example2()
        {
            const string moons = @"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>";

            var energy = Day12.EnergyAfterXSteps(moons.Split("\n"), 100);

            energy.Should().Be(1940);
        }
        
        [Test]
        public void Part2Example2()
        {
            const string moons = @"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>";

            var energy = Day12.StepsUntilRepeat(moons.Split("\n"));

            energy.Should().Be(4686774924);
        }

        [Test]
        public void Part1()
        {
            Day12.Part1().Should().Be(9743);
        }
        
        [Test]
        public void Part2()
        {
            Day12.Part2().Should().Be(9743);
        }
    }
}