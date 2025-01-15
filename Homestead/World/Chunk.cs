using Microsoft.Xna.Framework;

namespace Homestead.World
{
    public class Chunk
    {
        private readonly WorldManager _worldManager;
        private readonly Point _chunkLocation;

        public Point Location
        {
            get
            {
                return _chunkLocation;
            }
        }

        public Chunk(int resolution, WorldManager worldManager, Point location)
        {
            Floor = new FloorType[resolution * resolution];
            WorldObjects = new Dictionary<Point, WorldObject>();

            _worldManager = worldManager;
            _chunkLocation = location;
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

            // Reset world position relative to the chunk
            var chunkX = _worldManager.Transform.Position.X + (_chunkLocation.X * (_worldManager.ChunkResolution * _worldManager.TileResolution));
            var chunkY = _worldManager.Transform.Position.Y + (_chunkLocation.Y * (_worldManager.ChunkResolution * _worldManager.TileResolution));

            // Now take the location 
            var locationX = location.X * _worldManager.TileResolution;
            var locationY = location.Y * _worldManager.TileResolution;

            obj.Transform.Position = new Vector2(chunkX + locationX, chunkY + locationY);
        }

        public void Unload()
        {
            foreach (var worldObject in WorldObjects)
                worldObject.Value.Enabled = false;
        }

        public void Load()
        {
            foreach (var worldObject in WorldObjects)
                worldObject.Value.Enabled = true;
        }
    }
}
