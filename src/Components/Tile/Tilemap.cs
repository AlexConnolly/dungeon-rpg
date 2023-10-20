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
        public Tilemap(GameObject gameObject) : base(gameObject)
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

                    tile.Frame.Draw(spriteBatch, cameraPosition.ToVector2(), TileSize);
                }
            }
        }
    }
}
