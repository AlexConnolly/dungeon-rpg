using LDG.Extensions;
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
    public class SquareElement : UIElement
    {
        public SquareElement(Rectangle position) : base(position)
        {
        }

        public Color Color { get; set; } = Color.White;
        public Color Border { get; set; } = Color.Black;
        public int BorderSize { get; set; } = 4;

        public Action OnClick { get; set; }

        public override void Draw(SpriteBatch spriteBatch, UIGroup group)
        {
            spriteBatch.DrawSquare(new Rectangle(this.GlobalPosition, this.Position.Size), this.Color, this.Border, this.BorderSize);
        }

        private bool pressedInside = false;

        public override void Update(TimeFrame time)
        {
            if (IsMouseOver())
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    pressedInside = true;

                    return;
                }

                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    if (pressedInside)
                    {
                        if (this.OnClick != null)
                            this.OnClick();
                    }
                }
            }

            pressedInside = false;
        }

        public override Vector2 ContentDimensions()
        {
            return this.Position.Size.ToVector2();
        }
    }
}
