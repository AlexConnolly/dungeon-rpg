using LDG.Components;
using LDG.Components.Character;
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

        private List<GameObject> gameObjects = new List<GameObject>();

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

            var gameObject = new GameObject();

            gameObject.Components = new List<GameComponent>()
            {
                new Actor(gameObject)
                {
                    Direction = Direction.Up,
                    MovementSpeed = 30
                },
                new SpriteRenderer(gameObject)
                {
                    Texture = Content.Load<Texture2D>("Graphics/Sprites/Sample")
                },
                new Transform(gameObject)
                {

                },
                new CharacterController(gameObject)
            };

            gameObjects = new List<GameObject>()
            {
                gameObject
            };

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var gameObject in gameObjects)
            {
                gameObject.Components.ForEach((x) =>
                {
                    x.Update(gameTime);
                });
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach(var gameObject in gameObjects)
            {
                gameObject.Components.ForEach((x) =>
                {
                    x.Draw(_spriteBatch);
                });
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}