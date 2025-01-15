using LDG;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.World
{
    public class WorldManager : LDG.GameComponent
    {
        private Dictionary<Point, Chunk> _chunks = new Dictionary<Point, Chunk>();

        public int TileResolution { get; private set; } = 32;
        public int ChunkResolution { get; private set; } = 24;

        private Point _previousPoint;
        private ChunkView _previousChunkView;

        private ChunkGenerator _chunkGenerator;

        private Point GetCurrentChunkPoint(Vector2 currentPosition)
        {
            return GetPointAtPosition(currentPosition);
        }

        private Point GetPointAtPosition(Vector2 position)
        {
            int resolution = TileResolution * ChunkResolution;

            int x = (int)Math.Floor(position.X / (double)resolution);
            int y = (int)Math.Floor(position.Y / (double)resolution);

            return new Point(x, y);
        }

        private Point GetRelativePosition(Vector2 position, Chunk chunk)
        {
            // Determine the chunk position (world space)
            var chunkX = this.Transform.Position.X + (chunk.Location.X * (ChunkResolution * TileResolution));
            var chunkY = this.Transform.Position.Y + (chunk.Location.Y * (ChunkResolution * TileResolution));

            // Calculate the chunk boundaries
            var chunkFarX = chunkX + (ChunkResolution * TileResolution);
            var chunkFarY = chunkY + (ChunkResolution * TileResolution);

            // Calculate the offset of the world position within the chunk's space
            var offsetX = position.X - chunkX;
            var offsetY = position.Y - chunkY;

            // Calculate the relative position (tile) within the chunk
            int relativeX = (int)(offsetX / TileResolution);
            int relativeY = (int)(offsetY / TileResolution);

            // Return the relative position as a Point (tile coordinate)
            return new Point(relativeX, relativeY);
        }

        public Chunk GetCurrentChunk()
        {
            return GetOrGenerateChunk(GetCurrentChunkPoint(LDG.Camera.Position));
        }

        public Point GetRelativePositionAndChunk(Vector2 currentPosition, out Chunk chunk)
        {
            var chunkPoint = GetCurrentChunkPoint(currentPosition);

            chunk = GetOrGenerateChunk(chunkPoint);

            var relativePosition = GetRelativePosition(currentPosition, chunk);

            return relativePosition;
        }

        public override void Initialize()
        {
            _chunkGenerator = GameObject.AddComponent<ChunkGenerator>();
        }

        internal WorldObject GetWorldObjectAtWorldPosition(Vector2 position, Point offset)
        {
            var relativePosition = GetRelativePositionAndChunk(position + (offset.ToVector2() * TileResolution), out var chunk);

            return chunk.GetWorldObjectAtPoint(relativePosition);
        }

        private Chunk GetOrGenerateChunk(Point point)
        {
            if(_chunks.ContainsKey(point))
            {
                return _chunks[point];
            }

            var newChunk = _chunkGenerator.Generate(ChunkResolution, this, point);

            newChunk.Load();

            _chunks.Add(point, newChunk);

            return newChunk;
        }

        /// <summary>
        /// Returns whether it has changed or not
        /// </summary>
        /// <param name="chunkView"></param>
        /// <returns></returns>
        public bool GetChunkView(out ChunkView chunkView)
        {
            // TODO: Cache chunkView here by comparing currentPoint vs lastPoint and only returning if needed

            // Convert camera position to chunk
            var currentPoint = GetCurrentChunkPoint(LDG.Camera.Position);

            if(currentPoint == _previousPoint && _previousChunkView != null)
            {
                chunkView = _previousChunkView;

                return false;
            }

            _previousPoint = currentPoint;

            chunkView = new ChunkView()
            {
                Center = GetOrGenerateChunk(currentPoint),
                Left = GetOrGenerateChunk(new Point(currentPoint.X - 1, currentPoint.Y)),
                Right = GetOrGenerateChunk(new Point(currentPoint.X + 1, currentPoint.Y)),

                TopCenter = GetOrGenerateChunk(new Point(currentPoint.X, currentPoint.Y - 1)),
                TopLeft = GetOrGenerateChunk(new Point(currentPoint.X - 1, currentPoint.Y - 1)),
                TopRight = GetOrGenerateChunk(new Point(currentPoint.X + 1, currentPoint.Y - 1)),

                BottomCenter = GetOrGenerateChunk(new Point(currentPoint.X, currentPoint.Y + 1)),
                BottomLeft = GetOrGenerateChunk(new Point(currentPoint.X - 1, currentPoint.Y + 1)),
                BottomRight = GetOrGenerateChunk(new Point(currentPoint.X + 1, currentPoint.Y + 1)),
            };

            _previousChunkView = chunkView;

            return true;
        }
    }
}
