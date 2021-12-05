using System;
using System.Collections.Generic;

namespace AdventOfCode.Common.Utils
{
    public class LineUtils
    {
        public static IEnumerable<(int, int)> Line(int x, int y, int x2, int y2)
        {
            var width = x2 - x;
            var height = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            dx1 = width switch
            {
                < 0 => -1,
                > 0 => 1,
                _ => dx1
            };
            dy1 = height switch
            {
                < 0 => -1,
                > 0 => 1,
                _ => dy1
            };
            dx2 = width switch
            {
                < 0 => -1,
                > 0 => 1,
                _ => dx2
            };
            var longest = Math.Abs(width);
            var shortest = Math.Abs(height);
            if (!(longest > shortest))
            {
                longest = Math.Abs(height);
                shortest = Math.Abs(width);
                dy2 = height switch
                {
                    < 0 => -1,
                    > 0 => 1,
                    _ => dy2
                };
                dx2 = 0;
            }

            var numerator = longest >> 1;
            for (var i = 0; i <= longest; i++)
            {
                yield return (x, y);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
    }
}