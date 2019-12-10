using System;
using AdventOfCode._2019.Common.Models;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day10
{
    [TestFixture]
    public class Day10Tests
    {
        [TestCase(@".#..#
.....
#####
....#
...##", 8)]
        [TestCase(@"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####", 33)]
        [TestCase(@"#.#...#.#.
.###....#.
.#....#...
##.#.#.#.#
....#.#.#.
.##..###.#
..#...##..
..##....##
......#...
.####.###.", 35)]
        [TestCase(@".#..#..###
####.###.#
....###.#.
..###.##.#
##.##.#.#.
....###..#
..#.#..#.#
#..#.#.###
.##...##.#
.....#.#..", 41)]
        [TestCase(@".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##", 210)]
        public void NumberOfAsteroidsFromBestLocation(string asteroidMap, int expectedAsteroidCount)
        {
            var lines = asteroidMap.Split('\n');
            Day10.NumberOfAsteroidsFromBestLocation(lines).Should().Be(expectedAsteroidCount);
        }

        [TestCase(1, 1, 2, 2, 3, 3, true)]
        [TestCase(0, 0, 0, 1, 0, 2, true)]
        [TestCase(0, 0, 0, 2, 0, 1, false)]
        [TestCase(0, 0, 1, 0, 2, 0, true)]
        [TestCase(0, 0, 2, 0, 1, 0, false)]
        [TestCase(0, 0, 1, 2, 2, 4, true)]
        [TestCase(1, 0, 1, 2, 4, 0, false)]
        [TestCase(1, 0, 0, 2, 2, 2, false)]
        [TestCase(1, 0, 2, 2, 3, 2, false)]
        public void BlockedByTests(int x1, int y1, int xBlock, int yBlock, int xTarget, int yTarget, bool isBlock)
        {
            var asteroid = new Day10.Asteroid(x1, y1);
            var target = new Coordinate(xTarget, yTarget);
            var block = new Coordinate(xBlock, yBlock);

            asteroid.IsBlockedBy(target, block).Should().Be(isBlock);
        }

        [Test]
        public void Part1()
        {
            Day10.Part1().Should().Be(1);
        }
    }
}