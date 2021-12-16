using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day16
{
    [TestFixture]
    public class Day16Tests
    {
        [TestCase("D2FE28", 6)]
        [TestCase("38006F45291200", 9)]
        [TestCase("8A004A801A8002F478", 16)]
        [TestCase("620080001611562C8802118E34", 12)]
        [TestCase("C0015000016115A2E0802F182340", 23)]
        [TestCase("A0016C880162017C3686B18A3D4780", 31)]
        public void Part1_Example(string input, int expected)
        {
            var answer = Day16.Part1(true, input);
            answer.Should().Be(expected);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day16.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day16.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day16.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}