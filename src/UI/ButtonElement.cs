using LDG.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.UI
{
    public class ButtonElement : UIElement
    {
        public override Rectangle Position { get; set; }

        public string Text { get; set; }

        public override Vector2 ContentDimensions()
        {
            return new Vector2(Position.Width, Position.Height);
        }

        public ButtonElement(UIGroup group, Rectangle position) : base(group, position)
        {
        }

        public override void Initialize()
        {
            Group.Text(new TextElement(this.Group, new Rectangle( Position.X, Position.Y, Position.Width, Position.Height))
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Text = this.Text,
                Font = UIManager.Style.ButtonFont
            });
        }

        public override void Draw(SpriteBatch spriteBatch, UIGroup group)
        {
            if(IsMouseOver())
            {
                spriteBatch.DrawSquare(new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, Position.Width, Position.Height), Color.White, UIManager.Style.BorderColor, 2);
            } else
            {
                spriteBatch.DrawSquare(new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, Position.Width, Position.Height), UIManager.Style.BackgroundColor, UIManager.Style.BorderColor, 2);
            }
        }
    }
}
