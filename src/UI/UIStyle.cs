using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.UI
{
    public class FontConfig
    {
        public SpriteFont Font { get; set; }

        public FontShadowConfig? Shadow { get; set; }
    }

    public class FontShadowConfig
    {
        public Color Color { get; set; } = Color.Black;
        public Vector2 Offset { get; set; } = new Vector2(1, 1);
    }

    public class UIStyle
    {
        public Color BackgroundColor { get; set; }
        public Color BackgroundColorActive { get; set; }
        public Color BorderColor { get; set; }

        public Color BorderColorActive { get; set; }

        public FontConfig ButtonFont { get; set; }
        public FontConfig HeaderFont { get; set; }
        public FontConfig TextFont { get; set; }
    }
}
