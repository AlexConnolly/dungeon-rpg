using LDG;
using LDG.Audio;
using LDG.Components;
using LDG.Components.Audio;
using LDG.Components.Camera;
using LDG.Components.Collision;
using LDG.Components.Particles;
using LDG.Components.Sprite;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Factories
{
    public record CreateCharacterRequest
    {
        public required float MovementSpeed { get; set; }

        public required Vector2 CollisionBounds { get; set; }

        public required int MovementFPS { get; set; } = 10;

        public required Spritesheet MovementSheet { get; set; }

        public required Vector2 StartPosition { get; set; } = Vector2.Zero;
    }

    public class CharacterFactory
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

        public static GameObject CreateCharacter(Scene scene, CreateCharacterRequest request)
        {
            var gameObject = scene.AddGameObject();

            var walkingAudio = gameObject.AddComponent<AudioSource>();

            walkingAudio.Sound = AudioManager.GetSound("character_footsteps");

            var walkingParticles = CreateWalkingParticles(gameObject);

            var actor = gameObject.AddComponent<Actor>();

            //actor.WalkingAudio = walkingAudio;
            actor.WalkingParticles = walkingParticles;

            actor.MovementSpeed = request.MovementSpeed;

            var collider = gameObject.AddComponent<BoxCollider>();

            collider.Bounds = request.CollisionBounds;

            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

            var spriteAnimator = gameObject.AddComponent<SpriteMovementAnimator>();

            spriteAnimator.FramesPerSecond = request.MovementFPS;

            spriteAnimator.Sheet = request.MovementSheet;

            gameObject.GetComponent<Transform>().Position = request.StartPosition;

            return gameObject;
        }
    }
}
