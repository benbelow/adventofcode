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

            var answer = Day23.CupsToString(Day23.PlayCrabCubs(cups, 0, 10));

            answer.Should().Be("583741926");
        }

        [Test]
        public void Part2_Example()
        {
            var answer = Day23.Part2(true);
            answer.Should().Be("149245887792");
        }

        [Test]
        public void Part1()
        {
            var answer = Day23.Part1();
            answer.Should().NotBe("527861349");
            answer.Should().Be("34952786");
        }

        [Test]
        public void Part2()
        {
            var lines = FileReader.ReadInputLines(23).ToList();
            var cups = lines.Single().Select(x => int.Parse(x.ToString())).ToList();

            var someExtra = Enumerable.Range(10, 20);
            Console.WriteLine(Day23.CupsToString(
                Day23.PlayCrabCubs(cups.Concat(someExtra).ToList(), 0, 10))
            );

            var x = "10";
            var y = "1000000 - 1";
            
            var knownStates = new[]
            {
                "0: 253149867{x...y}",
                "1: 249867{x...y}531",
                "2: 247{x...y}539861",
                "3: 247{x+3...y}53986{x...x+2}1",
                "4: 247{x+3}{x+7...y}53986{x...x+2}{x+4...x+6}1",
                "5: 247{x+3}{x+7}{x+11...y}53986{x...x+2}{x+4...x+6}{x+8...x+10}1",
                ".... ~250,000 attempts",
                "249,999: 247{x+3}{x+7}{x+11}[...]{x+999988}{?..y}53986{x...x+2}{x+4...x+6}{x+8...x+10}[...]1",
                "250,000: 1{x+3}{x+7}{x+11}[...]{x+999988}{?..y}24753986{x...x+2}{x+4...x+6}{x+8...x+10}[...]",
                "250,000: 1{x+15}{x+19}{x+23}[...]{x+999988}{?..y}24753986{x...x+2}{x+3}{x+7}{x+11}{x+4...x+6}{x+8...x+10}[...]",
                "500,000: Catches back up to 1-10?" 
            };

            var answer = Day23.Part2();
        }
    }
}