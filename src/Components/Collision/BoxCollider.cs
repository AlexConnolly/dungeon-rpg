using LDG.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LDG.Components.Collision
{
    public class BoxCollider : Collider
    {
        public BoxCollider()
        {
        }

        public Vector2 Bounds { get; set; }

        public Vector2 Offset { get; set; }

        public Rectangle WorldRectangle
        {
            get
            {
                // Account for offset
                return new Rectangle((int)this.Transform.Position.X - (int)(Bounds.X / 2) + (int)Offset.X, (int)this.Transform.Position.Y - (int)(Bounds.Y / 2) + (int)Offset.Y, (int)Bounds.X, (int)Bounds.Y);
            }
        }

        public override List<Rectangle> GetCollisionRectangles()
        {
            return new List<Rectangle>()
            {
                WorldRectangle
            };
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawSquare(new Rectangle(LDG.Camera.WorldPositionToCameraPoint(new Vector2(WorldRectangle.Location.X, WorldRectangle.Location.Y)), WorldRectangle.Size), Color.GreenYellow, null, 0);
        }

        public override bool Intersects(Rectangle source)
        {
            return !(source.Right < WorldRectangle.X ||
                     source.X > WorldRectangle.Right ||
                     source.Bottom < WorldRectangle.Y ||
                     source.Y > WorldRectangle.Bottom);
        }
    }
}
