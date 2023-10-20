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
        public NPCMover(GameObject gameObject) : base(gameObject)
        {

        }

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

        public override void Update(TimeFrame time)
        {
        }

        private List<Vector2> CollisionRectangles = null;

        public override void DrawDebug(SpriteBatch spriteBatch)
        {
            Collider collider = this.GameObject.GetComponent<BoxCollider>();

            var range = collider.GetCollisionRectangles()[0];

            int size = Math.Max(range.Width, range.Height);

            if(CollisionRectangles == null)
            {
                CollisionRectangles = new PathFinder(GetCollisionRectangles()).FindPath(new Vector2(150, 650), this.GetComponent<Collider>().GetCollisionRectangles()[0], size);
            }

            foreach(var node in CollisionRectangles)
            {
                spriteBatch.DrawSquare(new Rectangle(LDG.Camera.WorldPositionToCameraPoint(new Vector2(node.X, node.Y)), new Point(5, 5)), Color.Red, null, 0);
            }
        }
    }
}
