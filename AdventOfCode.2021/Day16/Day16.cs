using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;
using MoreLinq;

namespace AdventOfCode._2021.Day16
{
    public static class Day16
    {
        private const int Day = 16;

        public enum PacketType
        {
            Literal, Operator
        }
        
        public class Packet
        {
            public int Version { get; set; }
            public int Type { get; set; }

            public PacketType PacketType => Type switch
            {
                4 => PacketType.Literal,
                _ => PacketType.Operator
            };
            
            public int LiteralValue { get; set; }
            public List<Packet> SubPackets { get; set; } = new List<Packet>();

            public Packet(string rawBinary)
            {
                Version = Convert.ToInt32(rawBinary[..3], 2);
                Type = Convert.ToInt32(rawBinary.Substring(3,3), 2);
                if (PacketType == PacketType.Literal)
                {
                    var literalDigits = new List<char>();
                    foreach (var digit in rawBinary.Skip(6).Batch(5))
                    {
                        literalDigits.AddRange(digit.Skip(1).Select(x => x));
                        if (digit.First() == '0')
                        {
                            break;
                        } 
                    }

                    LiteralValue = Convert.ToInt32(literalDigits.CharsToString(), 2);
                }
            }

            public int SumVersions()
            {
                return Version + SubPackets.Sum(p => p.SumVersions());
            }
        }
        
        public static long Part1(bool isExample = false, string input = null)
        {
            var line = input ?? FileReader.ReadInputLines(Day, isExample).ToList().Single();
            var binaryString = string.Join(string.Empty,
                line.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
            
            Console.WriteLine(binaryString);
            var packet = new Packet(binaryString);
            Console.WriteLine(packet.LiteralValue);
            return packet.SumVersions();
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}