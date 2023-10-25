using LDG;
using LDG.Components.Camera;
using LDG.Components.Character;
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
    public record CreatePlayerRequest
    {
        public required CreateCharacterRequest CreateCharacterRequest { get; set; }
    }

    public static class PlayerFactory
    {
        public static GameObject CreatePlayer(Scene scene, CreatePlayerRequest request)
        {
            var character = CharacterFactory.CreateCharacter(scene, request.CreateCharacterRequest);

            character.AddComponent<CharacterController>();
            character.AddComponent<MainCameraFollow>();

            character.Tag = "Player";

            var particleEngine = character.AddComponent<ParticleEngine>();

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
                ParticlesPerSecond = 100,
                EmissionArea = new Rectangle(0, 0, Screen.Resolution.X, Screen.Resolution.Y)
            };

            return character;
        }
    }
}
