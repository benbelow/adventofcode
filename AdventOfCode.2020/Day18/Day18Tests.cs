using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day18
{
    [TestFixture]
    public class Day18Tests
    {
        [TestCase("1 + 2 * 3 + 4 * 5 + 6",71)]
        [TestCase("2 * 3 + (4 * 5)",26)]
        [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)",437)]
        [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",12240)]
        [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2",13632)]
        [TestCase("((1 + 2) * 3) + 4",13)]
        public void Part1Examples(string input, long output)
        {
            Day18.EvaluateExpression(input).Should().Be(output);
        }
        
        [TestCase("1 + 2 * 3 + 4 * 5 + 6",231)]
        [TestCase("1 + (2 * 3) + (4 * (5 + 6))",51)]
        [TestCase("2 * 3 + (4 * 5)",46)]
        [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)",1445)]
        [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",669060)]
        [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2",23340)]
        [TestCase("2 * 3 + 4",14)]
        public void Part2Examples(string input, long output)
        {
            Day18.EvaluateExpression2(input).Should().Be(output);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day18.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(9535936849815L);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day18.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(472171581333710);
        }
    }
}