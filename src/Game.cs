using LDG.Components;
using LDG.Components.Camera;
using LDG.Components.Character;
using LDG.Components.Collision;
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

            var gameObject = new GameObject(currentScene);

            Texture2D sheet = Content.Load<Texture2D>("Graphics/Sprites/george");

            var rows = SpriteFrame.GetRowsFromSheet(sheet, new Vector2(48, 48), true);

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
                new Actor(gameObject)
                {
                    Direction = Direction.Up,
                    MovementSpeed = 60
                },
                new SpriteRenderer(gameObject)
                {

                },
                new Transform(gameObject)
                {
                    Position = new Vector2(120, 20)
                },
                new CharacterController(gameObject),
                new SpriteMovementAnimator(gameObject)
                {
                    FramesPerSecond = 10,
                    DownFrames = rows[0],
                    LeftFrames = rows[1],
                    UpFrames = rows[2],
                    RightFrames = rows[3]
                },
                new MainCameraFollow(gameObject)
            };

            var npc = new GameObject(currentScene);

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
                new Actor(npc)
                {
                    Direction = Direction.Right,
                    MovementSpeed = 20
                },
                new SpriteRenderer(npc)
                {

                },
                new Transform(npc)
                {
                    Position = new Vector2(20, 20)
                },
                new SpriteMovementAnimator(npc)
                {
                    FramesPerSecond = 10,
                    DownFrames = rows[0],
                    LeftFrames = rows[1],
                    UpFrames = rows[2],
                    RightFrames = rows[3]
                }
            };

            Texture2D tileSheet = Content.Load<Texture2D>("Graphics/Tiles/world");

            var tileFrames = SpriteFrame.GetFramesFromSheet(tileSheet, new Vector2(16, 16));

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

            for(int x = -10; x < 10; x++)
            {
                for(int y = -10; y < 10; y++)
                {
                    tileLayer.Layers[0].Tiles.Add(new TilemapItem()
                    {
                        Location = new Point(x, y),
                        Frame = tileFrames[11]
                    });
                }
            }

            for(int x = -10; x < 10; x++)
            {
                for(int y = -10; y < 10; y++)
                {
                    bool yes = Random.Shared.Next(1, 100) < 8;

                    if(yes)
                    {
                        tileLayer.Layers[1].Tiles.Add(new TilemapItem()
                        {
                            Location = new Point(x, y),
                            Frame = tileFrames[30]
                        });
                    }
                }
            }

            for (int x = -10; x < 10; x++)
            {
                for (int y = -10; y < 10; y++)
                {
                    bool yes = Random.Shared.Next(1, 100) < 2;

                    if (yes)
                    {
                        tileLayer.Layers[1].Tiles.Add(new TilemapItem()
                        {
                            Location = new Point(x, y),
                            Frame = tileFrames[31]
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

            currentScene.GameObjects = new List<GameObject>()
            {
                gameObject,
                npc,
                tilemap
            };

            // TODO: use this.Content to load your game content here
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