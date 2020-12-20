using System;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day20
{
    [TestFixture]
    public class Day20Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day20.Part1();
            answer.Should().NotBe(-1);
            answer.Should().Be(83775126454273);
        }

        [Test]
        public void TransformTests()
        {
            const string testInput = @"Tile 999:
#..
#..
##.";
            var tile = new Tile(testInput.Split(Environment.NewLine));

            tile.ToString().Should().Be(@"
#..
#..
##.".Trim());

            var right90 = tile.Rotate(Rotation.Right90);
            right90.ToString().Should().Be(@"
###
#..
...".Trim());
            
            var right180 = tile.Rotate(Rotation.Right180);
            right180.ToString().Should().Be(@"
.##
..#
..#".Trim());
            
            var left90 = tile.Rotate(Rotation.Left90);
            left90.ToString().Should().Be(@"
...
..#
###".Trim());

            var horizontalFlip = tile.Flip(Flip2D.Horizontal);
            horizontalFlip.ToString().Should().Be(@"
..#
..#
.##".Trim());
            
            var verticalFlip = tile.Flip(Flip2D.Vertical);
            verticalFlip.ToString().Should().Be(@"
##.
#..
#..".Trim());
        }

        [Test]
        public void Part2()
        {
            var answer = Day20.Part2();
            answer.Should().NotBe(-2);
            answer.Should().Be(1993);
        }
    }
}