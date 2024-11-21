using Microsoft.Xna.Framework.Content;
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

        public static void Initialize(ContentManager contentManager)
        {
            SceneManager._contentManager = contentManager;
        }

        public static Scene SetScene<T>() where T : Scene
        {
            var instance = Activator.CreateInstance<T>();

            return SetScene(instance);
        }

        public static Scene SetScene(Scene scene)
        {
            scene.Initialize();
            scene.Load(_contentManager);

            SceneManager.CurrentScene = scene;

            return scene;
        }
    }
}
