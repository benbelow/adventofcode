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

        [Test]
        public void Part1()
        {
            Day10.Part1().Should().Be(263);
        }

        [Test]
        public void Part2()
        {
            Day10.Part2().Should().BeGreaterThan(210);
            Day10.Part2().Should().BeGreaterThan(613);
            Day10.Part2().Should().BeLessThan(1320);
            Day10.Part2().Should().Be(1110);
        }

//        [TestCase(18, 4, 4)]
        [TestCase(1, 8, 1)]
//        [TestCase(36, 14, 3)]
//        [TestCase(35, 13, 3)]
        public void Part2Example(int n, int x, int y)
        {
            const string example = @"
.#....#####...#..
##...##.#####..##
##...#...#.#####.
..#.....#...###..
..#.#.....#....##";
            var nthVaporisedAsteroid = Day10.GetNthVaporisedAsteroid(example.Split('\n'), n);
            nthVaporisedAsteroid.X.Should().Be(x);
            nthVaporisedAsteroid.Y.Should().Be(y);
        }

//        [TestCase(1, 11, 12)]
        [TestCase(2, 12, 1)]
        [TestCase(3, 12, 2)]
        [TestCase(20, 16, 0)]
        [TestCase(50, 16, 9)]
        [TestCase(100, 10, 16)]
        [TestCase(199, 9, 6)]
        [TestCase(200, 8, 2)]
//        [TestCase(201, 10, 9)]
        public void Part2Example2(int n, int x, int y)
        {
            const string example = @".#..##.###...#######
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
###.##.####.##.#..##";
            var nthVaporisedAsteroid = Day10.GetNthVaporisedAsteroid(example.Split('\n'), n);
            nthVaporisedAsteroid.X.Should().Be(x);
            nthVaporisedAsteroid.Y.Should().Be(y);
        }
    }
}