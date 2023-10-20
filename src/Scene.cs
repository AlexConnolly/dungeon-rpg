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
        public List<GameObject> GameObjects { get; set; }

        public List<T> GetAllComponentsOfType<T>() where T : GameComponent
        {
            var components = new List<T>();

            foreach(var gameObject in GameObjects)
            {
                var objectComponents = gameObject.GetComponent<T>();

                components.Add(objectComponents);
            }

            return components;
        }
    }
}
