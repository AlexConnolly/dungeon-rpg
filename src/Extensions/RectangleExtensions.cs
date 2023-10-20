using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Extensions
{
    public static class RectangleExtensions
    {
        public static bool Intersects(this Rectangle a, Rectangle b)
        {
            return !(a.X > b.X + b.Width ||
a.X + a.Width < b.X ||
a.Y > b.Y + b.Height ||
a.Y + a.Height < b.Y);
        }
    }
}
