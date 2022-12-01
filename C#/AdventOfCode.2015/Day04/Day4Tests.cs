using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2015.Day04
{
    [TestFixture]
    public class Day04Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day04.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(346386L);
        }
        
        [Test]
        public void Part2()
        {
            // To complete this puzzle from scratch, use a seed of 0. This takes ~5s even with optimisations, so I don't want to run it with the full test suite. 
            // A pre-computed seed can be used if we already know that the answer is not found < this seed.
            var answer = Day04.Part2(9958000);
            answer.Should().NotBe(-2);
            answer.Should().Be(9958218L);
        }
    }
}