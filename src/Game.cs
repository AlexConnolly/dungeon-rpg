using LDG.Audio;
using LDG.Components;
using LDG.Components.Audio;
using LDG.Components.Camera;
using LDG.Components.Character;
using LDG.Components.Collision;
using LDG.Components.HUD;
using LDG.Components.NPC;
using LDG.Components.Sprite;
using LDG.Components.Tile;
using LDG.Sprite;
using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LDG
{
    public class LDGGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Scene currentScene = new Scene();

        private bool isDebugMode = false;

        public LDGGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = Screen.Resolution.X;
            _graphics.PreferredBackBufferHeight = Screen.Resolution.Y;

            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            UIManager.Load(_spriteBatch, Content);
            SpriteSheetManager.Load(Content);
            AudioManager.Load(Content);

            var gameObject = new GameObject(currentScene);

            var georgeSheet = SpriteSheetManager.GetSheetByName("character_george");

            gameObject.Components = new List<GameComponent>()
            {
                new BoxCollider(gameObject)
                {
                    Bounds = new Vector2()
                    {
                        X = 20,
                        Y = 20
                    }
                },
                new SpriteRenderer(gameObject)
                {

                },
                new Transform(gameObject)
                {
                    Position = new Vector2(120, 20)
                },
                new CharacterController(gameObject),
                new SpriteMovementAnimator(gameObject, georgeSheet)
                {
                    FramesPerSecond = 10
                },
                new MainCameraFollow(gameObject),
                new ItemBar(gameObject),
                new HealthBar(gameObject),
                new AudioSource(gameObject, AudioManager.GetSound("character_footsteps")),
                new Inventory(gameObject)
            };

            gameObject.Components.Add(
                new Actor(gameObject, gameObject.GetComponent<AudioSource>())
                {
                    Direction = Direction.Up,
                    MovementSpeed = 85
                }
            );

            var npc = new GameObject(currentScene);

            var chickenFrames = SpriteSheetManager.GetSheetByName("character_chicken");

            npc.Components = new List<GameComponent>()
            {
                new BoxCollider(npc)
                {
                    Bounds = new Vector2()
                    {
                        X = 20,
                        Y = 20
                    }
                },
                new SpriteRenderer(npc)
                {

                },
                new Transform(npc)
                {
                    Position = new Vector2(20, 20)
                },
                new SpriteMovementAnimator(npc, chickenFrames)
                {
                    FramesPerSecond = 10
                }, 
                new AudioSource(npc, AudioManager.GetSound("character_footsteps"))
            };

            npc.Components.Add(
                new Actor(npc, npc.GetComponent<AudioSource>())
                {
                    Direction = Direction.Right,
                    MovementSpeed = 20,
                    IsMoving = true
                });

            var worldTiles = SpriteSheetManager.GetSheetByName("tiles_world");

            var tilemap = new GameObject(currentScene)
            {
                DrawPriority = 99
            };

            var tileLayer =
                new Tilemap(tilemap)
                {
                    Layers = new List<TilemapLayer>()
                    {
                        new TilemapLayer()
                        {
                            Tiles = new List<TilemapItem>()
                            {

                            }
                        },
                        new TilemapLayer()
                        {
                            Tiles = new List<TilemapItem>()
                            {

                            }
                        }
                    },
                    TileSize = new Point(32, 32)
                };

            for(int x = -100; x < 100; x++)
            {
                for(int y = -100; y < 100; y++)
                {
                    tileLayer.Layers[0].Tiles.Add(new TilemapItem()
                    {
                        Location = new Point(x, y),
                        Frame = worldTiles.GetByKey("64")
                    });
                }
            }

            for(int x = -100; x < 100; x++)
            {
                for(int y = -100; y < 100; y++)
                {
                    bool yes = Random.Shared.Next(1, 100) < 8;

                    if(yes)
                    {
                        tileLayer.Layers[1].Tiles.Add(new TilemapItem()
                        {
                            Location = new Point(x, y),
                            Frame = worldTiles.GetByKey("30")
                        });
                    }
                }
            }

            for (int x = -100; x < 100; x++)
            {
                for (int y = -100; y < 100; y++)
                {
                    bool yes = Random.Shared.Next(1, 100) < 8;

                    if (yes)
                    {
                        tileLayer.Layers[0].Tiles.Add(new TilemapItem()
                        {
                            Location = new Point(x, y),
                            Frame = worldTiles.GetByKey("12")
                        });
                    }
                }
            }

            for (int x = -100; x < 100; x++)
            {
                for (int y = -100; y < 100; y++)
                {
                    bool yes = Random.Shared.Next(1, 100) < 8;

                    if (yes)
                    {
                        tileLayer.Layers[0].Tiles.Add(new TilemapItem()
                        {
                            Location = new Point(x, y),
                            Frame = worldTiles.GetByKey("20")
                        });
                    }
                }
            }

            for (int x = -100; x < 100; x++)
            {
                for (int y = -100; y < 100; y++)
                {
                    bool yes = Random.Shared.Next(1, 100) < 2;

                    if (yes)
                    {
                        tileLayer.Layers[1].Tiles.Add(new TilemapItem()
                        {
                            Location = new Point(x, y),
                            Frame = worldTiles.GetByKey("38")
                        });
                    }
                }
            }

            var layerCollider = new TileLayerCollider(tilemap)
            {
                TileSize = new Point(32, 32),
                Layer = tileLayer.Layers[1]
            };

            tilemap.Components = new List<GameComponent>()
            {
                new Transform(tilemap),
                tileLayer,
                layerCollider
            };

            currentScene.AddGameobject(gameObject);
            currentScene.AddGameobject(npc);
            currentScene.AddGameobject(tilemap);
        }

        protected override void Update(GameTime gameTime)
        {
            var timeFrame = new TimeFrame(gameTime);

            // Update the UI manager
            UIManager.Update(timeFrame);

            // Update the scene
            foreach (var gameObject in currentScene.GameObjects)
            {
                gameObject.Components.ForEach((x) =>
                {
                    x.Update(timeFrame);
                });
            }

            var keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.Tab))
            {
                isDebugMode = !isDebugMode;
            }

            // Base updates
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // Draw scene
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            foreach(var gameObject in currentScene.GameObjects.OrderByDescending(x=> x.DrawPriority))
            {
                gameObject.Components.ForEach((x) =>
                {
                    x.Draw(_spriteBatch);
                });
            }

            _spriteBatch.End();

            // Draw UI
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            UIManager.Draw();

            _spriteBatch.End();

            // Draw debugs
            if(isDebugMode)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                foreach (var gameObject in currentScene.GameObjects)
                {
                    gameObject.Components.ForEach((x) =>
                    {
                        x.DrawDebug(_spriteBatch);
                    });
                }

                _spriteBatch.End();
            }


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}