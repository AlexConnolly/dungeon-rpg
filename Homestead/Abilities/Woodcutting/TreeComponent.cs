using Homestead.Abilities.Woodcutting.Items;
using Homestead.World;
using LDG.Components.Particles;
using LDG.Particles.MovementStrategies;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using System.Numerics;

namespace Homestead.Abilities.Woodcutting
{
    internal class TreeComponent : WorldObject
    {
        public int HitsLeft = 3;

        private ParticleEngine _hitEmitter;

        public override void Initialize()
        {
            _hitEmitter = GameObject.AddComponent<ParticleEngine>();

            _hitEmitter.Config = new LDG.Particles.ParticleEmitterConfig()
            {
                ParticleConfig = new LDG.Particles.ParticleConfig()
                {
                    Frame = SpriteSheetManager.GetSheetByName("Particles").GetByKey("0"),
                    MovementStrategy = new WeightedRandomMovementStrategy() { RelativeDirection = new Microsoft.Xna.Framework.Vector2(0, 20) },
                    StartSize = 15,
                    EndSize = 5,
                    TimeToLive = 0.5f,
                    StartSpeed = 5,
                    EndSpeed = 3,
                    StartOpacity = 1,
                    EndOpacity = 1,
                    Color = Color.White
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
            
            Sprite = SpriteSheetManager.GetSheetByName("WorldObjects").GetByKey("0");

            // Important
            base.Initialize();
        }

        public void Hit()
        {
            if(HitsLeft != 0)
            {
                HitsLeft--;

                _hitEmitter.Enabled = true;

                if (HitsLeft == 0)
                {
                    var itemObject = AddGameObject();

                    itemObject.Transform.Position = this.Transform.Position;

                    var worldItem = itemObject.AddComponent<WorldItem>();

                    worldItem.Item = new Logs();

                    Sprite = SpriteSheetManager.GetSheetByName("WorldObjects").GetByKey("1");
                }
            }
        }

        public void Shake()
        {

        }
    }
}
