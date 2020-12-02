using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day2
{
    [TestFixture]
    public class Day2Tests
    {
        [TestCase("1-3 a: abcde", true)]
        [TestCase("1-3 b: cdefg", false)]
        [TestCase("2-9 c: ccccccccc", true)]
        [TestCase("2-9 c: cccccccccc", false)]
        public void Password_Valid(string entry, bool isValid)
        {
            var pwd = new Day2.Password(entry);
            pwd.IsValid1().Should().Be(isValid);
        }
        
        [Test]
        public void Part1()
        {
            Day2.Part1().Should().NotBe(907);
            Day2.Part1().Should().NotBe(809);
            Day2.Part1().Should().NotBe(316);
            Day2.Part1().Should().Be(447);
        }
        
        [Test]
        public void Part2()
        {
            Day2.Part2().Should().NotBe(143);
            Day2.Part2().Should().Be(249);
        }
        
        
        [TestCase("1-3 a: abcde", true)]
        [TestCase("1-3 b: cdefg", false)]
        [TestCase("2-9 c: ccccccccc", false)]
        public void Password_Valid2(string entry, bool isValid)
        {
            var pwd = new Day2.Password(entry);
            pwd.IsValid2().Should().Be(isValid);
        }
    }
}