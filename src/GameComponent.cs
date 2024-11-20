using LDG.Components;
using LDG.Drawing;
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
        public virtual bool Enabled { get; set; } = true;

        public virtual void Update(TimeFrame time)
        {

        }

        /// <summary>
        /// Late update purposesly has no timeframe to make sure you do not do anything time-based
        /// </summary>
        public virtual void LateUpdate()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void DrawDebug(SpriteBatch spriteBatch)
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
            foreach (var component in GameObject.Components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }

            return null;
        }

        public GameObject GameObject {
            get; internal set;
        }
    }
}
