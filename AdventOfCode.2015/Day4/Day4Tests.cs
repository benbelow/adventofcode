using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day4
{
    [TestFixture]
    public class Day4Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day4.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(346386L);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day4.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(9958218L);
        }
    }
}