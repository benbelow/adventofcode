using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day23
{
    public static class Day23
    {
        private const int Day = 23;

        public class Cup
        {
            public int Id { get; set; }
            public Cup Next { get; set; }
            public int RingSize { get; set; }

            public static Cup NewCup(IList<int> cupsSource)
            {
                var cups = cupsSource.Select(c => new Cup(c, cupsSource.Count)).ToList();

                for (int i = 0; i < cups.Count; i++)
                {
                    var cup = cups[i];
                    var nextCup = cups[i + 1 == cups.Count ? 0 : i + 1];
                    cup.Next = nextCup;
                }

                lookupCache = cups.ToDictionary(c => c.Id, c => c);
                
                return cups.First();
            }

            public Cup(int id, int ringSize)
            {
                Id = id;
                RingSize = ringSize;
            }
            
            public Cup(IList<int> cupsSource, Cup wrapCup = null)
            {
                Id = cupsSource.First();
                RingSize = wrapCup?.RingSize ?? cupsSource.Count;
                if (cupsSource.Count == 1)
                {
                    Next = wrapCup;
                }
                else
                {
                    wrapCup ??= this;
                    Next = new Cup(cupsSource.Skip(1).ToList(), wrapCup);
                }
            }

            public IEnumerable<Cup> AllCups()
            {
                yield return this;
                var cup = this;
                cup = cup.Next;
                while (cup.Id != Id)
                {
                    yield return cup;
                    cup = cup.Next;
                }
            }

            private static Dictionary<int, Cup> lookupCache;
            
            public Dictionary<int, Cup> CupLookup()
            {
                return lookupCache ??= AllCups().ToDictionary(c => c.Id, c => c);
            }

            public string CupString => AllCups().Select(c => c.Id).Aggregate("", (a, x) => a + x);

            public Cup LastCup()
            {
                return AllCups().Last();
            }

            /// <returns>Next target cup.</returns>
            public Cup TickCrabCups()
            {
                CupLookup();
                
                // The crab picks up the three cups that are immediately clockwise of the current cup.
                // They are removed from the circle; cup spacing is adjusted as necessary to maintain the circle.
                var removedCup = Next;
                Next = removedCup.Next.Next.Next;
                removedCup.Next.Next.Next = removedCup;

                // The crab selects a destination cup: the cup with a label equal to the current cup's label minus one.
                // If this would select one of the cups that was just picked up, the crab will keep subtracting one until it finds a cup that wasn't just picked up.
                // If at any point in this process the value goes below the lowest value on any cup's label, it wraps around to the highest value on any cup's label instead.
                var removedLabels = removedCup.AllCups().Select(c => c.Id).ToHashSet();
                var destinationCupLabel = Id - 1;
                destinationCupLabel = destinationCupLabel == 0 ? RingSize : destinationCupLabel;
                while (removedLabels.Contains(destinationCupLabel))
                {
                    destinationCupLabel--;
                    destinationCupLabel = destinationCupLabel == 0 ? RingSize : destinationCupLabel;
                }

                // The crab places the cups it just picked up so that they are immediately clockwise of the destination cup.
                // They keep the same order as when they were picked up.
                var map = CupLookup();
                var destinationCup = map[destinationCupLabel];
                removedCup.LastCup().Next = destinationCup.Next;
                destinationCup.Next = removedCup;

                // The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
                return Next;
            }

            public Cup RecenterOn(int newCenter)
            {
                return CupLookup()[newCenter];
            }
        }

        public static Cup PlayCrabCubs(List<int> cups, int numberOfRounds)
        {
            var activeCup = Cup.NewCup(cups);
            for (var i = 0; i < numberOfRounds; i++)
            {
                activeCup = activeCup.TickCrabCups();
            }

            return activeCup;
        }

        public static string Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var cups = lines.Single().Select(x => int.Parse(x.ToString())).ToList();

            var stringIncluding1 = PlayCrabCubs(cups, 100).RecenterOn(1).CupString;
            return stringIncluding1.Skip(1).CharsToString();
        }

        public static string Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var cups = lines.Single().Select(x => int.Parse(x.ToString())).ToList();
            
            var extra = Enumerable.Range(cups.Count + 1, 1_000_000 - cups.Count);
            cups = cups.Concat(extra).ToList();

            var finalCup = PlayCrabCubs(cups, 10_000_000).RecenterOn(1);
            return (1L * finalCup.Next.Id * finalCup.Next.Next.Id).ToString();
        }
    }
}