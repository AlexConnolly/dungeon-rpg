using LDG.Components.Collision;
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

        public bool HasCollision { get; set; } = false;
    }

    public class TilemapItem
    {
        public Point Location { get; set; }
        public SpriteFrame Frame { get; set; }
    }

    public class TilemapConfig
    {
        public class TilemapConfigLayer
        {
            public List<List<string>> Tiles { get; set; }
            public bool IsCollision { get; set; }
            public Spritesheet Sheet { get; set; }
        }

        public List<TilemapConfigLayer> Layers { get; set; }
    }

    public class Tilemap : GameComponent
    {
        public Tilemap()
        {
        }

        public Point TileSize { get; set; } = new Point(24, 24);

        public List<TilemapLayer> Layers { get; } = new List<TilemapLayer>();

        public TilemapLayer AddLayer(TilemapLayer layer)
        {
            Layers.Add(layer);

            if(layer.HasCollision)
            {
                var collision = GameObject.AddComponent<TileLayerCollider>();

                collision.TileSize = new Point(24, 24);

                collision.Layer = layer;
            }

            return layer;
        }

        public void LoadFromConfig(TilemapConfig config)
        {
            foreach(var layer in config.Layers)
            {
                var tileLayer = new TilemapLayer()
                {
                    Tiles = new List<TilemapItem>(),
                    HasCollision = layer.IsCollision
                };

                for(int y = 0; y < layer.Tiles.Count; y++)
                {
                    var row = layer.Tiles[y];

                    for(int x = 0; x < row.Count; x++)
                    {
                        string tileKey = row[x];

                        if(!string.IsNullOrEmpty(tileKey))
                        {
                            var point = new Point(x, y);

                            tileLayer.Tiles.Add(new TilemapItem()
                            {
                                Frame = layer.Sheet.GetByKey(tileKey),
                                Location = point
                            });
                        }
                    }
                }

                AddLayer(tileLayer);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Go through each layer, draw the layer relative to the camera
            foreach(var layer in Layers)
            {
                foreach(var tile in layer.Tiles)
                {
                    // Determine it's position based on position * tile size then offset it with to camera
                    float xPosition = (tile.Location.X * TileSize.X) + Transform.Position.X;
                    float yPosition = (tile.Location.Y * TileSize.Y) + Transform.Position.Y;

                    Vector2 location = new Vector2(xPosition, yPosition);

                    var cameraPosition = LDG.Camera.WorldPositionToCameraPoint(location);

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
