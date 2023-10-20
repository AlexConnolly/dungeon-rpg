using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Collision
{
    public abstract class Collider : GameComponent
    {
        protected Collider(GameObject gameObject) : base(gameObject)
        {
        }

        public abstract bool Intersects(Rectangle rectangle);
    }
}
