using System.Linq;
using AdventOfCode.Common.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Common.Tests
{
    [TestFixture]
    public class CollectionUtilsTests
    {
        [Test]
        public void Split_WorksAsExpected()
        {
            var inputs = new[] {"a", "b", "", "c", "d", "e", ""};

            var batches = inputs.Split("").ToList();

            batches.Count().Should().Be(3);
            batches.Select(b => b.Count()).Should().BeEquivalentTo(new[] {2, 3, 0});
        }
        
        [Test]
        public void DoubleSplit_WorksAsExpected()
        {
            var inputs = new[] {"a", "b", "", "c", "d", "e", "", "", "f", "g", "h", "", "i", ""};

            var batches = inputs.DoubleSplit("").ToList();

            batches.Count().Should().Be(2);
            batches[0].Count().Should().Be(2);
            batches[0].Select(x => x.Count()).Should().BeEquivalentTo(new [] {2, 3});
            batches[1].Count().Should().Be(2);
            batches[1].Select(x => x.Count()).Should().BeEquivalentTo(new [] {3, 1});
        }
        
        [Test]
        public void DoubleSplit_WorksAsExpected_2()
        {
            var inputs = new[] {"a", "b", "", "c", "d", "e", "", "", "f", "g", "h", "", "i", "j"};

            var batches = inputs.DoubleSplit("").ToList();

            batches.Count().Should().Be(2);
            batches[0].Count().Should().Be(2);
            batches[0].Select(x => x.Count()).Should().BeEquivalentTo(new [] {2, 3});
            batches[1].Count().Should().Be(2);
            batches[1].Select(x => x.Count()).Should().BeEquivalentTo(new [] {3, 2});
        }
    }
}