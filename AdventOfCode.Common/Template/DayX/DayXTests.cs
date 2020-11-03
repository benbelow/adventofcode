using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Common.Template.DayX
{
    [TestFixture]
    public class Day1Tests
    {
        [Test]
        public void Part1()
        {
            DayX.Part1().Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            DayX.Part2().Should().Be(0);
        }
    }
}