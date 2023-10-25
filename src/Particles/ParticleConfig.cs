using Microsoft.Xna.Framework;

namespace LDG.Particles
{
    public class ParticleConfig
    {
        public Vector2 RelativeTarget { get; set; }

        public float StartSize { get; set; } = 1;
        public float EndSize { get; set; } = 5;

        public float TimeToLive { get; set; } = 5;
    }
}
