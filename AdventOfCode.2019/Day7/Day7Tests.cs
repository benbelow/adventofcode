using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day7
{
    [TestFixture]
    public class Day7Tests
    {
        [TestCase("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", 43210)]
        [TestCase("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", 54321)]
        [TestCase("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", 65210)]
        public void MaximumThrust(string program, int expectedThrust)
        {
            Day7.GetMaximumThrust(program).Should().Be(expectedThrust);
        }

        [Test]
        public void Part1()
        {
            Day7.Part1().Should().Be(95757);
        }
    }
}