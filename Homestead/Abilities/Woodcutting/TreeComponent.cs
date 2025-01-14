using Homestead.World;
using LDG.Components.Particles;
using LDG.Particles.MovementStrategies;
using LDG.Sprite;
using System.Numerics;

namespace Homestead.Abilities.Woodcutting
{
    internal class TreeComponent : WorldObject
    {
        public int HitsLeft = 3;

        public override SpriteFrame Sprite => SpriteSheetManager.GetSheetByName("WorldObjects").GetByKey("0");

        private ParticleEngine _hitEmitter;

        public override void Initialize()
        {
            _hitEmitter = GameObject.AddComponent<ParticleEngine>();

            _hitEmitter.Config = new LDG.Particles.ParticleEmitterConfig()
            {
                ParticleConfig = new LDG.Particles.ParticleConfig()
                {
                    Frame = SpriteSheetManager.GetSheetByName("WorldObjects").GetByKey("0"),
                    MovementStrategy = new DirectionMovementStrategy() { RelativeDirection = new Vector2(0, -20) },
                    StartSize = 20,
                    EndSize = 30
                },
                OneShot = true,
                EmissionArea = new Microsoft.Xna.Framework.Rectangle()
                {
                    X = 0,
                    Y = 0,
                    Width = 50,
                    Height = 10
                },
                ParticlesPerSecond = 10
            };

            _hitEmitter.Enabled = false;

            // Important
            base.Initialize();
        }

        public void Hit()
        {
            HitsLeft--;

            _hitEmitter.Enabled = true;

            if(HitsLeft == 0)
            {

            }
        }

        public void Shake()
        {

        }
    }
}
