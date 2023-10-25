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
    public class SpriteRenderer : GameComponent
    {
        public SpriteRenderer()
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
            Frame.Draw(spriteBatch, LDG.Camera.WorldPositionToCameraPoint(drawPosition).ToVector2(), Color.White);

            base.Draw(spriteBatch);
        }
    }
}
