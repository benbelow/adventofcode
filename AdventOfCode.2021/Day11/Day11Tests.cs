using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day11
{
    [TestFixture]
    public class Day11Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day11.Part1(true);
            answer.Should().Be(1656);
        }
        
        [Test]
        public void Part1_Example_Small()
        {
            var example = @"11111
19991
19191
19991
11111";
            
            var answer = Day11.Part1(true, example.Split(Environment.NewLine).ToList());
            answer.Should().Be(9);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day11.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day11.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day11.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}