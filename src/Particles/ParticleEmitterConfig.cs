using Microsoft.Xna.Framework;

namespace LDG.Particles
{
    public class ParticleEmitterConfig
    {
        public Rectangle EmissionArea { get; set; }

        public float ParticlesPerSecond { get; set; }

        public ParticleConfig ParticleConfig { get; set; }

        public bool OneShot { get; set; }
    }
}
