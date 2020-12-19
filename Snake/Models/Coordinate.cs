using System;

namespace Snake.Models
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Coordinate c = (Coordinate)obj;
                return (X == c.X) && (Y == c.Y);
            }
        }

        public override int GetHashCode()
        {
            return (X << 2) ^ Y;
        }
    }
}