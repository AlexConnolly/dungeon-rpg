using LDG.Components.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components
{
    public class Transform : GameComponent
    {
        public Transform()
        {
        }

        /// <summary>
        /// Careful with setting this - it will not trigger triggers or colliders
        /// </summary>
        public Vector2 Position { get; set; }

        private bool CanMoveToPosition(Vector2 position)
        {
            if (!this.GameObject.TryGetComponent<BoxCollider>(out BoxCollider collider))
                return true;

            float halfWidth = collider.Bounds.X / 2;
            float halfHeight = collider.Bounds.Y / 2;

            Rectangle targetRect = new Rectangle((int)(position.X - halfWidth), (int)(position.Y - halfHeight), (int)collider.Bounds.X, (int)collider.Bounds.Y);

            bool result = true;

            this.GameObject.TryGetComponent<BoxTrigger>(out BoxTrigger myTrigger);

            foreach (var gameObject in this.GameObject.Scene.GameObjects)
            {
                if (gameObject == this.GameObject)
                    continue;

                Collider otherCollider = null;

                if(result)
                    if (gameObject.TryGetComponent<Collider>(out otherCollider) && otherCollider.Intersects(targetRect))
                        result =  false;

                if(gameObject.TryGetComponent<BoxTrigger>(out BoxTrigger trigger))
                {
                    trigger.CheckTrigger(targetRect, this.GameObject);
                }

                if(myTrigger != null && otherCollider != null)
                {
                    var rectangles = otherCollider.GetCollisionRectangles();

                    if(rectangles.Count != 0)
                        myTrigger.CheckTrigger(rectangles[0], gameObject);
                }
            }

            return result;
        }

        public Vector2 Translate(Vector2 movement)
        {
            Vector2 destination = this.Position + movement;

            // Check scene to determine whether we can move here
            if(CanMoveToPosition(destination))
            {
                this.Position = destination;
            }

            return this.Position;
        }
    }
}
