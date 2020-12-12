using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day08
{
    [TestFixture]
    public class Day08Tests
    {
        [TestCase("", 0, 0, 2)]
        [TestCase("\"\"", 0, 2, 6)]
        [TestCase("\"abc\"", 3, 5, 9)]
        [TestCase("\"aaa\\\"aaa\"", 7, 10, 16)]
        [TestCase("\"\\x27\"", 1, 6, 11)]
        public void Part1_Examples(string input, int expectedMemory, int expectedCode, int expectedDoubleEncode)
        {
            Day08.NumberOfCharactersInMemory(input).Should().Be(expectedMemory);
            Day08.NumberOfCharactersInCode(input).Should().Be(expectedCode);
            Day08.NumberOfCharactersInDoubleEscaped(input).Should().Be(expectedDoubleEncode);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day08.Part1();
            answer.Should().NotBe(4172);
            answer.Should().BeLessThan(4172);
            answer.Should().BeGreaterThan(1238);
            answer.Should().Be(1333);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day08.Part2();
            answer.Should().BeGreaterThan(598);
            answer.Should().Be(2046);
        }
    }
}