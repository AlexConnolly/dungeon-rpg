﻿using LDG.Components.Tile;
using LDG.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Collision
{
    public class TileLayerCollider : Collider
    {
        public TileLayerCollider()
        {
        }

        public TilemapLayer Layer { get; set; }

        public Point TileSize { get; set; }

        public override bool Intersects(Rectangle rectangle)
        {
            // Calculate potential bounding box of tiles that might intersect
            int startX = rectangle.X - TileSize.X;
            int startY = rectangle.Y - TileSize.Y;
            int endX = rectangle.Right;
            int endY = rectangle.Bottom;

            foreach (var tile in Layer.Tiles)
            {
                // Convert tile location to world coordinates
                int tileX = (tile.Location.X * TileSize.X) + (int)Transform.Position.X;
                int tileY = (tile.Location.Y * TileSize.Y) + (int)Transform.Position.Y;

                // Skip tiles outside the potential bounding box
                if (tileX > endX || tileX + TileSize.X < startX || tileY > endY || tileY + TileSize.Y < startY)
                    continue;

                Rectangle tileRect = new Rectangle(tileX, tileY, TileSize.X, TileSize.Y);

                if (rectangle.Intersects(tileRect))
                    return true;
            }

            return false;
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            foreach (var tile in Layer.Tiles)
            {
                int x = (int)(tile.Location.X * TileSize.X) + (int)Transform.Position.X;
                int y = (int)(tile.Location.Y * TileSize.Y) + (int)Transform.Position.Y;

                var tileRect = new Rectangle(x, y, TileSize.X, TileSize.Y);

                spriteBatch.DrawSquare(new Rectangle(LDG.Camera.WorldPositionToCameraPoint(new Vector2(tileRect.Location.X, tileRect.Location.Y)), tileRect.Size), Color.GreenYellow, null, 0);
            }
        }

        public override List<Rectangle> GetCollisionRectangles()
        {
            var returns = new List<Rectangle>();

            var transformPosition = Transform.Position;

            foreach (var tile in Layer.Tiles)
            {
                int x = (int)(tile.Location.X * TileSize.X) + (int)transformPosition.X;
                int y = (int)(tile.Location.Y * TileSize.Y) + (int)transformPosition.Y;
                
                returns.Add(new Rectangle(x, y, TileSize.X, TileSize.Y));
            }

            return returns;
        }
    }
}
