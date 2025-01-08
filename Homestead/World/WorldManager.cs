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

        private Point GetCurrentChunkPoint()
        {
            int resolution = TileResolution * ChunkResolution;

            var currentCameraPosition = LDG.Camera.Position;

            return new Point((int)currentCameraPosition.X / resolution, (int)currentCameraPosition.Y / resolution);
        }

        private Chunk GetOrGenerateChunk(Point point)
        {
            if(_chunks.ContainsKey(point))
            {
                return _chunks[point];
            }

            var newChunk = ChunkGenerator.Generate(null, ChunkResolution, LDG.Direction.Down);

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
