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
        private readonly WorldManager _worldManager;
        private Tilemap _tilemap;
        private Spritesheet _floorSheet;

        public WorldRenderer()
        {
            _worldManager = new WorldManager();
        }

        public override void Initialize()
        {
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

            for (int i = 0; i < chunk.Floor.Length; i++)
            {
                // Calculate x and y based on the chunk resolution
                int x = i % _worldManager.ChunkResolution; // Remainder gives the column (x)
                int y = i / _worldManager.ChunkResolution; // Integer division gives the row (y)

                // Now you have x and y for the current index
                _tilemap.SetTileAtLocation(0, new Microsoft.Xna.Framework.Point(offset.X + x, offset.Y + y), chunk.Floor[i] == FloorType.Grass ? grass : flower);
            }
        }

        public override void Update(TimeFrame time)
        {
            // Update tilemap only if we changed chonks
            if(_worldManager.GetChunkView(out var visibleChunks))
            {
                DrawChunk(visibleChunks.Center, new Point(0, 0));
                DrawChunk(visibleChunks.Left, new Point(-_worldManager.ChunkResolution, 0));
                DrawChunk(visibleChunks.Right, new Point(_worldManager.ChunkResolution, 0));

                DrawChunk(visibleChunks.TopCenter, new Point(0, -_worldManager.ChunkResolution));
                DrawChunk(visibleChunks.TopLeft, new Point(-_worldManager.ChunkResolution, -_worldManager.ChunkResolution));
                DrawChunk(visibleChunks.TopRight, new Point(_worldManager.ChunkResolution, -_worldManager.ChunkResolution));

                DrawChunk(visibleChunks.BottomCenter, new Point(0, +_worldManager.ChunkResolution));
                DrawChunk(visibleChunks.BottomLeft, new Point(-_worldManager.ChunkResolution, +_worldManager.ChunkResolution));
                DrawChunk(visibleChunks.BottomRight, new Point(_worldManager.ChunkResolution, +_worldManager.ChunkResolution));
            }
        }
    }
}
