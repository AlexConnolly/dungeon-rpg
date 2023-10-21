using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Extensions
{
    public static class DirectionExtensions
    {
        public static Direction FromTwoPoints(Vector2 a, Vector2 b)
        {
            // Check for vertical direction first (highest priority)
            Vector2 distance = a - b;

            if (Math.Abs(distance.X) >= Math.Abs(distance.Y))
            {
                // X priority
                if (b.X > a.X)
                    return Direction.Right;

                if (b.X < a.X)
                    return Direction.Left;
            } else
            {
                // Y priority
                if (b.Y > a.Y)
                    return Direction.Down;

                if (b.Y < a.Y)
                    return Direction.Up;

            }

            // If the points are the same, you can either return a default direction
            // or throw an exception, depending on your requirements

            return Direction.Right; // Default return value
        }
    }
}
