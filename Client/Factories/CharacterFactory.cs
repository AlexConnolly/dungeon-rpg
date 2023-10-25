using LDG;
using LDG.Components;
using LDG.Components.Collision;
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
        public static GameObject CreateCharacter(Scene scene, CreateCharacterRequest request)
        {
            var gameObject = scene.AddGameObject();

            var actor = gameObject.AddComponent<Actor>();

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
