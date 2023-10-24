using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Camera
{
    public class MainCameraFollow : GameComponent
    {
        public MainCameraFollow()
        {
        }

        public override void Update(TimeFrame time)
        {
            float lerpSpeed = 5.0f; // This determines how fast the camera will follow the player. Adjust as needed.
            float lerpFactor = 1.0f - (float)Math.Exp(-lerpSpeed * time.Delta); // Exponential smoothing factor

            // Linearly interpolate between the current camera position and the target position
            Vector2 newPosition = Vector2.Lerp(LDG.Camera.Position, this.Transform.Position, lerpFactor);

            LDG.Camera.Position = newPosition;
        }
    }
}
