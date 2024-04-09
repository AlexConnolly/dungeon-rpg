using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Drawing
{
    public class OccludedSpriteBatch
    {
        private readonly SpriteBatch _spriteBatch;

        public OccludedSpriteBatch(SpriteBatch spriteBatch)
        {
            this._spriteBatch = spriteBatch;
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            this._spriteBatch.Draw()
        }
    }
}
