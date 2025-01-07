using LDG.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Camera
{
    public class CameraMover : GameComponent
    {
        private Vector2 velocity;
        private Vector2 acceleration;
        private float maxSpeed;
        private float accelerationRate;
        private float decelerationRate;
        private float stopThreshold = 1.0f;

        public CameraMover()
        {
            this.velocity = Vector2.Zero;
            this.acceleration = Vector2.Zero;
            this.maxSpeed = 200;
            this.accelerationRate = 300;
            this.decelerationRate = 500;
        }

        public override void Update(TimeFrame gameTime)
        {
            Vector2 input = Vector2.Zero;

            // Use KeyboardHelper for input handling
            if (KeyboardHelper.IsKeyDown(Keys.W))
                input.Y -= 1;
            if (KeyboardHelper.IsKeyDown(Keys.S))
                input.Y += 1;
            if (KeyboardHelper.IsKeyDown(Keys.A))
                input.X -= 1;
            if (KeyboardHelper.IsKeyDown(Keys.D))
                input.X += 1;

            if (input.LengthSquared() > 0)
            {
                input.Normalize();
                acceleration = input * accelerationRate * (float)gameTime.Delta;
            }
            else
            {
                // Gradual deceleration
                acceleration = -velocity * decelerationRate * (float)gameTime.Delta;
            }

            // Update velocity
            velocity += acceleration;

            // Clamp velocity to max speed
            if (velocity.Length() > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }

            // Stop small residual velocity
            if (velocity.LengthSquared() < stopThreshold * stopThreshold)
            {
                velocity = Vector2.Zero;
            }

            // Update camera position
            LDG.Camera.Position += velocity * (float)gameTime.Delta;
        }
    }
}
