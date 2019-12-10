namespace AdventOfCode._2019.Common.Models
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Coordinate;
            if (other == null)
            {
                return false;
            }

            return X == other.X && Y == other.Y;
        }

        protected bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !Equals(left, right);
        }
    }
}