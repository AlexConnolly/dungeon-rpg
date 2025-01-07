using LDG;
using LDG.Components.Camera;
using LDG.Components.Tile;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RestaurantGame.Scenes.Game.Components.Ui;

namespace RestaurantGame.Scenes.Game
{
    internal class GameScene : Scene
    {
        public override Color ClearColor => Color.LawnGreen;

        public override void Initialize()
        {
            var uiObject = AddGameObject();
            uiObject.AddComponent<GameUiComponent>();

            // We want it to be drawn last
            uiObject.DrawPriority = 0;
            
            var tilemapObject = AddGameObject();

            tilemapObject.Transform.Position = new Vector2(-500, -200);

            var tilemap = tilemapObject.AddComponent<Tilemap>();

            var worldSheet = Spritesheet.FromSheet(Texture2D.FromFile(Graphics, "Scenes/Game/Artwork/World.png"), new Point(24, 24));

            tilemap.LoadFromConfig(new TilemapConfig()
            {
                Layers = new List<TilemapConfig.TilemapConfigLayer>()
                {
                    new TilemapConfig.TilemapConfigLayer()
                    {
                        IsCollision = false,
                        Sheet = worldSheet,
                        Tiles = new List<List<string>>()
                        {
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                            new List<string>()
                            {
                                "0", "0", "0", "0", "1", "1", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2"
                            },
                        }
                    },
                    new TilemapConfig.TilemapConfigLayer()
                    {
                        IsCollision = true,
                        Sheet = worldSheet,
                        Tiles = new List<List<string>>()
                        {
                            new List<string>()
                            {
                                "", "", "", "", "", "","3", "4"
                            },
                            new List<string>()
                            {
                                "", "", "", "", "", "","3", "4"
                            },
                            new List<string>()
                            {
                                "", "", "", "", "", "","3", "4"
                            },
                            new List<string>()
                            {
                                "", "", "", "", "", "","3", "4"
                            }
                        }
                    }
                }
            });
        }

        private float spawnTime = 3.0f;
        private float currentSpawnTime = 3.0f;

        private void SpawnNpc()
        {
            var npcObject = AddGameObject(new Vector2(-380, 230));

            npcObject.AddComponent<Npc>();
        }

        public override void Update(TimeFrame time)
        {
            currentSpawnTime -= time.Delta;

            if(currentSpawnTime <= 0)
            {
                currentSpawnTime = spawnTime;
                SpawnNpc();
            }
        }
    }
}
