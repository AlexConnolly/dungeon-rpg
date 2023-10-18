using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Sprite
{
    internal class SpriteRenderer : GameComponent
    {
        public SpriteRenderer(GameObject gameObject) : base(gameObject)
        {
        }

        public SpriteFrame Frame { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Frame == null)
                return;

            // Calculate where to draw 
            Vector2 currentPosition = Transform.Position;

            Vector2 sizing = Frame.Size;

            Vector2 drawPosition = currentPosition - (sizing / 2);

            // Draw the sprite
            Frame.Draw(spriteBatch, new Vector2((int)drawPosition.X, (int)drawPosition.Y));

            base.Draw(spriteBatch);
        }
    }
}
