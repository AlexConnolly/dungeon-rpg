using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public class PathFinder
    {
        private List<Rectangle> colliders;
        private Rectangle movingObject;

        public PathFinder(List<Rectangle> colliders)
        {
            this.colliders = colliders;
        }

        private const int STEP_SIZE = 10;  // Increase step size for reduced resolution
        private const int MAX_DEPTH = 1000000000; // Limit the search depth

        // ... [rest of the code]
        public List<Vector2> FindPath(Vector2 destination, Rectangle currentObject)
        {
            this.movingObject = currentObject;

            Queue<Node> queue = new Queue<Node>();
            HashSet<Point> visited = new HashSet<Point>();
            Node startNode = new Node(currentObject.Location, null);
            queue.Enqueue(startNode);
            int depth = 0; // Track the current search depth

            while (queue.Count > 0 && depth < MAX_DEPTH * 2) // Increase the MAX_DEPTH
            {
                depth++;
                Node current = queue.Dequeue();
                visited.Add(current.position);

                if (WithinStepSize(current.position, destination.ToPoint())) // New check to see if destination is nearby
                {
                    return RetracePath(current);
                }

                // Prioritize neighbors based on their distance to the destination
                var neighbors = GetNeighbors(current.position);
                neighbors.Sort((a, b) =>
                {
                    int distanceA = (int)Math.Abs(a.X - destination.X) + (int)Math.Abs(a.Y - destination.Y);
                    int distanceB = (int)Math.Abs(b.X - destination.X) + (int)Math.Abs(b.Y - destination.Y);
                    return distanceA.CompareTo(distanceB);
                });

                foreach (var neighborPosition in neighbors)
                {
                    if (!visited.Contains(neighborPosition) && !IsCollider(neighborPosition))
                    {
                        queue.Enqueue(new Node(neighborPosition, current));
                        visited.Add(neighborPosition);
                    }
                }
            }

            return null; // No path found or exceeded max depth
        }

        private bool WithinStepSize(Point current, Point destination)
        {
            return Math.Abs(current.X - destination.X) <= STEP_SIZE && Math.Abs(current.Y - destination.Y) <= STEP_SIZE;
        }

        private List<Point> GetNeighbors(Point current)
        {
            List<Point> neighbors = new List<Point>
        {
            new Point(current.X + STEP_SIZE, current.Y),
            new Point(current.X - STEP_SIZE, current.Y),
            new Point(current.X, current.Y + STEP_SIZE),
            new Point(current.X, current.Y - STEP_SIZE)
        };

            return neighbors;
        }


        private bool IsCollider(Point position)
        {
            Rectangle objectBounds = new Rectangle(position.X, position.Y, movingObject.Width, movingObject.Height);
            foreach (var collider in colliders)
            {
                if (objectBounds.Intersects(collider))
                {
                    return true;
                }
            }
            return false;
        }

        private List<Vector2> RetracePath(Node endNode)
        {
            List<Vector2> path = new List<Vector2>();
            Node currentNode = endNode;

            while (currentNode != null)
            {
                path.Add(currentNode.position.ToVector2());
                currentNode = currentNode.parent;
            }

            path.Reverse();
            return path;
        }

        private class Node
        {
            public Point position;
            public Node parent;

            public Node(Point position, Node parent)
            {
                this.position = position;
                this.parent = parent;
            }
        }
    }
}
