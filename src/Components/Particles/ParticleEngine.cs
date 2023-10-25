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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private ParticleInstance Emit()
        {

        }

        private float timeUntilNextEmit = 0;

        public override void Update(TimeFrame time)
        {
            // Determine next emission
            timeUntilNextEmit -= time.Delta;

            if(timeUntilNextEmit <= 0)
            {
                this._particles.Add(Emit());

                timeUntilNextEmit = 1.0f / this.Config.ParticlesPerSecond;
            }
        }
    }
}
