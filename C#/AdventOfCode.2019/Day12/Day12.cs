using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(12);
            return StepsUntilRepeat(lines);
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

        public static long StepsUntilRepeat(IEnumerable<string> moonStrings)
        {
            var moons = moonStrings.Select(ParseMoon).ToList();

            var xAHistory = new Dictionary<Complex, bool>();
            var xBHistory = new Dictionary<Complex, bool>();
            var xCHistory = new Dictionary<Complex, bool>();
            var xDHistory = new Dictionary<Complex, bool>();

            var yAHistory = new Dictionary<Complex, bool>();
            var yBHistory = new Dictionary<Complex, bool>();
            var yCHistory = new Dictionary<Complex, bool>();
            var yDHistory = new Dictionary<Complex, bool>();

            var zAHistory = new Dictionary<Complex, bool>();
            var zBHistory = new Dictionary<Complex, bool>();
            var zCHistory = new Dictionary<Complex, bool>();
            var zDHistory = new Dictionary<Complex, bool>();

            xAHistory[new Complex(moons[0].Position.X, moons[0].Velocity.X)] = true;
            xBHistory[new Complex(moons[1].Position.X, moons[1].Velocity.X)] = true;
            xCHistory[new Complex(moons[2].Position.X, moons[2].Velocity.X)] = true;
            xDHistory[new Complex(moons[3].Position.X, moons[3].Velocity.X)] = true;

            yAHistory[new Complex(moons[0].Position.Y, moons[0].Velocity.Y)] = true;
            yBHistory[new Complex(moons[1].Position.Y, moons[1].Velocity.Y)] = true;
            yCHistory[new Complex(moons[2].Position.Y, moons[2].Velocity.Y)] = true;
            yDHistory[new Complex(moons[3].Position.Y, moons[3].Velocity.Y)] = true;

            zAHistory[new Complex(moons[0].Position.Z, moons[0].Velocity.Z)] = true;
            zBHistory[new Complex(moons[1].Position.Z, moons[1].Velocity.Z)] = true;
            zCHistory[new Complex(moons[2].Position.Z, moons[2].Velocity.Z)] = true;
            zDHistory[new Complex(moons[3].Position.Z, moons[3].Velocity.Z)] = true;

            moons.ForEach(m => m.CalculateGravity(moons));
            moons.ForEach(m => m.ApplyGravity());
            moons.ForEach(m => m.ApplyVelocity());

            long steps = 1;

            long? xPeriodicity = null;
            long? yPeriodicity = null;
            long? zPeriodicity = null;

            do
            {
                var xTuple = new Tuple<Complex, Complex, Complex, Complex>(
                    new Complex(moons[0].Position.X, moons[0].Velocity.X),
                    new Complex(moons[1].Position.X, moons[1].Velocity.X),
                    new Complex(moons[2].Position.X, moons[2].Velocity.X),
                    new Complex(moons[3].Position.X, moons[3].Velocity.X)
                );
                var yTuple = new Tuple<Complex, Complex, Complex, Complex>(
                    new Complex(moons[0].Position.Y, moons[0].Velocity.Y),
                    new Complex(moons[1].Position.Y, moons[1].Velocity.Y),
                    new Complex(moons[2].Position.Y, moons[2].Velocity.Y),
                    new Complex(moons[3].Position.Y, moons[3].Velocity.Y)
                );
                var zTuple = new Tuple<Complex, Complex, Complex, Complex>(
                    new Complex(moons[0].Position.Z, moons[0].Velocity.Z),
                    new Complex(moons[1].Position.Z, moons[1].Velocity.Z),
                    new Complex(moons[2].Position.Z, moons[2].Velocity.Z),
                    new Complex(moons[3].Position.Z, moons[3].Velocity.Z)
                );

                var xA = new Complex(moons[0].Position.X, moons[0].Velocity.X);
                var xB = new Complex(moons[1].Position.X, moons[1].Velocity.X);
                var xC = new Complex(moons[2].Position.X, moons[2].Velocity.X);
                var xD = new Complex(moons[3].Position.X, moons[3].Velocity.X);
                xAHistory[xA] = true;
                xAHistory[xB] = true;
                xAHistory[xC] = true;
                xAHistory[xD] = true;
                var zA = new Complex(moons[0].Position.Z, moons[0].Velocity.Z);
                var zB = new Complex(moons[1].Position.Z, moons[1].Velocity.Z);
                var zC = new Complex(moons[2].Position.Z, moons[2].Velocity.Z);
                var zD = new Complex(moons[3].Position.Z, moons[3].Velocity.Z);
                zAHistory[zA] = true;
                zAHistory[zB] = true;
                zAHistory[zC] = true;
                zAHistory[zD] = true;
                var yA = new Complex(moons[0].Position.Y, moons[0].Velocity.Y);
                var yB = new Complex(moons[1].Position.Y, moons[1].Velocity.Y);
                var yC = new Complex(moons[2].Position.Y, moons[2].Velocity.Y);
                var yD = new Complex(moons[3].Position.Y, moons[3].Velocity.Y);
                yAHistory[yA] = true;
                yAHistory[yB] = true;
                yAHistory[yC] = true;
                yAHistory[yD] = true;


                if (xAHistory.TryGetValue(xA, out var nxa)
                    && xBHistory.TryGetValue(xB, out var nxb)
                    && xCHistory.TryGetValue(xC, out var nxc)
                    && xDHistory.TryGetValue(xD, out var nxd)
                    && xPeriodicity == null)
                {
                    xPeriodicity = steps;
                }

                if (yAHistory.TryGetValue(yA, out var nya)
                    && yBHistory.TryGetValue(yB, out var nyb)
                    && yCHistory.TryGetValue(yC, out var nyc)
                    && yDHistory.TryGetValue(yD, out var nyd) && yPeriodicity == null)
                {
                    yPeriodicity = steps;
                }

                if (zAHistory.TryGetValue(zA, out var nza)
                    && zBHistory.TryGetValue(zB, out var nzb)
                    && zCHistory.TryGetValue(zC, out var nzc)
                    && zDHistory.TryGetValue(zD, out var nzd) && zPeriodicity == null)
                {
                    zPeriodicity = steps;
                }

                moons.ForEach(m => m.CalculateGravity(moons));
                moons.ForEach(m => m.ApplyGravity());
                moons.ForEach(m => m.ApplyVelocity());

                steps++;
            } while (xPeriodicity == null || yPeriodicity == null || zPeriodicity == null);

            return LCM(LCM(xPeriodicity.Value, yPeriodicity.Value), zPeriodicity.Value);
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
            public Coordinate3D Velocity;

            private Coordinate3D StagingGravityChange;

            public Moon(int x, int y, int z)
            {
                Id = IdGenerator.NextId();
                Position = new Coordinate3D(x, y, z);
                Velocity = new Coordinate3D(0, 0, 0);
            }

            public Moon(Moon moon)
            {
                Id = moon.Id;
                Position = new Coordinate3D(moon.Position.X, moon.Position.Y, moon.Position.Z);
                Velocity = new Coordinate3D(moon.Velocity.X, moon.Velocity.Y, moon.Velocity.Z);
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

        private static long GCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            // Pull out remainders.
            for (;;)
            {
                long remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            }

            ;
        }

        private static long LCM(long a, long b)
        {
            return a * b / GCD(a, b);
        }
    }
}