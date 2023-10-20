using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Extensions
{
    public static class PointExtensions
    {
        public static bool IsIn(this Point a, Rectangle b)
        {
            return a.X >= b.Left && a.X <= b.Right && a.Y >= b.Top && a.Y <= b.Bottom;
        }
    }
}
