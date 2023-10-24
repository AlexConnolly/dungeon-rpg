using LDG.Components.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public class Scene
    {

        private List<GameObject> _gameObjects = new List<GameObject>();

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

        public GameObject AddGameobject(GameObject gameObject)
        {
            this._gameObjects.Add(gameObject);

            return gameObject;
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
