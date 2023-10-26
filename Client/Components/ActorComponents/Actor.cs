using LDG;
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

namespace Client.Components.ActorComponents
{
    public class Actor : LDG.GameComponent
    {
        public AudioSource WalkingAudio { get; set; }
        public ParticleEngine WalkingParticles { get; set; }

        public SpriteMovementAnimator SpriteAnimator { get; set; }

        public BoxTrigger ReachZone { get; set; }

        private Vector2 _size = Vector2.Zero;

        public Vector2 Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
                Collider.Bounds = value;
            }
        }

        private BoxCollider Collider { get; set; }

        public Actor()
        {

        }

        private Direction _direction = Direction.Up;
        public Direction Direction
        {
            get
            {
                return this._direction;
            }

            set
            {
                this._direction = value;
                this.SpriteAnimator.Direction = value;
            }
        }

        private bool moving = false;

        public bool IsMoving
        {
            get
            {
                return moving;
            }

            set
            {
                moving = value;

                if (WalkingAudio != null)
                {
                    if (value)
                    {
                        if (!WalkingAudio.IsPlaying())
                        {
                            WalkingAudio.Start(true);
                        }
                    }
                    else
                    {
                        WalkingAudio.Stop();
                    }
                }

                if (WalkingParticles != null)
                {
                    if (value)
                    {
                        if (!WalkingParticles.Enabled)
                        {
                            WalkingParticles.Enabled = true;
                        }
                    }
                    else
                    {
                        WalkingParticles.Enabled = false;
                    }
                }

                this.SpriteAnimator.IsMoving = value;
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
                    velocity = Vector2Extensions.FromDirection(Direction);
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
            Collider = this.GameObject.AddComponent<BoxCollider>();
            Collider.Bounds = Size;
        }

        public override void Update(TimeFrame time)
        {
            // Move the reach trigger to face direction
            Vector2 offset = Vector2.Zero;

            switch (Direction)
            {
                case Direction.Left:
                    offset.X = -(Size.X / 2);
                    break;

                case Direction.Right:
                    offset.X = Size.X / 2;
                    break;

                case Direction.Up:
                    offset.Y = -Size.Y / 2;
                    break;

                case Direction.Down:
                    offset.Y = Size.Y / 2;
                    break;
            }

            ReachZone.Bounds = new Microsoft.Xna.Framework.Rectangle(
                offset.ToPoint(),
                ReachZone.Bounds.Size
            );

            // Handle movement
            Vector2 move = Velocity * time.Delta;

            if (move != Vector2.Zero)
            {
                this.Transform.Translate(move);
            }
        }
    }
}
