using LDG.Particles;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Particles
{
    public class ParticleEngine : GameComponent
    {
        public ParticleEmitterConfig Config { get; set; }

        private List<ParticleInstance> _particles = new List<ParticleInstance>();

        public bool Enabled { get; set; } = true;

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var particle in _particles)
            {
                particle.Draw(spriteBatch);
            }
        }

        private ParticleInstance Emit()
        {
            var particle = new ParticleInstance() { Config = this.Config.ParticleConfig };

            // Pick random place to spawn
            float randomX = Random.Shared.Next(-(this.Config.EmissionArea.Width / 2), this.Config.EmissionArea.Width / 2);
            float randomY = Random.Shared.Next(-(this.Config.EmissionArea.Height / 2), this.Config.EmissionArea.Height / 2);

            particle.Position = this.Transform.Position + new Microsoft.Xna.Framework.Vector2(randomX, randomY);

            return particle;
        }

        private float timeUntilNextEmit = 0;

        public override void Update(TimeFrame time)
        {

            // Determine next emission
            if(Enabled)
            {
                if (this.Config.OneShot)
                {
                    for (int x = 0; x < this.Config.ParticlesPerSecond; x++)
                    {
                        this._particles.Add(Emit());
                    }

                    this.Enabled = false;
                } else
                {
                    timeUntilNextEmit -= time.Delta;

                    if (timeUntilNextEmit <= 0)
                    {
                        this._particles.Add(Emit());

                        timeUntilNextEmit = 1.0f / this.Config.ParticlesPerSecond;
                    }
                }
            }

            // Update all
            List<ParticleInstance> toDelete = new List<ParticleInstance>();

            foreach(var particle in _particles)
            {
                if(particle.Update(time))
                {
                    toDelete.Add(particle);
                }
            }

            foreach(var particle in toDelete)
            {
                _particles.Remove(particle);
            }
        }
    }
}
