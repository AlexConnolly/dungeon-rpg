using LDG.Components;
using LDG.Components.Collision;
using Microsoft.Xna.Framework;
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
    public class Scene
    {
        private List<GameObject> _gameObjects = new List<GameObject>();

        private static Scene _currentScene = null;

        internal ContentManager _contentManager;
        internal GraphicsDevice _graphicsDevice;

        public GraphicsDevice Graphics { get
            {
                return _graphicsDevice;
            } }

        public IEnumerable<GameObject> GameObjects
        {
            get
            {
                // Use to avoid any adjustments to the GOs
                var tempList = new List<GameObject>(_gameObjects);

                foreach (var obj in tempList)
                {
                    yield return obj;
                }
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(TimeFrame time)
        {

        }

        public virtual void Load(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {

        }

        public virtual Color ClearColor { get; } = Color.CornflowerBlue;

        public GameObject AddGameObject(Vector2 position)
        {
            var obj = AddGameObject();

            obj.Transform.Position = position;

            return obj;
        }

        public GameObject AddGameObject()
        {
            var gameObject = new GameObject(this);

            gameObject.AddComponent<Transform>();

            this._gameObjects.Add(gameObject);

            return gameObject;
        }

        public void RemoveObject(GameObject obj)
        {
            if (this._gameObjects.Contains(obj))
                this._gameObjects.Remove(obj);
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

        public List<DestinationType> GetAllComponentsOfTypeAs<SourceType, DestinationType>() 
            where SourceType : GameComponent 
            where DestinationType : GameComponent
        {
            var components = new List<DestinationType>();

            foreach (var gameObject in _gameObjects)
            {
                var objectComponents = gameObject.GetComponent<SourceType>();

                if (objectComponents != null)
                    components.Add(objectComponents as DestinationType);
            }

            return components;
        }

        public List<T> GetAllComponentsOfType<T>() where T : GameComponent
        {
            return GetAllComponentsOfTypeAs<T, T>();
        }
    }
}
