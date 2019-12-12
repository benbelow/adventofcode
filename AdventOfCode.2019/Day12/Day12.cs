using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;
using AdventOfCode._2019.Common.Models;

namespace AdventOfCode._2019.Day12
{
    public class Day12
    {
        public static int Part1()
        {
            var lines = FileReader.ReadInputLines(12);
            return EnergyAfterXSteps(lines, 1000);
        }
        
        public static int EnergyAfterXSteps(IEnumerable<string> moonStrings, int steps)
        {
            var moons = moonStrings.Select(ParseMoon).ToList();
            for (var i = 0; i < steps; i++)
            {
                moons.ForEach(m => m.CalculateGravity(moons));
                moons.ForEach(m => m.ApplyGravity());
                moons.ForEach(m => m.ApplyVelocity());
            }

            return moons.Sum(m => m.Energy());
        }

        private static Moon ParseMoon(string arg)
        {
            var x = int.Parse(arg.Split("x=")[1].Split(",")[0]);
            var y = int.Parse(arg.Split("y=")[1].Split(",")[0]);
            var z = int.Parse(arg.Split("z=")[1].Split(">")[0]);
            return new Moon(x, y, z);
        }

        public class Moon
        {
            public int Id { get; set; }
            public Coordinate3D Position { get; set; }
            private Coordinate3D Velocity;

            private Coordinate3D StagingGravityChange;

            public Moon(int x, int y, int z)
            {
                Id = IdGenerator.NextId();
                Position = new Coordinate3D(x, y, z);
                Velocity = new Coordinate3D(0,0,0);
            }

            public Coordinate3D CalculateGravity(IEnumerable<Moon> moons)
            {
                var otherMoons = moons.Where(m => m.Id != Id);
                var changes = otherMoons.Select(ChangeBasedOnGravityOf);
                var gravityChange = changes.Aggregate((final, current) =>
                {
                    final.Add(current);
                    return final;
                });
                StagingGravityChange = gravityChange;
                return gravityChange;
            }

            public void ApplyGravity()
            {
                Velocity.Add(StagingGravityChange);
            }

            public void ApplyVelocity()
            {
                Position.Add(Velocity);
            }

            public int Energy()
            {
                return PotentialEnergy() * KineticEnergy();
            }

            private int KineticEnergy()
            {
                return Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
            }

            private int PotentialEnergy()
            {
                return Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
            }

            private Coordinate3D ChangeBasedOnGravityOf(Moon otherMoon)
            {
                int GetChange(Func<Moon, int> accessor)
                {
                    var diff = accessor(this) - accessor(otherMoon);
                    return diff switch
                    {
                        0 => 0,
                        _ when diff < 0 => 1,
                        _ when diff > 0 => -1,
                        _ => throw new Exception(),
                    };
                }

                var dx = GetChange(m => m.Position.X);
                var dy = GetChange(m => m.Position.Y);
                var dz = GetChange(m => m.Position.Z);

                return new Coordinate3D(dx, dy, dz);
            }
        }

        public static class IdGenerator
        {
            private static int id = 0;

            public static int NextId()
            {
                id++;
                return id;
            }
        }
    }
}