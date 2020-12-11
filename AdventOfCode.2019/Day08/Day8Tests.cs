using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2019.Day08
{
    [TestFixture]
    public class Day08Tests
    {
        [TestCase("123456789012", 3, 2, 1)]
        public void CheckSum(string imageData, int width, int height, int expectedCheckSum)
        {
            Day08.CheckSum(imageData, width, height).Should().Be(expectedCheckSum);
        }

        [Test]
        public void Part1()
        {
            Day08.Part1().Should().Be(2480);
        }

        [Test]
        public void Render()
        {
            var image = new SpaceImage("0222112222120000".ToList().Select(i => int.Parse(i.ToString())).ToList(), 2, 2);
            var render = image.Render();
            render.PixelAt(0, 0).Should().Be(0);
            render.PixelAt(0, 1).Should().Be(1);
            render.PixelAt(1, 0).Should().Be(1);
            render.PixelAt(1, 1).Should().Be(0);
        }

        [Test]
        public void Part2()
        {
            Day08.Part2().Should().Be("ZYBLH");
        }
    }
}