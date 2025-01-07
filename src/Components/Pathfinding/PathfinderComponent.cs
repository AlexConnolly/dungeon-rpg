using LDG.Components.Actor;
using LDG.Components.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Pathfinding
{
    public class PathfinderComponent : GameComponent
    {
        private List<Vector2> path = null; // Current path
        private int currentPathIndex = 0; // Current position in the path
        private Nullable<Vector2> destination = Vector2.Zero; // Target destination
        private Vector2 lastKnownPosition = Vector2.Zero; // Last known position to detect destination changes
        private const float speed = 0.2f; // Movement speed multiplier

        private float stuckTime = 0.5f;

        public void SetDestination(Vector2 newDestination)
        {
            destination = newDestination;
            path = null; // Reset path to recalculate on the next update
            currentPathIndex = 0;
        }

        public override void Update(TimeFrame time)
        {
            if (destination == null)
                return;

            // Get the actor component
            var actor = GetComponent<ActorComponent>();
            if (actor == null) return;

            // Recalculate path if necessary
            if (path == null || currentPathIndex >= path.Count || lastKnownPosition != destination)
            {
                var getColliders = GameObject.Scene.GetAllComponentsOfTypeAs<TileLayerCollider, Collider>();

                var pathFinder = new PathFinder(
                    new Vector2((int)Transform.Position.X, (int)Transform.Position.Y),
                    new Vector2((int)destination.Value.X, (int)destination.Value.Y),
                    24, // Default size width
                    24, // Default size height
                    getColliders
                );

                path = pathFinder.FindPath();
                currentPathIndex = 0;
                lastKnownPosition = destination.Value;
            } else
            {
                // Aka we have a path
                bool isStuck = lastKnownPosition == Transform.Position;

                if(isStuck)
                {
                    stuckTime -= time.Delta;

                    if(stuckTime <= 0)
                    {
                        // Reset path
                        stuckTime = 0.5f;
                        path = null;
                        return;
                    }
                }
            }

            // No path available or target already reached
            if (path == null || currentPathIndex >= path.Count)
            {
                actor.IsMoving = false;
                return;
            }

            // Get next target position
            var nextPosition = path[currentPathIndex];

            // Calculate movement
            var moveX = nextPosition.X - actor.Transform.Position.X;
            var moveY = nextPosition.Y - actor.Transform.Position.Y;

            // Determine direction
            if (Math.Abs(moveX) > Math.Abs(moveY))
                actor.Direction = moveX > 0 ? Direction.Right : Direction.Left;
            else
                actor.Direction = moveY > 0 ? Direction.Down : Direction.Up;

            // Check if reached next position
            if (Vector2.Distance(actor.Transform.Position, nextPosition) < 0.1f)
            {
                currentPathIndex++;
            }

            // Update movement status
            actor.IsMoving = currentPathIndex < path.Count;

            lastKnownPosition = Transform.Position;
        }
    }
}
