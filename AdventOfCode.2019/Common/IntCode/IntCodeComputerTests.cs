using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Common.IntCode
{
    [TestFixture]
    public class IntCodeComputerTests
    {
        [TestCase("1,9,10,3,2,3,11,0,99,30,40,50", 0, 3500)]
        [TestCase("1,0,0,0,99", 0, 2)]
        [TestCase("2,3,0,3,99", 3, 6)]
        [TestCase("2,4,4,5,99,0", 5, 9801)]
        [TestCase("1,1,1,4,99,5,6,0,99", 0, 30)]
        [TestCase("1,1,1,4,99,5,6,0,99", 4, 2)]
        public void Parse_And_RunIntCode(string intCode, int indexOfCheck, int expectedResult)
        {
            IntCodeComputer.ParseAndRunIntCode(intCode).FinalState.Skip(indexOfCheck).First().Should().Be(expectedResult);
        }
    }
}