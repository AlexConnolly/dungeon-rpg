using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public static class SceneManager
    {
        public static Scene CurrentScene { get; private set; }

        private static ContentManager _contentManager;
        private static GraphicsDevice _graphicsDevice;

        public static void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            SceneManager._contentManager = contentManager;
            SceneManager._graphicsDevice = graphicsDevice;
        }

        public static Scene SetScene<T>() where T : Scene
        {
            var instance = Activator.CreateInstance<T>();

            return SetScene(instance);
        }

        public static Scene SetScene(Scene scene)
        {
            scene._contentManager = _contentManager;
            scene._graphicsDevice = _graphicsDevice;

            scene.Initialize();

            scene.Load(_contentManager, _graphicsDevice);

            SceneManager.CurrentScene = scene;

            return scene;
        }
    }
}
