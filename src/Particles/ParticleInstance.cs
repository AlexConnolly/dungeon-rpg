using LDG.Extensions;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Particles
{
    public class ParticleInstance
    {
        public required ParticleConfig Config { get; set; }

        public Vector2 Position { get; set; }

        public bool Update(TimeFrame time)
        {
            timeAlive += time.Delta;

            float percentageComplete = (timeAlive / this.Config.TimeToLive);

            currentSpeed = CalculateCurrentSpeed(percentageComplete);
            currentSize = CalculateCurrentSize(percentageComplete);
            currentOpacity = GetValueBetween(Config.StartOpacity, Config.EndOpacity, percentageComplete);

            if(this.Config.MovementStrategy != null)
                this.Position += (this.Config.MovementStrategy.GetRelativeTargetDirection(this.Position, time) * (time.Delta * currentSpeed));

            return percentageComplete >= 1.0f;
        }

        private float CalculateCurrentSize(float percentage)
        {
            float difference = this.Config.EndSize - this.Config.StartSize;

            return this.Config.StartSize + (difference * percentage);
        }

        private float CalculateCurrentSpeed(float percentage)
        {
            float difference = this.Config.EndSpeed - this.Config.StartSpeed;

            return this.Config.StartSpeed + (difference * percentage);
        }

        private float GetValueBetween(float startValue, float endValue, float percentage)
        {
            float difference = endValue - startValue;

            return startValue + (difference * percentage);
        }

        private float currentSpeed = 0;
        private float currentSize = 0;
        private float currentOpacity = 0;

        private float timeAlive = 0;

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawPosition = this.Position;

            this.Config.Frame.Draw(spriteBatch, LDG.Camera.WorldPositionToCameraPoint(drawPosition).ToVector2(), this.Config.Color, new Point((int)this.currentSize, (int)this.currentSize), this.currentOpacity);
        }
    }
}
