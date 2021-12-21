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
            // key = (score, pos). val = number of winning options
            var cache = new Dictionary<(long, long), long>();
            
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
            
            for (long score = 20; score >= 0; score--)
            {
                for (long pos = 0; pos < 10; pos++)
                {
                    cache[(score, pos)] = 0;
                    foreach (var rollPair in distribution)
                    {
                        var roll = rollPair.Key;
                        var times = rollPair.Value;
                        
                        // not sure about this base case!
                        if (score >= 21)
                        {
                            cache[(score, pos)] += times;
                        }
                        else
                        {
                            var (nextScore, nextPos) = Turn(score, pos, roll);
                            if (nextScore >= 21)
                            {
                                cache[(score, pos)] += times;
                            }
                            else
                            {
                                cache[(score, pos)] += cache[(nextScore, nextPos)] * times;
                            }
                        }
                    }
                }
            }
            
            return Math.Max(cache[(0, p1Start - 1L)], cache[(0, p2Start - 1L)]);
        }
    }
}