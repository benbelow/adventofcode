using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2020.Day10
{
    [TestFixture]
    public class Day10Tests
    {
        [Test]
        public void Part1()
        {
            var answer = Day10.Part1();
            answer.Should().NotBe(2310);
            answer.Should().Be(2380);
        }
        
        [Test]
        public void Part2()
        {
            var answer = Day10.Part2();
            answer.Should().NotBe(1776893001870606336L);
            answer.Should().Be(0);
        }
        
        [Test]
        public void Part2_Example1()
        {
            var lines = @"16
10
15
5
1
11
7
19
6
12
4";
            
            var answer = Day10.Part2(lines.Split("\n").ToList());
            answer.Should().NotBe(1776893001870606336L);
            answer.Should().Be(8);
        }
        
        [Test]
        public void Part2_Example2()
        {
            var lines = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";
            
            var answer = Day10.Part2(lines.Split("\n").ToList());
            answer.Should().NotBe(1776893001870606336L);
            answer.Should().Be(19208);
        }
    }
}