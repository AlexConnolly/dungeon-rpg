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
        public TextElement(UIGroup group, Rectangle position) : base(group, position)
        {
        }

        public required string Text { get; set; }

        public required FontConfig Font { get; set; }

        public override Vector2 ContentDimensions()
        {
            return Font.Font.MeasureString(this.Text);
        }

        public override void Draw(SpriteBatch spriteBatch, UIGroup group)
        {
            spriteBatch.DrawString(Font.Font, this.Text, new Vector2(this.GlobalPosition.X, this.GlobalPosition.Y), this.Font.Color);

            if(this.Font.Shadow != null)
            {
                spriteBatch.DrawString(Font.Font, this.Text, new Vector2(this.GlobalPosition.X, this.GlobalPosition.Y) + this.Font.Shadow.Offset, this.Font.Shadow.Color);
            }
        }
    }
}
