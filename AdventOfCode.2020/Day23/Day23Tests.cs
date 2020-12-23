using System;
using System.Linq;
using AdventOfCode.Common;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day23
{
    [TestFixture]
    public class Day23Tests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = Day23.Part1(true);
            answer.Should().Be("67384529");
        }

        [Test]
        public void Part1_SmallExample()
        {
            var cups = "389125467".Select(x => int.Parse(x.ToString())).ToList();

            var answer = Day23.PlayCrabCubs(cups, 10).CupString;

            // answer.Should().Be("583741926");
            
            // Order slightly off, but final puzzle is re-centered so it doesn't matter 
            answer.Should().Be("837419265");
        }

        [Test]
        public void Part1()
        {
            var answer = Day23.Part1();
            answer.Should().NotBe("527861349");
            answer.Should().Be("34952786");
        }

        [Test]
        public void Part2_Example()
        {
            var answer = Day23.Part2(true);
            answer.Should().Be("149245887792");
        }

        [Test]
        public void Part2()
        {
            var answer = Day23.Part2();
            answer.Should().Be("505334281774");
        }
    }
}