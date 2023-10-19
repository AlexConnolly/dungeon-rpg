using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Extensions
{
    public static class SpriteBatchExtensions
    {
        private static Texture2D _blackSquare = null;

        private static Texture2D GetSquare(SpriteBatch batch)
        {
            if(SpriteBatchExtensions._blackSquare == null)
            {
                Texture2D texture = new Texture2D(batch.GraphicsDevice, 100, 100);

                Color[] colorData = new Color[100 * 100];

                for (int i = 0; i < colorData.Length; i++)
                {
                    colorData[i] = Color.White;
                }

                texture.SetData(colorData);

                SpriteBatchExtensions._blackSquare = texture;

            }

            return SpriteBatchExtensions._blackSquare;
        }

        public static void DrawSquare(this SpriteBatch spriteBatch, Rectangle rectangle)
        {
            spriteBatch.Draw(SpriteBatchExtensions.GetSquare(spriteBatch), rectangle, new Color(Color.Pink, 0.45f));
        }
    }
}
