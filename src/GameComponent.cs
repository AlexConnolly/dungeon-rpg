using LDG.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG
{
    public abstract class GameComponent
    {
        private readonly GameObject _gameObject;

        public GameComponent(GameObject gameObject)
        {
            this._gameObject = gameObject;
        }

        public virtual void Update(TimeFrame time)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public Transform Transform { 
            get
            {
                return this.GameObject.GetComponent<Transform>();
            }
        }

        public T GetComponent<T>() where T : GameComponent
        {
            foreach (var component in _gameObject.Components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }

            return null;
        }

        public GameObject GameObject { 
            get
            {
                return _gameObject;
            }
        }
    }
}
