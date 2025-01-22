using LDG.Audio;
using LDG.Scenes;
using LDG.Shaders;
using LDG.Sprite;
using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace LDG
{
    public class ShaderConfiguration
    {
        public string Key { get; set; }
        public string Resource { get; set; }
    }

    public class LDGGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private bool isDebugMode = false;

        private Scene startScene;

        private ShaderConfiguration[] _shaders;

        public LDGGame(ShaderConfiguration[] shaders = null, Scene startScene = null)
        {

            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = Screen.Resolution.X;
            _graphics.PreferredBackBufferHeight = Screen.Resolution.Y;

            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _shaders = shaders;

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
            SceneManager.Initialize(Content, GraphicsDevice);

            if(this.startScene != null)
            {
                SceneManager.SetScene(this.startScene);
            } else
            {
                SceneManager.SetScene<DemoScene>();
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
            foreach (var gameObject in SceneManager.CurrentScene.GameObjects)
            {
                if (!gameObject.Enabled)
                    continue;

                gameObject.Components.ForEach((x) =>
                {
                    if(x.Enabled)
                        x.Update(timeFrame);
                });
            }

            SceneManager.CurrentScene.Update(timeFrame);

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
            GraphicsDevice.Clear(SceneManager.CurrentScene.ClearColor);

            // Draw scene
            Effect? currentEffect = null; // Tracks the current active Effect
            bool batchStarted = false;

            // Draw scene
            foreach (var gameObject in SceneManager.CurrentScene.GameObjects
                .OrderByDescending(x => x.DrawPriority))
            {
                if (!gameObject.Enabled) continue;

                foreach (var component in gameObject.Components.Where(x => x.Enabled))
                {
                    // Check if the component has an Effect
                    var effectComponent = component as ShaderComponent;
                    var effect = effectComponent?.Effect;

                    // Start a new batch if no batch started yet, or if the Effect changes
                    if (!batchStarted || currentEffect != effect)
                    {
                        if (batchStarted)
                            _spriteBatch.End();

                        _spriteBatch.Begin(
                            SpriteSortMode.Deferred,
                            BlendState.AlphaBlend,
                            SamplerState.PointClamp,
                            null,
                            null,
                            effect
                        );

                        currentEffect = effect;
                        batchStarted = true;
                    }

                    // Draw the component
                    component.Draw(_spriteBatch);
                }
            }

            // Ensure the last batch ends, regardless of what happens in the loop
            if (batchStarted)
            {
                _spriteBatch.End();
            }


            // Draw UI
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            UIManager.Draw();

            _spriteBatch.End();

            // Draw debugs
            if(isDebugMode)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                foreach (var gameObject in SceneManager.CurrentScene.GameObjects)
                {
                    gameObject.Components.ForEach((x) =>
                    {
                        x.DrawDebug(_spriteBatch);
                    });
                }

                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}