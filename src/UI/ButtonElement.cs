using LDG.Extensions;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public ButtonImage Image { get; set; }

        public Action OnClick { get; set; }

        public override Vector2 ContentDimensions()
        {
            return new Vector2(Position.Width, Position.Height);
        }

        public ButtonElement(Rectangle position) : base(position)
        {
        }

        public override void Initialize()
        {
            Group.Text(new TextElement(new Rectangle( Position.X, Position.Y, Position.Width, Position.Height))
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Text = this.Text,
                Font = UIManager.Style.ButtonFont
            });
        }

        private bool pressedInside = false;

        public override void Update(TimeFrame time)
        {
            if(IsMouseOver())
            {
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    pressedInside = true;

                    return;
                }

                if(Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    if(pressedInside)
                    {
                        if(this.OnClick != null)
                            this.OnClick();
                    }
                }
            }

            pressedInside = false;
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

            if(Image != null && Image.Image != null)
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
