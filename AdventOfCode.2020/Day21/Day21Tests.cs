using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day21
{
    [TestFixture]
    public class Day21Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day21.Part1();
            answer.Should().NotBe(-1);
            answer.Should().NotBe(192);
            answer.Should().NotBe(193);
            answer.Should().NotBe(2416);
            answer.Should().NotBe(2180);
            answer.Should().BeLessThan(2417);
            answer.Should().Be(2150);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day21.Part2();
            answer.Should().Be("vpzxk,bkgmcsx,qfzv,tjtgbf,rjdqt,hbnf,jspkl,hdcj");
        }
    }
}