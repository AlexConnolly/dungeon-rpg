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

        private Point GetCurrentChunkPoint()
        {
            return GetPointAtPosition(LDG.Camera.Position);
        }

        private Point GetPointAtPosition(Vector2 position)
        {
            int resolution = TileResolution * ChunkResolution;

            int x = (int)Math.Floor(position.X / (double)resolution);
            int y = (int)Math.Floor(position.Y / (double)resolution);

            return new Point(x, y);
        }

        private Point GetRelativePosition(Vector2 position)
        {
            // Determine the chunk position
            int chunkResolution = TileResolution * ChunkResolution;
            int chunkX = (int)Math.Floor(position.X / (double)chunkResolution);
            int chunkY = (int)Math.Floor(position.Y / (double)chunkResolution);

            // Determine the local tile position within the chunk
            int tileX = (int)(position.X % chunkResolution) / TileResolution;
            int tileY = (int)(position.Y % chunkResolution) / TileResolution;

            return new Point(tileX, tileY);
        }

        public Chunk GetCurrentChunk()
        {
            return GetOrGenerateChunk(GetCurrentChunkPoint());
        }

        public Point GetRelativePositionAndChunk(Vector2 currentPosition, out Chunk chunk)
        {
            var chunkPoint = GetCurrentChunkPoint();

            chunk = GetOrGenerateChunk(chunkPoint);

            var relativePosition = GetRelativePosition(currentPosition);

            return relativePosition;
        }

        public override void Initialize()
        {
            _chunkGenerator = GameObject.AddComponent<ChunkGenerator>();
        }

        internal WorldObject GetWorldObjectAtWorldPosition(Vector2 position, Point offset)
        {
            // Get the chunk
            Vector2 calculatedOffset = new Vector2(offset.X * TileResolution, offset.Y * TileResolution);

            var relativePosition = GetRelativePositionAndChunk(position + calculatedOffset, out var chunk);

            return chunk.GetWorldObjectAtPoint(relativePosition);
        }

        private Chunk GetOrGenerateChunk(Point point)
        {
            if(_chunks.ContainsKey(point))
            {
                return _chunks[point];
            }

            var newChunk = _chunkGenerator.Generate(null, ChunkResolution, LDG.Direction.Down);

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
            var currentPoint = GetCurrentChunkPoint();

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
