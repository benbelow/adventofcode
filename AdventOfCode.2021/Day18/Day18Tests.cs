using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2021.Day18
{
    [TestFixture]
    public class Day18Tests
    {
        [TestCase("[9,1]", 29)]
        [TestCase("[[1,2],[[3,4],5]]", 143)]
        [TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
        [TestCase("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
        [TestCase("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
        [TestCase("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
        [TestCase("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
        public void SnailNumber_Magnitude(string rawNumber, int expected)
        {
            var snailNumber = new Day18.SnailNumber(rawNumber);
            snailNumber.Magnitude().Should().Be(expected);
        }
        
        [Test]
        public void Part1_Example()
        {
            var answer = Day18.Part1(true);
            answer.Should().Be(4140);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = Day18.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            var answer = Day18.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day18.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}