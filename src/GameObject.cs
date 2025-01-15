using LDG.Components;
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
        public int DrawPriority { get; set; } = 1;

        public string Tag { get; set; }

        public List<GameComponent> Components { get; set; } = new List<GameComponent>();

        public Scene Scene
        {
            get
            {
                return _scene;
            }
        }

        public T AddComponent<T>() where T : GameComponent
        {
            var component = Activator.CreateInstance<T>();

            component.GameObject = this;

            this.Components.Add(component);

            component.Load(Scene._contentManager, Scene._graphicsDevice);

            component.Initialize();

            return component;
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

        public Transform Transform
        {
            get
            {
                return GetComponent<Transform>();
            }
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

        private bool _enabled = true;

        public bool Enabled { 
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;

                foreach(var component in Components)
                {
                    component.Enabled = value;
                }
            }
        }
    }
}
