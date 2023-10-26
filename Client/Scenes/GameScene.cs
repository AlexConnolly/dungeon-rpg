using Client.Factories;
using LDG;
using LDG.Components;
using LDG.Components.Particles;
using LDG.Particles.MovementStrategies;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Scenes
{
    public class GameScene : Scene
    {
        public override Color ClearColor => Color.Snow;

        public GameScene()
        {

        }

        public override void Initialize()
        {
            // Create world
            WorldRendererFactory.CreateWorldRenderer(this);

            // Create player
            PlayerFactory.CreatePlayer(this, new CreatePlayerRequest()
            {
                CreateCharacterRequest = new CreateCharacterRequest()
                {
                    CollisionBounds = new Microsoft.Xna.Framework.Vector2(40, 20),
                    MovementFPS = 10,
                    MovementSheet = SpriteSheetManager.GetSheetByName("characters_george"),
                    MovementSpeed = 75,
                    StartPosition = new Microsoft.Xna.Framework.Vector2(-100, 10)
                }
            });

            for(int x = 0; x < 10; x++)
            {
                var chicken = CharacterFactory.CreateCharacter(this, new CreateCharacterRequest()
                {
                    CollisionBounds = new Microsoft.Xna.Framework.Vector2(40, 20),
                    MovementFPS = 10,
                    MovementSheet = SpriteSheetManager.GetSheetByName("characters_chicken"),
                    MovementSpeed = 35,
                    StartPosition = new Microsoft.Xna.Framework.Vector2(Random.Shared.Next(-1000, 1000), Random.Shared.Next(-1000, 1000))
                });

                chicken.GetComponent<Actor>().IsMoving = true;
            }

            var particleObject = this.AddGameObject();

            var particleEngine = particleObject.AddComponent<ParticleEngine>();

            particleEngine.Config = new LDG.Particles.ParticleEmitterConfig()
            {
                EmissionArea = new Rectangle(0, -50, 50, 20),
                ParticlesPerSecond = 10,
                ParticleConfig = new LDG.Particles.ParticleConfig()
                {
                    StartOpacity = 1,
                    EndOpacity = 0,

                    StartSize = 50,
                    EndSize = 70,

                    StartSpeed = 30,
                    EndSpeed = 70,

                    Frame = SpriteSheetManager.GetSheetByName("particles_smoke").GetByKey("0"),

                    MovementStrategy = new WeightedRandomMovementStrategy() { 
                        RelativeDirection = new Vector2(0, -1)
                    },

                    Color = Color.DarkGray,

                    TimeToLive = 10
                }
            };

            var particleSparks = particleObject.AddComponent<ParticleEngine>();

            particleSparks.Config = new LDG.Particles.ParticleEmitterConfig()
            {
                EmissionArea = new Rectangle(0, -5, 20, 2),
                ParticlesPerSecond = 30,
                ParticleConfig = new LDG.Particles.ParticleConfig()
                {
                    StartOpacity = 1,
                    EndOpacity = 0,

                    StartSize = 5,
                    EndSize = 5,

                    StartSpeed = 100,
                    EndSpeed = 100,

                    Frame = SpriteSheetManager.GetSheetByName("particles_smoke").GetByKey("0"),

                    MovementStrategy = new WeightedRandomMovementStrategy()
                    {
                        RelativeDirection = new Vector2(0, -1)
                    },

                    Color = Color.Orange,

                    TimeToLive = 2
                }
            };



            var particleFire = particleObject.AddComponent<ParticleEngine>();

            particleFire.Config = new LDG.Particles.ParticleEmitterConfig()
            {
                EmissionArea = new Rectangle(0, 0, 20, 2),
                ParticlesPerSecond = 20,
                ParticleConfig = new LDG.Particles.ParticleConfig()
                {
                    StartOpacity = 1,
                    EndOpacity = 0,

                    StartSize = 20,
                    EndSize = 30,

                    StartSpeed = 30,
                    EndSpeed = 40,

                    Frame = SpriteSheetManager.GetSheetByName("particles_smoke").GetByKey("0"),

                    MovementStrategy = new WeightedRandomMovementStrategy()
                    {
                        RelativeDirection = new Vector2(0, -1)
                    },

                    Color = Color.Orange,

                    TimeToLive = 3
                }
            };


            // Create UI
            UIFactory.CreateGameUI(this);
        }
    }
}
