using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public class GameObject
    {
        public List<GameComponent> Components { get; set; }

        public List<string> Tags { get; set; }

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
    }
}
