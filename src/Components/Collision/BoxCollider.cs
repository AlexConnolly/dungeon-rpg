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
    internal class BoxCollider : Collider
    {
        public BoxCollider(GameObject gameObject) : base(gameObject)
        {
        }

        public Vector2 Bounds { get; set; }

        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle((int)this.Transform.Position.X - (int)(Bounds.X / 2), (int)this.Transform.Position.Y - (int)(Bounds.Y / 2), (int)Bounds.X, (int)Bounds.Y);
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
            return !(source.X > WorldRectangle.X + WorldRectangle.Width ||
                     source.X + source.Width < WorldRectangle.X ||
                     source.Y > WorldRectangle.Y + WorldRectangle.Height ||
                     source.Y + source.Height < WorldRectangle.Y);
        }
    }
}
