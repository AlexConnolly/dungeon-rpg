using LDG.Extensions;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.UI
{
    public class ButtonImage
    {
        public required SpriteFrame Image { get; set; }
        public required Vector2 Size { get; set; }
    }

    public class ButtonElement : UIElement
    {
        public string Text { get; set; }

        public bool ForceHover { get; set; } = false;

        public ButtonImage Image { get; set; }

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
            if(IsMouseOver() || ForceHover)
            {
                spriteBatch.DrawSquare(new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, Position.Width, Position.Height), Color.White, UIManager.Style.BorderColor, 2);
            } else
            {
                spriteBatch.DrawSquare(new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, Position.Width, Position.Height), UIManager.Style.BackgroundColor, UIManager.Style.BorderColor, 2);
            }

            if(Image != null)
            {
                Vector2 drawPosition = GlobalPosition.ToVector2();

                drawPosition.X += (Position.Width / 2);
                drawPosition.Y += (Position.Height / 2);

                drawPosition.X -= (Image.Size.X / 2);
                drawPosition.Y -= (Image.Size.Y / 2);

                Image.Image.Draw(spriteBatch, drawPosition, Image.Size.ToPoint());
            }
        }
    }
}
