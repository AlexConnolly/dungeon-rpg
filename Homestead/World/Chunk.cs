using Microsoft.Xna.Framework;

namespace Homestead.World
{
    public class Chunk
    {
        public Chunk(int resolution)
        {
            Floor = new FloorType[resolution * resolution];
            WorldObjects = new Dictionary<Point, WorldObject>();
        }

        public FloorType[] Floor;

        private Dictionary<Point, WorldObject> WorldObjects;

        internal WorldObject GetWorldObjectAtPoint(Point relative)
        {
            if (WorldObjects.TryGetValue(relative, out var worldObject))
                return worldObject;

            return null;
        }

        internal void AddWorldObject(WorldObject obj, Point location)
        {
            WorldObjects.Add(location, obj);
        }
    }
}
