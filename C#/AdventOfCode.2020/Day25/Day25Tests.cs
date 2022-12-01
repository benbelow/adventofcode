using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day25
{
    [TestFixture]
    public class Day25Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day25.Part1();
            answer.Should().NotBe(-1);
            answer.Should().NotBe(2940436);
            answer.Should().NotBe(10705932L);
            answer.Should().Be(11328376L);
        }
    }
}