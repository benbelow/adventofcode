using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day3
{
    [TestFixture]
    public class Day3Tests
    {
        [TestCase("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void ClosestIntersection(string input1, string input2, int expectedDistance)
        {
            Day3.GetClosestIntersection(input1, input2).Should().Be(expectedDistance);
        }

        [Test]
        public void Part1()
        {
            Day3.Part1().Should().Be(896);
        }
        
        
        [TestCase("R8,U5,L5,D3", "U7,R6,D4,L4", 30)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 15)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 30)]
        public void FewestSteps(string input1, string input2, int expectedDistance)
        {
            Day3.GetFewestStepsToCrossOver(input1, input2).Should().Be(expectedDistance);
        }
    }
}