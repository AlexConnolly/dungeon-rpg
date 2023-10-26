using LDG.Components;
using LDG.Components.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public class Scene
    {

        private List<GameObject> _gameObjects = new List<GameObject>();

        public static Scene CurrentScene { 
            get
            {
                return _currentScene;
            }        
        }

        private static Scene _currentScene = null;

        public IEnumerable<GameObject> GameObjects
        {
            get
            {
                foreach(var obj in this._gameObjects)
                {
                    yield return obj;
                }
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual Color ClearColor { get; } = Color.CornflowerBlue;

        public GameObject AddGameObject()
        {
            var gameObject = new GameObject(this);

            gameObject.AddComponent<Transform>();

            this._gameObjects.Add(gameObject);

            return gameObject;
        }

        public GameObject GetGameObjectWithTag(string tag)
        {
            foreach(var obj in this._gameObjects)
            {
                if (obj.Tag == null)
                    continue;

                if(obj.Tag.Equals(tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    return obj;
                }
            }

            return null;
        }

        public static T LoadScene<T>() where T : Scene
        {
            var scene = Activator.CreateInstance<T>();

            scene.Initialize();

            Scene._currentScene = scene;

            return scene;
        }

        public static Scene SetScene(Scene scene)
        {
            scene.Initialize();

            Scene._currentScene = scene;

            return scene;
        }

        public List<T> GetAllComponentsOfType<T>() where T : GameComponent
        {
            var components = new List<T>();

            foreach(var gameObject in _gameObjects)
            {
                var objectComponents = gameObject.GetComponent<T>();

                if(objectComponents != null)
                    components.Add(objectComponents);
            }

            return components;
        }
    }
}
