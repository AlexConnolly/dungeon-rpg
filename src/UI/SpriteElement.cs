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
    public class SpriteElement : UIElement
    {
        public SpriteElement(Rectangle position) : base(position)
        {
            this.Position = position;
        }

        public SpriteFrame Frame { get; set; }

        public override Vector2 ContentDimensions()
        {
            return Position.Size.ToVector2();
        }

        public override void Draw(SpriteBatch spriteBatch, UIGroup group)
        {
            this.Frame.Draw(spriteBatch, this.GlobalPosition.ToVector2(), Color.White, this.Position.Size);
        }
    }
}
