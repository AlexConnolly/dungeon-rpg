using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Particles.MovementStrategies
{
    public class DirectionMovementStrategy : ParticleMovementStrategy
    {
        public required Vector2 RelativeDirection { get; set; }

        public override Vector2 GetRelativeTargetDirection(Vector2 currentPosition)
        {
            return this.RelativeDirection;
        }
    }
}
