using LDG;
using LDG.Components.Particles;
using LDG.Particles.MovementStrategies;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Factories
{
    public enum ParticleType
    {
        Fire,
        Footsteps,
        Rain
    }

    public static class ParticleFactory
    {

        private static ParticleEngine CreateWalkingParticles(GameObject gameObject)
        {
            var particles = gameObject.AddComponent<ParticleEngine>();

            particles.Config = new LDG.Particles.ParticleEmitterConfig()
            {
                EmissionArea = new Rectangle(new Point(0, 20), new Point(20, 10)),
                ParticleConfig = new LDG.Particles.ParticleConfig()
                {
                    MovementStrategy = null,
                    StartOpacity = 0.8f,
                    EndOpacity = 0,
                    StartSize = 5,
                    EndSize = 5,
                    StartSpeed = 0,
                    TimeToLive = 15,
                    EndSpeed = 0,
                    Frame = SpriteSheetManager.GetSheetByName("tiles_world").GetByKey("58")
                },
                ParticlesPerSecond = 5f
            };

            return particles;
        }

        //private static ParticleEngine CreateFireParticles(GameObject gameObject)
        //{
        //    var particleObject = gameObject;

        //    var particleEngine = particleObject.AddComponent<ParticleEngine>();

        //    particleEngine.Config = new LDG.Particles.ParticleEmitterConfig()
        //    {
        //        EmissionArea = new Rectangle(0, -50, 50, 20),
        //        ParticlesPerSecond = 10,
        //        ParticleConfig = new LDG.Particles.ParticleConfig()
        //        {
        //            StartOpacity = 1,
        //            EndOpacity = 0,

        //            StartSize = 50,
        //            EndSize = 70,

        //            StartSpeed = 30,
        //            EndSpeed = 70,

        //            Frame = SpriteSheetManager.GetSheetByName("particles_smoke").GetByKey("0"),

        //            MovementStrategy = new WeightedRandomMovementStrategy()
        //            {
        //                RelativeDirection = new Vector2(0, -1)
        //            },

        //            Color = Color.DarkGray,

        //            TimeToLive = 10
        //        }
        //    };

        //    var particleSparks = particleObject.AddComponent<ParticleEngine>();

        //    particleSparks.Config = new LDG.Particles.ParticleEmitterConfig()
        //    {
        //        EmissionArea = new Rectangle(0, -5, 20, 2),
        //        ParticlesPerSecond = 30,
        //        ParticleConfig = new LDG.Particles.ParticleConfig()
        //        {
        //            StartOpacity = 1,
        //            EndOpacity = 0,

        //            StartSize = 5,
        //            EndSize = 5,

        //            StartSpeed = 100,
        //            EndSpeed = 100,

        //            Frame = SpriteSheetManager.GetSheetByName("particles_smoke").GetByKey("0"),

        //            MovementStrategy = new WeightedRandomMovementStrategy()
        //            {
        //                RelativeDirection = new Vector2(0, -1)
        //            },

        //            Color = Color.Orange,

        //            TimeToLive = 2
        //        }
        //    };


        //    var particleFire = particleObject.AddComponent<ParticleEngine>();

        //    particleFire.Config = new LDG.Particles.ParticleEmitterConfig()
        //    {
        //        EmissionArea = new Rectangle(0, 0, 20, 2),
        //        ParticlesPerSecond = 20,
        //        ParticleConfig = new LDG.Particles.ParticleConfig()
        //        {
        //            StartOpacity = 1,
        //            EndOpacity = 0,

        //            StartSize = 20,
        //            EndSize = 30,

        //            StartSpeed = 30,
        //            EndSpeed = 40,

        //            Frame = SpriteSheetManager.GetSheetByName("particles_smoke").GetByKey("0"),

        //            MovementStrategy = new WeightedRandomMovementStrategy()
        //            {
        //                RelativeDirection = new Vector2(0, -1)
        //            },

        //            Color = Color.Orange,

        //            TimeToLive = 3
        //        }
        //    };
        //}

        private static ParticleEngine CreateRainParticles(GameObject gameObject)
        {
            var particleEngine = gameObject.AddComponent<ParticleEngine>();

            particleEngine.Config = new LDG.Particles.ParticleEmitterConfig()
            {
                ParticleConfig = new LDG.Particles.ParticleConfig()
                {
                    MovementStrategy = new DirectionMovementStrategy() { RelativeDirection = new Vector2(1, 2) },
                    StartSize = 10,
                    EndSize = 10,
                    StartSpeed = 20,
                    EndSpeed = 100,
                    TimeToLive = 5,
                    Frame = SpriteSheetManager.GetSheetByName("tiles_world").GetByKey("29")
                },
                ParticlesPerSecond = 50,
                EmissionArea = new Rectangle(0, 0, Screen.Resolution.X, Screen.Resolution.Y)
            };

            return particleEngine;
        }

        public static ParticleEngine AddParticle(GameObject gameObject, ParticleType particleType)
        {
            switch(particleType)
            {
                case ParticleType.Footsteps:
                    return CreateWalkingParticles(gameObject);

                //case ParticleType.Fire:
                //    return CreateFireParticles(gameObject);

                case ParticleType.Rain:
                    return CreateRainParticles(gameObject);

            }

            return null;
        }
    }
}
