using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components
{
    public class Transform : GameComponent
    {
        public Transform(GameObject gameObject) : base(gameObject)
        {
        }

        public Vector2 Position { get; set; }

        public Vector2 Translate(Vector2 movement)
        {
            this.Position += movement;

            return this.Position;
        }
    }
}
