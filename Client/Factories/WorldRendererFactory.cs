using LDG;
using LDG.Components.Collision;
using LDG.Components.Tile;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Factories
{
    public static class WorldRendererFactory
    {
        public static GameObject CreateWorldRenderer(Scene scene)
        {
            var gameObject = scene.AddGameObject();

            var tilemap = gameObject.AddComponent<Tilemap>();

            Point tileSize = new Point(48, 48);

            tilemap.Layers = new List<TilemapLayer>()
            {
                // Ground layer (eg. Grass)
                new TilemapLayer() { Tiles = new List<TilemapItem>() },

                // Ground detail layer
                new TilemapLayer() {Tiles = new List<TilemapItem>()},

                // Object layer (eg. Tables, anything the player can't walk through)
                new TilemapLayer() { Tiles = new List<TilemapItem>() },

                // Detail layer (eg. A rug on the floor)
                new TilemapLayer() {Tiles = new List<TilemapItem>()}
            };

            tilemap.TileSize = tileSize;

            var collider = gameObject.AddComponent<TileLayerCollider>();

            collider.TileSize = tileSize;
            collider.Layer = tilemap.Layers[2];

            return gameObject;

            // Draw some grass just to show it works
            int grassSize = 200;

            for(int x = -grassSize; x < grassSize; x++)
            {
                for(int y = -grassSize; y < grassSize; y++)
                {
                    bool shouldPlace = Random.Shared.Next(0, 100) < 5;

                    if(shouldPlace)
                    {
                        tilemap.Layers[1].Tiles.Add(new TilemapItem()
                        {
                            Location = new Point(x, y),
                            Frame = SpriteSheetManager.GetSheetByName("tiles_world").GetByKey("58")
                        });
                    }
                    
                }
            }

            for (int x = -grassSize; x < grassSize; x++)
            {
                for (int y = -grassSize; y < grassSize; y++)
                {
                    bool shouldPlace = Random.Shared.Next(0, 100) < 10;

                    if (shouldPlace)
                    {
                        tilemap.Layers[2].Tiles.Add(new TilemapItem()
                        {
                            Location = new Point(x, y),
                            Frame = SpriteSheetManager.GetSheetByName("tiles_world").GetByKey("30")
                        });
                    }

                }
            }

            return gameObject;
        }
    }
}
