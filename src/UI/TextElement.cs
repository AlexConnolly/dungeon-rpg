using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.UI
{
    public class TextElement : UIElement
    {
        public int Size { get; set; } = 12;

        public required string Text { get; set; }

        public override Rectangle Position { get; set; }

        public required Color Color { get; set; }

        public override Vector2 ContentDimensions()
        {
            return UIManager.LargeFont.MeasureString(this.Text);
        }

        public override void Draw(Vector2 offset, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(UIManager.LargeFont, this.Text, new Vector2(offset.X + this.Position.X, offset.Y + this.Position.Y), this.Color);
        }
    }
}
