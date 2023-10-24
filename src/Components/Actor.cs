using LDG.Components.Audio;
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
        private readonly AudioSource audioSource;
        public Actor(GameObject gameObject, AudioSource movementAudio) : base(gameObject) {
            this.audioSource = movementAudio;
        }

        public Direction Direction { get; set; } = Direction.Up;

        private bool moving = false;

        public bool IsMoving {
            get
            {
                return this.moving;
            }

            set
            {
                // Don't set the value if it's the same as before
                if (value == this.moving)
                    return;

                this.moving = value;

                if(this.moving)
                {
                    this.audioSource.Start();
                    this.audioSource.Loop();
                } else
                {
                    this.audioSource.Stop();
                }
            }
        }

        public float MovementSpeed { get; set; } = 20.0f;
        private float currentSpeed = 0.0f; // Current speed of the object
        private float accelerationRate = 0.25f; // Determines the rate of acceleration towards max speed

        private Vector2 Velocity
        {
            get
            {
                Vector2 velocity = Vector2.Zero;

                if (IsMoving)
                {
                    velocity = Vector2Extensions.FromDirection(this.Direction);
                    currentSpeed = MathHelper.Lerp(currentSpeed, MovementSpeed, accelerationRate); // Lerp towards the desired MovementSpeed using MathHelper
                }
                else
                {
                    currentSpeed = 0.0f; // Reset the current speed if not moving
                }

                return velocity * currentSpeed; // Use currentSpeed here instead of MovementSpeed
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
