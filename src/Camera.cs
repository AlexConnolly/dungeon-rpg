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

            // Adjust based on screen size
            relative.X += (Screen.Resolution.X / 2);
            relative.Y += (Screen.Resolution.Y / 2);

            return new Point((int)relative.X, (int)relative.Y);
        }

        public static Vector2 CameraPointToWorldPosition(Point point)
        {
            // Reverse screen adjustment
            var relative = new Vector2(point.X - (Screen.Resolution.X / 2),
                                       point.Y - (Screen.Resolution.Y / 2));

            // Add camera position to get world position
            return relative + Position;
        }
    }
}
