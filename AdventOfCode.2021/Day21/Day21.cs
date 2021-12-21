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

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}