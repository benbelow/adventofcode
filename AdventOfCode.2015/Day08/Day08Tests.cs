using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day08
{
    [TestFixture]
    public class Day08Tests
    {
        [TestCase("", 0, 0)]
        [TestCase("\"\"", 0, 2)]
        [TestCase("\"abc\"", 3, 5)]
        [TestCase("\"aaa\\\"aaa\"", 7, 10)]
        [TestCase("\"\\x27\"", 1, 6)]
        public void Part1_Examples(string input, int expectedMemory, int expectedCode)
        {
            Day08.NumberOfCharactersInMemory(input).Should().Be(expectedMemory);
            Day08.NumberOfCharactersInCode(input).Should().Be(expectedCode);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day08.Part1();
            answer.Should().NotBe(4172);
            answer.Should().BeLessThan(4172);
            answer.Should().BeLessThan(1238);
            answer.Should().Be(1333);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day08.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}