using LDG.Sprite;
using Microsoft.Xna.Framework;

namespace LDG.Particles
{
    public abstract class ParticleMovementStrategy
    {
        /// <summary>
        /// Should return the relative Vector2 for the target direction of this particle
        /// </summary>
        /// <returns></returns>
        public abstract Vector2 GetRelativeTargetDirection(Vector2 currentPosition, TimeFrame time);
    }

    public class ParticleConfig
    {
        public ParticleMovementStrategy MovementStrategy { get; set; }

        public SpriteFrame Frame { get; set; }

        public float StartSize { get; set; } = 1;
        public float EndSize { get; set; } = 5;

        public float StartSpeed { get; set; } = 0;
        public float EndSpeed { get; set; } = 5;

        public float StartOpacity { get; set; } = 1;
        public float EndOpacity { get; set; } = 0;

        public Color Color { get; set; } = Color.White;

        public float TimeToLive { get; set; } = 5;
    }
}
