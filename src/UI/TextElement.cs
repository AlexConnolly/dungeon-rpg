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
        public TextElement(Rectangle position) : base(position)
        {
        }

        public required string Text { get; set; }

        public required FontConfig Font { get; set; }

        public Color Color { get; set; } = Color.White;

        public override Vector2 ContentDimensions()
        {
            return Font.Font.MeasureString(this.Text);
        }

        public override void Draw(SpriteBatch spriteBatch, UIGroup group)
        {
            if (this.Font.Shadow != null)
            {
                spriteBatch.DrawString(Font.Font, this.Text, new Vector2(this.GlobalPosition.X, this.GlobalPosition.Y) + this.Font.Shadow.Offset, this.Font.Shadow.Color);
            }

            spriteBatch.DrawString(Font.Font, this.Text, new Vector2(this.GlobalPosition.X, this.GlobalPosition.Y), this.Color);
        }
    }
}
