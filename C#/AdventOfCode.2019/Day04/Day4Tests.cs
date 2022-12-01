using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day04
{
    [TestFixture]
    public class Day04Tests
    {
        [TestCase(111111, true)]
        [TestCase(223450, false)]
        [TestCase(123789, false)]
        public void MeetsCriteria(int password, bool shouldMatch)
        {
            Day04.MeetsPasswordCriteria(password).Should().Be(shouldMatch);
        }
        
        
        [TestCase(112233, true)]
        [TestCase(123444, false)]
        [TestCase(111122, true)]
        public void MeetsUpdatedCriteria(int password, bool shouldMatch)
        {
            Day04.MeetsPasswordCriteria(password, true).Should().Be(shouldMatch);
        }

        [Test]
        public void Part1()
        {
            Day04.Part1().Should().Be(475);
        }

        [Test]
        public void Part2()
        {
            Day04.Part2().Should().Be(297);
        }
    }
}