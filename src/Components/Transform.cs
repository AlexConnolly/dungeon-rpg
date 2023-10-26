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

        public Vector2 Position { get; set; }

        private bool CanMoveToPosition(Vector2 position)
        {
            if (!this.GameObject.TryGetComponent<BoxCollider>(out BoxCollider collider))
                return true;

            float halfWidth = collider.Bounds.X / 2;
            float halfHeight = collider.Bounds.Y / 2;

            Rectangle targetRect = new Rectangle((int)(position.X - halfWidth), (int)(position.Y - halfHeight), (int)collider.Bounds.X, (int)collider.Bounds.Y);

            foreach (var gameObject in this.GameObject.Scene.GameObjects)
            {
                if (gameObject == this.GameObject)
                    continue;

                if (gameObject.TryGetComponent<Collider>(out Collider otherCollider) && otherCollider.Intersects(targetRect))
                    return false;
            }

            return true;
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
