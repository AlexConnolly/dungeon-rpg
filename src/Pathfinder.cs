using LDG.Components.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LDG
{
    public class PathFinder
    {
        private Vector2 start;
        private Vector2 destination;
        private int objectWidth, objectHeight;
        private List<Collider> colliders;
        private int gridSize;

        public PathFinder(Vector2 start, Vector2 destination, int objectWidth, int objectHeight, List<Collider> colliders, int gridSize = 24)
        {
            this.start = SnapToGrid(start, gridSize);
            this.destination = SnapToGrid(destination, gridSize);
            this.objectWidth = objectWidth;
            this.objectHeight = objectHeight;
            this.colliders = colliders;
            this.gridSize = gridSize;
        }

        public List<Vector2> FindPath()
        {
            var openList = new PriorityQueue<Vector2, int>();
            var closedSet = new HashSet<Vector2>();
            var cameFrom = new Dictionary<Vector2, Vector2>();

            var gScores = new Dictionary<Vector2, int> { [start] = 0 };
            var fScores = new Dictionary<Vector2, int> { [start] = Heuristic(start, destination) };

            openList.Enqueue(start, fScores[start]);

            while (openList.Count > 0)
            {
                var current = openList.Dequeue();

                if (current == destination)
                    return ReconstructPath(cameFrom, current);

                closedSet.Add(current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (closedSet.Contains(neighbor) || !IsPositionValid(neighbor))
                        continue;

                    var tentativeGScore = gScores[current] + 1;

                    if (!gScores.ContainsKey(neighbor) || tentativeGScore < gScores[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScores[neighbor] = tentativeGScore;
                        fScores[neighbor] = tentativeGScore + Heuristic(neighbor, destination);

                        if (!openList.UnorderedItems.Any(item => item.Element == neighbor))
                            openList.Enqueue(neighbor, fScores[neighbor]);
                    }
                }
            }

            return null; // No path found
        }

        private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
        {
            var path = new List<Vector2> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }
            path.Reverse();
            return path.Select(p => UnsnapFromGrid(p, gridSize)).ToList();
        }

        private int Heuristic(Vector2 a, Vector2 b)
        {
            return (int)(Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y)); // Manhattan distance
        }

        private IEnumerable<Vector2> GetNeighbors(Vector2 position)
        {
            var directions = new[]
            {
                new Vector2(0, -gridSize), new Vector2(0, gridSize),
                new Vector2(-gridSize, 0), new Vector2(gridSize, 0)
            };

            foreach (var dir in directions)
                yield return position + dir;
        }

        private bool IsPositionValid(Vector2 position)
        {
            var rect = new Rectangle((int)position.X, (int)position.Y, objectWidth, objectHeight);

            foreach (var collider in colliders)
            {
                if (collider.Intersects(rect))
                    return false;
            }
            return true;
        }

        private Vector2 SnapToGrid(Vector2 position, int gridSize)
        {
            return new Vector2(
                (float)Math.Floor(position.X / gridSize) * gridSize,
                (float)Math.Floor(position.Y / gridSize) * gridSize
            );
        }

        private Vector2 UnsnapFromGrid(Vector2 position, int gridSize)
        {
            return new Vector2(position.X + gridSize / 2, position.Y + gridSize / 2);
        }
    }
}
