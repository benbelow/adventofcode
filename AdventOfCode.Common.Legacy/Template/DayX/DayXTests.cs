using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Common.Template.DayX
{
    [TestFixture]
    public class DayXTests
    {
        [Test]
        public void Part1_Example()
        {
            var answer = DayX.Part1(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part2_Example()
        {
            var answer = DayX.Part2(true);
            answer.Should().Be(-1);
        }
        
        [Test]
        public void Part1()
        {
            var answer = DayX.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2()
        {
            var answer = DayX.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(0);
        }
    }
}