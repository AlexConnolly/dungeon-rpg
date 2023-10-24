using LDG.Components.Collision;
using LDG.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.NPC
{
    public class NPCMover : GameComponent
    {
        public NPCMover()
        {

        }

        public GameObject Target { get; set; }

        public List<Rectangle> GetCollisionRectangles()
        {
            var colliders = GameObject.Scene.GetAllComponentsOfType<TileLayerCollider>();

            var rectangles = new List<Rectangle>();

            foreach(var collider in colliders)
            {
                if(collider != null)
                    rectangles.AddRange(collider.GetCollisionRectangles());
            }

            return rectangles;
        }

        private Vector2 TargetPosition = Vector2.Zero;

        private List<Vector2> path = new List<Vector2>();

        private int nodeIndex = 0;

        public override void Update(TimeFrame time)
        {
            // Recalculate if the position has changed
            if(this.Target == null)
            {
                return;
            }

            if(this.Target.GetComponent<Transform>().Position != TargetPosition)
            {
                TargetPosition = this.Target.GetComponent<Transform>().Position;

                // Recalculate
                var range = GetComponent<BoxCollider>().GetCollisionRectangles()[0];

                int size = Math.Max(range.Width, range.Height);

                path = new PathFinder(GetCollisionRectangles()).FindPath(TargetPosition, this.GetComponent<Collider>().GetCollisionRectangles()[0], size);
            }

            if (path == null || path.Count == 0)
                return;

            var actor = this.GetComponent<Actor>();

            // Determine the direction
            if (nodeIndex >= path.Count)
            {
                path = new List<Vector2>();
                nodeIndex = 0;

                actor.IsMoving = false;

                return;
            }

            var firstNode = path[nodeIndex];

            actor.IsMoving = true;
            actor.Direction = DirectionExtensions.FromTwoPoints(this.Transform.Position, firstNode);

            if(Math.Abs(Vector2.Distance(firstNode, this.Transform.Position)) <= (actor.MovementSpeed))
            {
                nodeIndex++;
            }
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < path.Count; x++)
            {
                var node = path[x];

                spriteBatch.DrawSquare(new Rectangle(LDG.Camera.WorldPositionToCameraPoint(new Vector2(node.X, node.Y)), new Point(1 * x, 1 * x)), Color.Red, null, 0);
            }
        }
    }
}
