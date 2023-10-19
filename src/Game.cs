using LDG.Components;
using LDG.Components.Character;
using LDG.Components.Collision;
using LDG.Components.Sprite;
using LDG.Sprite;
using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace LDG
{
    public class LDGGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Scene currentScene = new Scene();

        public LDGGame()
        {
            _graphics = new GraphicsDeviceManager(this);
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

            UIManager.Load(_spriteBatch);

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
                }
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
                    MovementSpeed = 20,
                    IsMoving = true
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

            currentScene.GameObjects = new List<GameObject>()
            {
                gameObject,
                npc
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

            // Base updates
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // Draw scene
            _spriteBatch.Begin();

            foreach(var gameObject in currentScene.GameObjects)
            {
                gameObject.Components.ForEach((x) =>
                {
                    x.Draw(_spriteBatch);
                });
            }

            _spriteBatch.End();

            // Draw UI
            _spriteBatch.Begin();

            UIManager.Draw();

            _spriteBatch.End();

            // Draw debugs
            _spriteBatch.Begin();

            foreach (var gameObject in currentScene.GameObjects)
            {
                gameObject.Components.ForEach((x) =>
                {
                    x.DrawDebug(_spriteBatch);
                });
            }

            _spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}