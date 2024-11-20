using LDG.Drawing;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Tile
{
    public class TilemapLayer
    {
        public List<TilemapItem> Tiles { get; set; } = new List<TilemapItem>();
    }

    public class TilemapItem
    {
        public Point Location { get; set; }
        public SpriteFrame Frame { get; set; }
    }

    public class Tilemap : GameComponent
    {
        public Tilemap()
        {
        }

        public Point TileSize { get; set; } = new Point(24, 24);

        public List<TilemapLayer> Layers { get; set; } = new List<TilemapLayer>();

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Go through each layer, draw the layer relative to the camera
            foreach(var layer in Layers)
            {
                foreach(var tile in layer.Tiles)
                {
                    // Determine it's position based on position * tile size then offset it with to camera
                    Point location = tile.Location * TileSize;

                    var cameraPosition = LDG.Camera.WorldPositionToCameraPoint(location.ToVector2());

                    tile.Frame.Draw(spriteBatch, cameraPosition.ToVector2(), Color.White, TileSize);
                }
            }
        }

        /// <summary>
        /// Takes a world position and returns the starting position of the tile that resides there. Zero if no tile.
        /// </summary>
        /// <returns></returns>
        public Vector2 WorldPositionToTileStart(Vector2 position)
        {
            float x = (position.X - this.Transform.Position.X) / TileSize.X;
            float y = (position.Y - this.Transform.Position.Y) / TileSize.Y;

            return TilePositionToWorldPosition(new Point((int)Math.Floor(x), (int)Math.Floor(y)));
        }

        public Point WorldPositionToTilePosition(Vector2 position)
        {
            float x = (position.X - this.Transform.Position.X) / TileSize.X;
            float y = (position.Y - this.Transform.Position.Y) / TileSize.Y;

            return new Point((int)Math.Floor(x), (int)Math.Floor(y));
        }

        public void SetTileAtLocation(int layer, Point location, SpriteFrame frame)
        {
            // Find if it and remove it
            this.Layers[layer].Tiles.RemoveAll(x => x.Location == location);

            // Place it
            if (frame == null)
                return;

            this.Layers[layer].Tiles.Add(new TilemapItem()
            {
                Location = location,
                Frame = frame
            });
        }

        public Vector2 TilePositionToWorldPosition(Point point)
        {
            int x = point.X * this.TileSize.X;
            int y = point.Y * this.TileSize.Y;

            return new Vector2()
            {
                X = x,
                Y = y
            };
        }
    }
}
