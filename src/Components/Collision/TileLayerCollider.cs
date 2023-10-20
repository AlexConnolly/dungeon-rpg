using LDG.Components.Tile;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Collision
{
    public class TileLayerCollider : Collider
    {
        public TileLayerCollider(GameObject gameObject) : base(gameObject)
        {
        }

        public TilemapLayer Layer { get; set; }

        public Point TileSize { get; set; }

        public override bool Intersects(Rectangle rectangle)
        {
            foreach(var tile in Layer.Tiles)
            {
                var tileRect = new Rectangle(tile.Location.X * TileSize.X, tile.Location.Y * TileSize.Y, TileSize.X, TileSize.Y);

                if(rectangle.Intersects(tileRect))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
