using System;
using System.Linq;
using AdventOfCode._2019.Common;

namespace AdventOfCode._2019.Day4
{
    public static class Day4
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(4).ToList();
            var min = int.Parse(lines.First().Split("-").First());
            var max = int.Parse(lines.First().Split("-").Last());
            return NumberOfPasswordsInRange(min, max, false);
        }

        public static int Part2()
        {
            var lines = FileReader.ReadInputLines(4).ToList();
            var min = int.Parse(lines.First().Split("-").First());
            var max = int.Parse(lines.First().Split("-").Last());
            return NumberOfPasswordsInRange(min, max, true);
        }

        private static int NumberOfPasswordsInRange(int min, int max, bool includePart2Rule)
        {
            var count = 0;

            for (var i = min; i <= max; i++)
            {
                if (MeetsPasswordCriteria(i, includePart2Rule))
                {
                    count++;
                }
            }

            return count;
        }

        public static bool MeetsPasswordCriteria(int password, bool includePart2Rule = false)
        {
            if (password < 100000 || password > 999999)
            {
                throw new Exception("password must be 6 character");
            }

            return MeetsDoubleCriteria(password) && MeetsNonDecreasingCriteria(password) && (!includePart2Rule || MeetsGroupingCriteria(password));
        }

        private static bool MeetsGroupingCriteria(int password)
        {
            var stringPassword = password.ToString();
            return stringPassword[0] == stringPassword[1] && stringPassword[1] != stringPassword[2]
                   || stringPassword[1] == stringPassword[2] && stringPassword[2] != stringPassword[3] && stringPassword[0] != stringPassword[1]
                   || stringPassword[2] == stringPassword[3] && stringPassword[3] != stringPassword[4] && stringPassword[1] != stringPassword[2]
                   || stringPassword[3] == stringPassword[4] && stringPassword[4] != stringPassword[5] && stringPassword[2] != stringPassword[3]
                   || stringPassword[4] == stringPassword[5] && stringPassword[3] != stringPassword[4];
        }

        private static bool MeetsDoubleCriteria(int password)
        {
            var stringPassword = password.ToString();
            return stringPassword[0] == stringPassword[1]
                   || stringPassword[1] == stringPassword[2]
                   || stringPassword[2] == stringPassword[3]
                   || stringPassword[3] == stringPassword[4]
                   || stringPassword[4] == stringPassword[5];
        }

        private static bool MeetsNonDecreasingCriteria(int password)
        {
            return password.DigitAt(0) <= password.DigitAt(1)
                   && password.DigitAt(1) <= password.DigitAt(2)
                   && password.DigitAt(2) <= password.DigitAt(3)
                   && password.DigitAt(3) <= password.DigitAt(4)
                   && password.DigitAt(4) <= password.DigitAt(5);
        }

        private static int DigitAt(this int integer, int index)
        {
            if (integer.ToString().Length - 1 < index)
            {
                throw new Exception("index invalid for integer");
            }

            return int.Parse(integer.ToString()[index].ToString());
        }
    }
}