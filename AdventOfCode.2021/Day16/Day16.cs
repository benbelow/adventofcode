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

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}