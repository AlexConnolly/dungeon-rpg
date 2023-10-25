using LDG.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LDG.Sprite
{
    public class SpriteFrame
    {
        private readonly Texture2D texture;
        private readonly Rectangle? source;

        public SpriteFrame(Texture2D texture, Rectangle? source = null)
        {
            this.texture = texture;
            this.source = source;
        }

        public Vector2 Size
        {
            get
            {
                if(source == null)
                {
                    return new Vector2(this.texture.Width, this.texture.Height);
                } else
                {
                    return new Vector2(this.source.Value.Width, this.source.Value.Height);
                }
            }
        }

        public static List<SpriteFrame> GetFramesFromSheet(Texture2D texture, Vector2 resolution)
        {
            var result = new List<SpriteFrame>();

            for (int y = 0; y < texture.Height; y += (int)resolution.Y)
            {
                for (int x = 0; x < texture.Width; x += (int)resolution.X)
                {
                    result.Add(new SpriteFrame(texture, new Rectangle(x, y, (int)resolution.X, (int)resolution.Y)));
                }
            }

            return result;
        }

        public static List<List<SpriteFrame>> GetRowsFromSheet(Texture2D texture, Vector2 resolution, bool directionIsY = false)
        {
            var result = new List<List<SpriteFrame>>();

            if(directionIsY)
            {
                for (int x = 0; x < texture.Width; x += (int)resolution.X)
                {
                    var row = new List<SpriteFrame>();

                    for (int y = 0; y < texture.Height; y += (int)resolution.Y)
                    {
                        row.Add(new SpriteFrame(texture, new Rectangle(x, y, (int)resolution.X, (int)resolution.Y)));
                    }

                    result.Add(row);
                }
            } else
            {
                for (int y = 0; y < texture.Height; y += (int)resolution.Y)
                {
                    var row = new List<SpriteFrame>();

                    for (int x = 0; x < texture.Width; x += (int)resolution.X)
                    {
                        row.Add(new SpriteFrame(texture, new Rectangle(x, y, (int)resolution.X, (int)resolution.Y)));
                    }

                    result.Add(row);
                }
            }

            return result;
        }

        public void Draw(SpriteBatch batch, Vector2 position, Point? drawSize = null, float opacity = 1)
        {
            Rectangle destination = new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y);

            if(drawSize != null)
            {
                destination.Width = drawSize.Value.X;
                destination.Height = drawSize.Value.Y;
            }

            batch.Draw(this.texture, destination, this.source, Color.White.SetOpacity(opacity));
        }
    }
}
