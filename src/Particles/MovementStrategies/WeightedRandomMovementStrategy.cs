using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Particles.MovementStrategies
{
    public class WeightedRandomMovementStrategy : ParticleMovementStrategy
    {
        public Vector2 RelativeDirection { get; set; }

        public Vector2 Randomiser { get; set; } = new Vector2(1.3f, 0);

        public override Vector2 GetRelativeTargetDirection(Vector2 currentPosition, TimeFrame time)
        {
            bool negative = Random.Shared.Next(0, 10) < 6;

            float randomX = Random.Shared.NextSingle() * Randomiser.X;
            float randomY = Random.Shared.NextSingle() * Randomiser.Y;

            if(negative)
            {
                return this.RelativeDirection + new Vector2(-randomX, -randomY);
            } else
            {
                return this.RelativeDirection + new Vector2(randomX, randomY);
            }
        }
    }
}
