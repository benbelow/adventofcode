using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day21
{
    public static class Day21
    {
        private const int Day = 21;

        public static IEnumerable<long> RollD100()
        {
            var v = 1;
            while (true)
            {
                yield return v;
                v++;
            }
        }

        public static long Part1(long p1Start, long p2Start)
        {
            // consider 0-9 and add 1 for score to use modulo

            var p1Pos = p1Start - 1;
            var p2Pos = p2Start - 1;

            var p1Score = 0L;
            var p2Score = 0L;

            var rolls = 0L;

            using var dieResults = RollD100().GetEnumerator();

            var p1Turn = true;

            void BankTurn(long remainder)
            {
                if (p1Turn)
                {
                    p1Pos += remainder;
                    p1Pos %= 10;
                    p1Score += p1Pos + 1L;
                }
                else
                {
                    p2Pos += remainder;
                    p2Pos %= 10;
                    p2Score += p2Pos + 1L;
                }
            }

            while (p1Score < 1000 && p2Score < 1000)
            {
                dieResults.MoveNext();
                var roll1 = dieResults.Current;
                dieResults.MoveNext();
                var roll2 = dieResults.Current;
                dieResults.MoveNext();
                var roll3 = dieResults.Current;
                rolls += 3;

                var sum = roll1 + roll2 + roll3;
                var remainder = sum % 10;

                BankTurn(remainder);
                p1Turn = !p1Turn;
            }

            return Math.Min(p1Score, p2Score) * rolls;
        }

        public static long Part2(long p1Start, long p2Start)
        {
            // key = p1 (score, pos), p2 (score, pos) => number of winning options for p1 
            var cache = new Dictionary<((long, long), (long, long)), long>();

            // possible rolls:
            // 111 = 3
            // 112 / 121 / 211 = 4
            // 122 / 212 / 221 / 113 / 131 / 311= 5
            // 222 / 123 / 132 / 321 / 312 / 213 / 231 = 6
            // 133 / 313 / 331 / 223 / 232 / 322 = 7
            // 332 / 323 / 233 = 8
            // 333 = 9

            var distribution = new Dictionary<long, long>
            {
                { 3, 1 },
                { 4, 3 },
                { 5, 6 },
                { 6, 7 },
                { 7, 6 },
                { 8, 3 },
                { 9, 1 },
            };

            // returns (score, pos)
            (long, long) Turn(long score, long pos, long roll)
            {
                pos += roll;
                pos %= 10;
                score += pos + 1L;
                return (score, pos);
            }

            for (long p1Score = 20; p1Score >= 0; p1Score--)
            {
                for (long p2Score = 20; p2Score >= 0; p2Score--)
                {
                    for (long p1Pos = 0; p1Pos < 10; p1Pos++)
                    {
                        for (long p2Pos = 0; p2Pos < 10; p2Pos++)
                        {
                            var p1 = (p1Score, p1Pos);
                            var p2 = (p2Score, p2Pos);
                            cache[(p1, p2)] = 0;
                            foreach (var p1RollKvp in distribution)
                            {
                                var roll1 = p1RollKvp.Key;
                                var times1 = p1RollKvp.Value;
                                var (nextScore1, nextPos1) = Turn(p1Score, p1Pos, roll1);
                                if (nextScore1 >= 21)
                                {
                                    // if p1 wins this roll, p2 never splits the universe again
                                    cache[(p1, p2)] += times1;
                                    continue;
                                }

                                // if p2 rolls the dice...
                                foreach (var p2RollKvp in distribution)
                                {
                                    var roll2 = p2RollKvp.Key;
                                    var times2 = p2RollKvp.Value;

                                    var totalTimes = times1 * times2;

                                    var (nextScore2, nextPos2) = Turn(p2Score, p2Pos, roll2);
                                    if (nextScore2 < 21)
                                    {
                                        cache[(p1, p2)] += cache[((nextScore1, nextPos1), (nextScore2, nextPos2))] * totalTimes;
                                    }
                                    else
                                    {
                                        // p2 has won, do not give p1 any more wins in this timeline.
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var total = 444356092776315 + 341960390180808;
            Console.WriteLine($"Expected total: {total / 1000000000}B");

            var p1Final = cache[((0, p1Start - 1), (0, p2Start - 1))];
            var p2Final = total - p1Final;
            Console.WriteLine($"My total: {(p1Final + p2Final) / 1000000000}B");
            return Math.Max(p1Final, p2Final);
        }
    }
}