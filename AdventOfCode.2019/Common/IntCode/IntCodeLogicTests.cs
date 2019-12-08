using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Common.IntCode
{
    [TestFixture]
    public class IntCodeLogicTests
    {
        [TestCase("1,9,10,3,2,3,11,0,99,30,40,50", 0, 3500)]
        [TestCase("1,0,0,0,99", 0, 2)]
        [TestCase("2,3,0,3,99", 3, 6)]
        [TestCase("2,4,4,5,99,0", 5, 9801)]
        [TestCase("1,1,1,4,99,5,6,0,99", 0, 30)]
        [TestCase("1,1,1,4,99,5,6,0,99", 4, 2)]
        public void Parse_And_RunIntCode(string intCode, int indexOfCheck, int expectedResult)
        {
            IntCodeLogic.ParseAndRunIntCode(intCode).ToEnumerable().Last().CurrentState.Skip(indexOfCheck).First().Should().Be(expectedResult);
        }

        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 9, 0)]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 7, 0)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 9, 0)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 7, 0)]
        public void EqualTo(string intCode, int input, int expectedOutput)
        {
            IntCodeLogic.ParseAndRunIntCode(intCode, getNextInput: () => input)
                .ToEnumerable()
                .Single(x => !x.IsComplete)
                .Output
                .Should().Be(expectedOutput);
        }

        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 8, 0)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 9, 0)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 7, 1)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 8, 0)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 9, 0)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 7, 1)]
        public void LessThan(string intCode, int input, int expectedOutput)
        {
            IntCodeLogic.ParseAndRunIntCode(intCode, getNextInput:() => input)
                .ToEnumerable()
                .Single(x => !x.IsComplete)
                .Output
                .Should().Be(expectedOutput);
        }

        // Example from https://adventofcode.com/2019/day/5#part2
        [TestCase(8, 1000)]
        [TestCase(7, 999)]
        [TestCase(9, 1001)]
        public void Comparators_Example(int input, int expectedOutput)
        {
            const string code =
                "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31, 1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104, 999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

            IntCodeLogic.ParseAndRunIntCode(code, getNextInput: () => input)
                .ToEnumerable()
                .Single(x => !x.IsComplete)
                .Output
                .Should().Be(expectedOutput);
        }
    }
}