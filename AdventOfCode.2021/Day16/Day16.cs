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
            Literal,
            Operator
        }

        public enum OperatorType
        {
            Sum, Product, Minimum, Maximum, GreaterThan, LessThan, EqualTo
        }

        public enum LengthType
        {
            InBits,
            InPackets
        }

        public class Packet
        {
            public int RawBinarySize { get; set; }
            public int Version { get; set; }
            public int PacketTypeRaw { get; set; }

            public PacketType PacketType => PacketTypeRaw switch
            {
                4 => PacketType.Literal,
                _ => PacketType.Operator
            };

            public OperatorType? OperatorType => PacketTypeRaw switch {
                0 => Day16.OperatorType.Sum,
                1 => Day16.OperatorType.Product,
                2 => Day16.OperatorType.Minimum,
                3 => Day16.OperatorType.Maximum,
                5 => Day16.OperatorType.GreaterThan,
                6 => Day16.OperatorType.LessThan,
                7 => Day16.OperatorType.EqualTo,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            public LengthType LengthType { get; set; }
            
            public int Length { get; set; }

            public long LiteralValue { get; set; }
            public List<Packet> SubPackets { get; set; } = new List<Packet>();

            public Packet(string rawBinary, bool isOuterPacket = true)
            {
                Version = Convert.ToInt32(rawBinary[..3], 2);
                PacketTypeRaw = Convert.ToInt32(rawBinary.Substring(3, 3), 2);
                RawBinarySize = 6;
                if (PacketType == PacketType.Literal)
                {
                    var literalDigits = new List<char>();
                    foreach (var digit in rawBinary.Skip(6).Batch(5))
                    {
                        literalDigits.AddRange(digit.Skip(1).Select(x => x));
                        RawBinarySize += 5;
                        if (digit.First() == '0')
                        {
                            break;
                        }
                    }

                    LiteralValue = Convert.ToInt64(literalDigits.CharsToString(), 2);
                }
                else
                {
                    LengthType = rawBinary[6] == '0' ? LengthType.InBits : LengthType.InPackets;
                    RawBinarySize++;
                    if (LengthType == LengthType.InBits)
                    {
                        var rawLength = rawBinary.Substring(RawBinarySize, 15);
                        RawBinarySize += 15;
                        Length = Convert.ToInt32(rawLength, 2);

                        var targetLength = RawBinarySize + Length;

                        while (RawBinarySize < targetLength)
                        {
                            var subPacket = new Packet(rawBinary[RawBinarySize..], false);
                            RawBinarySize += subPacket.RawBinarySize;
                            SubPackets.Add(subPacket);
                        }
                    }
                    else
                    {
                        var rawLength = rawBinary.Substring(RawBinarySize, 11);
                        RawBinarySize += 11;
                        Length = Convert.ToInt32(rawLength, 2);

                        while (SubPackets.Count < Length)
                        {
                            var subPacket = new Packet(rawBinary[RawBinarySize..], false);
                            RawBinarySize += subPacket.RawBinarySize;
                            SubPackets.Add(subPacket);
                        }
                    }
                }

                if (isOuterPacket)
                {
                    // ending padding zeros to make multiple of 4
                    RawBinarySize += 4 - (RawBinarySize % 4);
                }
            }

            public int SumVersions()
            {
                return Version + SubPackets.Sum(p => p.SumVersions());
            }

            public long Value()
            {
                if (PacketType == PacketType.Literal)
                {
                    return LiteralValue;
                }

                var subValues = SubPackets.Select(s => s.Value()).ToList();

                return OperatorType switch
                {
                    Day16.OperatorType.Sum => subValues.Sum(),
                    Day16.OperatorType.Product => subValues.Aggregate(1L, (i, l) => i * l),
                    Day16.OperatorType.Minimum => subValues.Min(),
                    Day16.OperatorType.Maximum => subValues.Max(),
                    Day16.OperatorType.GreaterThan => subValues[0] > subValues[1] ? 1L : 0,
                    Day16.OperatorType.LessThan => subValues[0] < subValues[1] ? 1L : 0,
                    Day16.OperatorType.EqualTo => subValues[0] == subValues[1] ? 1L : 0,
                    null => throw new Exception("Should have been literal"),
                    _ => throw new ArgumentOutOfRangeException()
                };
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
            Console.WriteLine(packet.RawBinarySize);
            return packet.SumVersions();
        }

        public static long Part2(bool isExample = false, string input = null)
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
            Console.WriteLine(packet.RawBinarySize);
            return packet.Value();
        }
    }
}