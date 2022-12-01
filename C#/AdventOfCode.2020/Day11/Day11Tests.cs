using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day11
{
    [TestFixture]
    public class Day11Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day11.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(2453);
        }
        
        [Test]
        public void Part1Example()
        {
            var input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            var answer = Day11.Part1(input);
            answer.Should().NotBe(-1);
            answer.Should().Be(37);
        }
        
        [Test]
        public void Part2Example()
        {
            var input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            var answer = Day11.Part2(input);
            answer.Should().Be(26);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day11.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(2159);
        }
    }
}