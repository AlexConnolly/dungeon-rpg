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
    /// <summary>
    /// Add a trigger 
    /// </summary>
    public class BoxTrigger : GameComponent
    {
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Determines whether another rectangle in the world intersects with this
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public void CheckTrigger(Rectangle rectangle, GameObject sender)
        {
            if(IsTriggering(rectangle))
            {
                if(!IntersectingObjects.Contains(sender))
                    IntersectingObjects.Add(sender);
            }
        }

        private bool IsTriggering(Rectangle source)
        {
            return !(source.Right < WorldRectangle.X ||
         source.X > WorldRectangle.Right ||
         source.Bottom < WorldRectangle.Y ||
         source.Y > WorldRectangle.Bottom);
        }

        private Rectangle WorldRectangle
        {
            get; set;
        }

        public override void Update(TimeFrame time)
        {
            // Calculate the trigger position in the world
            this.WorldRectangle = new Rectangle((int)this.Transform.Position.X - (int)(Bounds.Width / 2) + (Bounds.X), (int)this.Transform.Position.Y - (int)(Bounds.Height / 2) + Bounds.Y, (int)Bounds.Width, (int)Bounds.Height);

            // Check whether intersecting objects were still intersecting
            var toRemove = this.IntersectingObjects.Where(x => !this.IsTriggering(x.GetComponent<BoxCollider>().WorldRectangle)).ToList();

            toRemove.ForEach(x => this.IntersectingObjects.Remove(x));
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawSquare(new Rectangle(LDG.Camera.WorldPositionToCameraPoint(new Vector2(WorldRectangle.Location.X, WorldRectangle.Location.Y)), WorldRectangle.Size), Color.Pink, null, 0);
        }

        public List<GameObject> IntersectingObjects
        {
            get; private set;
        } = new List<GameObject>();
    }
}
