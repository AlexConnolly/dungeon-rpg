using LDG.Components.Actor;
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
        private Vector2 destination = Vector2.Zero; // Target destination
        private Vector2 lastKnownPosition = Vector2.Zero; // Last known position to detect destination changes
        private const float speed = 0.2f; // Movement speed multiplier

        public void SetDestination(Vector2 newDestination)
        {
            destination = newDestination;
            path = null; // Reset path to recalculate on the next update
            currentPathIndex = 0;
        }

        public override void Update(TimeFrame time)
        {
            base.Update(time);

            // Get the actor component
            var actor = GetComponent<ActorComponent>();
            if (actor == null) return;

            // Recalculate path if necessary
            if (path == null || currentPathIndex >= path.Count || lastKnownPosition != destination)
            {
                var colliders = new List<Rectangle> // Replace this with actual collider data
            {
                new Rectangle(50, 50, 30, 30) // Example collider
            };

                var pathFinder = new PathFinder(
                    new Vector2((int)Transform.Position.X, (int)Transform.Position.Y),
                    new Vector2((int)destination.X, (int)destination.Y),
                    24, // Default size width
                    24, // Default size height
                    colliders
                );

                path = pathFinder.FindPath();
                currentPathIndex = 0;
                lastKnownPosition = destination;
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
        }
    }
}
