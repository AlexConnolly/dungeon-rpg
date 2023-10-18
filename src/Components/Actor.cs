using LDG.Components.Sprite;
using LDG.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components
{
    internal class Actor : GameComponent
    {
        public Actor(GameObject gameObject) : base(gameObject) { }

        public Direction Direction { get; set; } = Direction.Up;

        public bool IsMoving { get; set; }

        public float MovementSpeed { get; set; } = 20.0f;

        public Vector2 Velocity {
            get
            {
                Vector2 velocity = Vector2.Zero;

                if (IsMoving)
                    velocity = Vector2Extensions.FromDirection(this.Direction);

                return velocity * this.MovementSpeed;
            }
        }

        public override void Update(TimeFrame time)
        {
            // Handle movement
            Vector2 move = (Velocity * time.Delta);

            this.Transform.Translate(move);
        }
    }
}
