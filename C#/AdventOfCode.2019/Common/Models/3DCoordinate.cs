namespace AdventOfCode._2019.Common.Models
{
    public class Coordinate3D
    {
        public Coordinate3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public void Add(Coordinate3D other)
        {
            X += other.X;
            Y += other.Y;
            Z += other.Z;
        }
    }
}