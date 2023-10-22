using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Extensions
{
    public static class ColorExtensions
    {
        public static Color SetOpacity(this Color color, float opacity)
        {
            return new Color(color, opacity);
        }
    }
}
