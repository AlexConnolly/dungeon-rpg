using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public static class Camera
    {
        public static Vector2 Position { get; set; }

        public static Point WorldPositionToCameraPoint(Vector2 position)
        {
            var relative = position - Position;

            // Take off screen size
            relative.X += (Screen.Resolution.X / 2);
            relative.Y += (Screen.Resolution.Y / 2);

            return new Point((int)relative.X, (int)relative.Y);
        }
    }
}
