using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components
{
    internal class SpriteRenderer : GameComponent
    {
        public SpriteRenderer(GameObject gameObject) : base(gameObject)
        {
        }

        public Texture2D Texture { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Calculate where to draw 
            Vector2 currentPosition = this.Transform.Position;

            Vector2 sizing = new Vector2(this.Texture.Width, this.Texture.Height);

            Vector2 drawPosition = currentPosition - sizing;

            // Draw the sprite
            spriteBatch.Draw(this.Texture, new Rectangle((int)drawPosition.X, (int)drawPosition.Y, this.Texture.Width * 2, this.Texture.Height * 2), Color.White);

            base.Draw(spriteBatch);
        }
    }
}
