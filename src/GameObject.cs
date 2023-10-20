using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public class GameObject
    {
        private Scene _scene;

        public GameObject(Scene scene)
        {
            this._scene = scene;
        }

        /// <summary>
        /// The higher the priority the sooner the item will be drawn before others
        /// </summary>
        public int DrawPriority { get; set; } = 0;

        public List<GameComponent> Components { get; set; }

        public Scene Scene
        {
            get
            {
                return _scene;
            }
        }

        public T GetComponent<T>() where T : GameComponent
        {
            foreach(var component in Components)
            {
                if(component is T)
                {
                    return (T)component;
                }
            }

            return null;
        }

        public bool TryGetComponent<T>(out T obj) where T : GameComponent
        {
            foreach (var component in Components)
            {
                if (component is T)
                {
                    obj = (T)component;
                    return true;
                }
            }

            obj = null;

            return false;
        }
    }
}
