using LDG.Audio;
using LDG.Scenes;
using LDG.Sprite;
using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace LDG
{
    public class LDGGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private bool isDebugMode = false;

        private Scene startScene;

        public LDGGame(Scene startScene = null)
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = Screen.Resolution.X;
            _graphics.PreferredBackBufferHeight = Screen.Resolution.Y;

            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.startScene = startScene;
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

            if(this.startScene != null)
            {
                Scene.SetScene(this.startScene);
            } else
            {
                Scene.LoadScene<DemoScene>();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var timeFrame = new TimeFrame(gameTime);

            // Update keyboard
            Input.KeyboardHelper.Update(timeFrame);

            // Update the UI manager
            UIManager.Update(timeFrame);

            // Update the scene
            foreach (var gameObject in Scene.CurrentScene.GameObjects)
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
            GraphicsDevice.Clear(Scene.CurrentScene.ClearColor);

            // Draw scene
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            foreach(var gameObject in Scene.CurrentScene.GameObjects.OrderByDescending(x=> x.DrawPriority))
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

                foreach (var gameObject in Scene.CurrentScene.GameObjects)
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