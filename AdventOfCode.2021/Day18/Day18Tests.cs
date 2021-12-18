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

        [TestCase("[9,1]", "[9,1]")]
        [TestCase("[[1,2],[[3,4],5]]", "[[1,2],[[3,4],5]]")]
        [TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
        [TestCase("[[[[1,1],[2,2]],[3,3]],[4,4]]", "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
        [TestCase("[[[[3,0],[5,3]],[4,4]],[5,5]]", "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
        [TestCase("[[[[5,0],[7,4]],[5,5]],[6,6]]", "[[[[5,0],[7,4]],[5,5]],[6,6]]")]
        [TestCase("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
        public void SnailNumber_ToString(string rawNumber, string expected)
        {
            var snailNumber = new Day18.SnailNumber(rawNumber);
            snailNumber.ToString().Should().Be(expected);
        }

        [Test]
        public void SnailNumber_RegularNumbers()
        {
            var snailNumber = new Day18.SnailNumber("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");
            snailNumber.RegularNumbers().Should().BeEquivalentTo(new[] { 8, 7, 7, 7, 8, 6, 7, 7, 0, 7, 6, 6, 8, 7 });
        }

        [Test]
        public void SnailNumber_NestingLevel()
        {
            var snailNumber = new Day18.SnailNumber("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");
            snailNumber.MaxNestingLevel().Should().Be(4);
        }

        [TestCase("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
        [TestCase("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
        [TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
        [TestCase("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
        [TestCase("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
        public void Explode(string start, string expected)
        {
            var sn = new Day18.SnailNumber(start);
            sn.Explode().ToString().Should().Be(expected);
        }

        [TestCase("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
        [TestCase("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")]
        public void Split(string start, string expected)
        {
            var sn = new Day18.SnailNumber(start);
            sn.Split().ToString().Should().Be(expected);
        }

        [TestCase("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
        public void Reduce(string start, string expected)
        {
            var sn = new Day18.SnailNumber(start);
            sn.Reduce().ToString().Should().Be(expected);
        }

        [TestCase("[[[[4,3],4],4],[7,[[8,4],9]]]", "[1,1]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
        [TestCase("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]", "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
            "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
        [TestCase("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]",
            "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
            "[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]")]
        [TestCase("[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]",
            "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
            "[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]")]
        [TestCase("[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]",
            "[7,[5,[[3,8],[1,4]]]]",
            "[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]")]
        [TestCase("[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]",
            "[[2,[2,2]],[8,[8,1]]]",
            "[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]]")]
        [TestCase("[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]]",
            "[2,9]",
            "[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]")]
        [TestCase("[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]",
            "[1,[[[9,3],9],[[9,0],[0,7]]]]",
            "[[[[7,8],[6,7]],[[6,8],[0,8]]],[[[7,7],[5,0]],[[5,5],[5,6]]]]")]
        [TestCase("[[[[7,8],[6,7]],[[6,8],[0,8]]],[[[7,7],[5,0]],[[5,5],[5,6]]]]",
            "[[[5,[7,4]],7],1]",
            "[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]]")]
        [TestCase("[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]]",
            "[[[[4,2],2],6],[8,7]]",
            "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
        public void Add(string in1, string in2, string expected)
        {
            var sn = new Day18.SnailNumber(in1);
            var sn2 = new Day18.SnailNumber(in2);
            sn.Add(sn2).ToString().Should().Be(expected);
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
            answer.Should().Be(3993);
        }

        [Test]
        public void Part1()
        {
            var answer = Day18.Part1();
            answer.Should().NotBe(-1);
            answer.Should().BeLessThan(5620);
            answer.Should().BeLessThan(5619);
            answer.Should().Be(3884L);
        }

        [Test]
        public void Part2()
        {
            var answer = Day18.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(4595L);
        }
    }
}