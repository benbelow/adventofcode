using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day14
{
    [TestFixture]
    public class Day14Tests
    {
        [Test]
        public void Part1Example()
        {
            var input = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";
            
            Day14.Part1(input).Should().Be(165);

            var input2 = @"mask = 01101X001X111X010X0000X1001X010XX0X0
mem[4841] = 3942";

            Day14.Part1(input2).Should().Be(28111278914);
        }
        
        [Test]
        public void Part2Example()
        {
            var input = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";
            
            Day14.Part2(input).Should().Be(208);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day14.Part1();
            answer.Should().NotBe(11934551314989L);
            answer.Should().BeLessThan(11934551314989L);
            answer.Should().Be(11926135976176L);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day14.Part2();
            answer.Should().BeGreaterThan(336692921234L);
            answer.Should().Be(          4330547254348L);
        }
    }
}