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

        public static Texture2D GetSquareTexture2D(SpriteBatch batch)
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

        public static void DrawSquare(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, Color? borderColor, int borderThickness = 0)
        {
            spriteBatch.Draw(SpriteBatchExtensions.GetSquareTexture2D(spriteBatch), rectangle, color);

            if(borderColor != null && borderThickness != 0)
            {
                // Draw top border
                DrawSquare(spriteBatch, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, borderThickness), borderColor.Value, null, 0);

                // Draw bottom border
                DrawSquare(spriteBatch, new Rectangle(rectangle.X, rectangle.Y + (rectangle.Height - borderThickness), rectangle.Width, borderThickness), borderColor.Value, null, 0);

                // Draw left border
                DrawSquare(spriteBatch, new Rectangle(rectangle.X, rectangle.Y, borderThickness, rectangle.Height), borderColor.Value, null, 0);

                // Draw left border
                DrawSquare(spriteBatch, new Rectangle(rectangle.X + (rectangle.Width - borderThickness), rectangle.Y, borderThickness, rectangle.Height), borderColor.Value, null, 0);
            }
        }
    }
}
