using LDG;
using LDG.Components.Tile;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Homestead.World
{
    internal class WorldRenderer : LDG.GameComponent
    {
        private WorldManager _worldManager;
        private Tilemap _tilemap;
        private Spritesheet _floorSheet;


        public override void Initialize()
        {
            _worldManager = GameObject.AddComponent<WorldManager>();

            _tilemap = GameObject.AddComponent<Tilemap>();

            _tilemap.AddLayer(new TilemapLayer()
            {
                HasCollision = false,
                Tiles = new List<TilemapItem>()
            });

            _tilemap.TileSize = new Point(_worldManager.TileResolution, _worldManager.TileResolution);
        }

        public override void Load(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            _floorSheet = Spritesheet.FromSheet(Texture2D.FromFile(graphicsDevice, "Media/Spritesheets/Floor.png"), new Point(32, 32));
        }

        private void DrawChunk(Chunk chunk, Point offset)
        {
            var grass = _floorSheet.GetByKey("0");
            var flower = _floorSheet.GetByKey("1");
            var grassRock = _floorSheet.GetByKey("2");

            for (int i = 0; i < chunk.Floor.Length; i++)
            {
                // Calculate x and y based on the chunk resolution
                int x = i % _worldManager.ChunkResolution; // Remainder gives the column (x)
                int y = i / _worldManager.ChunkResolution; // Integer division gives the row (y)

                SpriteFrame frame = null;

                switch(chunk.Floor[i])
                {
                    case FloorType.Grass:
                        frame = grass;
                        break;

                    case FloorType.GrassRock:
                        frame = grassRock;
                        break;

                    case FloorType.Flower:
                        frame = flower;
                        break;
                }

                // Now you have x and y for the current index
                _tilemap.SetTileAtLocation(0, new Microsoft.Xna.Framework.Point(offset.X + x, offset.Y  + y), frame);
            }
        }

        private Point GetCameraOffset()
        {
            int resolution = _worldManager.TileResolution * _worldManager.ChunkResolution;

            var currentCameraPosition = LDG.Camera.Position;

            int x = (int)Math.Floor(currentCameraPosition.X / (double)resolution);
            int y = (int)Math.Floor(currentCameraPosition.Y / (double)resolution);

            return new Point(x * _worldManager.ChunkResolution, y * _worldManager.ChunkResolution);
        }

        public override void Update(TimeFrame time)
        {
            // Update tilemap only if we changed chonks
            if(_worldManager.GetChunkView(out var visibleChunks))
            {
                var cameraOffset = GetCameraOffset();

                DrawChunk(visibleChunks.Center, cameraOffset + new Point(0, 0));
                DrawChunk(visibleChunks.Left, cameraOffset + new Point(-_worldManager.ChunkResolution, 0));
                DrawChunk(visibleChunks.Right, cameraOffset + new Point(_worldManager.ChunkResolution, 0));

                DrawChunk(visibleChunks.TopCenter, cameraOffset + new Point(0, -_worldManager.ChunkResolution));
                DrawChunk(visibleChunks.TopLeft, cameraOffset + new Point(-_worldManager.ChunkResolution, -_worldManager.ChunkResolution));
                DrawChunk(visibleChunks.TopRight, cameraOffset + new Point(_worldManager.ChunkResolution, -_worldManager.ChunkResolution));

                DrawChunk(visibleChunks.BottomCenter, cameraOffset + new Point(0, +_worldManager.ChunkResolution));
                DrawChunk(visibleChunks.BottomLeft, cameraOffset + new Point(-_worldManager.ChunkResolution, +_worldManager.ChunkResolution));
                DrawChunk(visibleChunks.BottomRight, cameraOffset + new Point(_worldManager.ChunkResolution, +_worldManager.ChunkResolution));
            }
        }
    }
}
