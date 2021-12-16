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
        
        [TestCase("C200B40A82", 3)]
        [TestCase("04005AC33890", 54)]
        [TestCase("880086C3E88112", 7)]
        [TestCase("CE00C43D881120", 9)]
        [TestCase("D8005AC2A8F0", 1)]
        [TestCase("F600BC2D8F", 0)]
        [TestCase("9C005AC2F8F0", 0)]
        [TestCase("9C0141080250320F1802104A08", 1)]
        public void Part2_Example(string input, long expected)
        {
            var answer = Day16.Part2(true, input);
            answer.Should().Be(expected);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day16.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(945);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day16.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(10637009915279L);
        }
    }
}