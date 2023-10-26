using LDG.Components.Audio;
using LDG.Components.Collision;
using LDG.Components.Particles;
using LDG.Components.Sprite;
using LDG.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components
{
    public class Actor : GameComponent
    {
        public AudioSource WalkingAudio { get; set; }
        public ParticleEngine WalkingParticles { get; set; }

        public BoxTrigger ReachZone { get; set; }

        private Vector2 _size = Vector2.Zero;

        public Vector2 Size {
            get
            {
                return this._size;
            }

            set
            {
                this._size = value;
                this.Collider.Bounds = value;
            }
        }

        private BoxCollider Collider { get; set; }

        public Actor() {

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
                this.moving = value;

                if(this.WalkingAudio != null)
                {
                    if (value)
                    {
                        if (!this.WalkingAudio.IsPlaying())
                        {
                            this.WalkingAudio.Start(true);
                        }
                    }
                    else
                    {
                        this.WalkingAudio.Stop();
                    }
                }

                if(this.WalkingParticles != null)
                {
                    if(value)
                    {
                        if(!this.WalkingParticles.Enabled)
                        {
                            this.WalkingParticles.Enabled = true;
                        }
                    } else
                    {
                        this.WalkingParticles.Enabled = false;
                    }
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

        public override void Initialize()
        {
            this.Collider = this.GameObject.AddComponent<BoxCollider>();
            this.Collider.Bounds = this.Size;
        }

        public override void Update(TimeFrame time)
        {
            // Move the reach trigger to face direction
            Vector2 offset = Vector2.Zero;

            switch(this.Direction)
            {
                case Direction.Left:
                    offset.X = -(this.Size.X / 2);
                    break;

                case Direction.Right:
                    offset.X = (this.Size.X / 2);
                    break;

                case Direction.Up:
                    offset.Y = (-this.Size.Y / 2);
                    break;

                case Direction.Down:
                    offset.Y = (this.Size.Y / 2);
                    break;
            }

            this.ReachZone.Bounds = new Microsoft.Xna.Framework.Rectangle(
                offset.ToPoint(),
                this.ReachZone.Bounds.Size
            );

            // Handle movement
            Vector2 move = (Velocity * time.Delta);

            if(move != Vector2.Zero)
            {
                this.Transform.Translate(move);
            }
        }
    }
}
