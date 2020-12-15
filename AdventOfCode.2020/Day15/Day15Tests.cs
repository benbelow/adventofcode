using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day15
{
    [TestFixture]
    public class Day15Tests
    {
        [TestCase("1,3,2", 1)]
        [TestCase("2,1,3", 10)]
        [TestCase("1,2,3", 27)]
        [TestCase("2,3,1", 78)]
        [TestCase("3,2,1", 438)]
        [TestCase("3,1,2", 1836)]
        [TestCase("0,3,6", 436)]
        public void Part1_Test(string seed, long expected)
        {
            Day15.Part1(seed).Should().Be(expected);
        }

        [TestCase("0,3,6", 175594)]
        [TestCase("1,3,2", 2578)]
        [TestCase("2,1,3", 3544142)]
        [TestCase("1,2,3", 261214)]
        [TestCase("2,3,1", 6895259)]
        [TestCase("3,2,1", 18)]
        [TestCase("3,1,2", 362)]
        public void Part2_Test(string seed, long expected)
        {
            Day15.Part2(seed).Should().Be(expected);
        }

        [Test]
        public void Part1()
        {
            var answer = Day15.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(371);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day15.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(352);
        }
    }
}