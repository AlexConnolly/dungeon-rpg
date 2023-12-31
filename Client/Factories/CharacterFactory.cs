﻿using Client.Actor;
using Client.Actor.MortalActor;
using Client.HUD;
using Client.Items.Items;
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

        public required Action OnDeath { get; set; }
    }

    public class CharacterFactory
    {
        public static GameObject CreateCharacter(Scene scene, CreateCharacterRequest request)
        {
            var gameObject = scene.AddGameObject();

            var walkingAudio = gameObject.AddComponent<AudioSource>();

            walkingAudio.Sound = AudioManager.GetSound("character_footsteps");

            var walkingParticles = ParticleFactory.AddParticle(gameObject, ParticleType.Footsteps);

            var reachZone = gameObject.AddComponent<BoxTrigger>();

            reachZone.Bounds = new Rectangle(0, 0, 50, 50);

            var spriteAnimator = gameObject.AddComponent<SpriteMovementAnimator>();

            spriteAnimator.FramesPerSecond = request.MovementFPS;

            spriteAnimator.Sheet = request.MovementSheet;

            var actor = gameObject.AddComponent<Actor.MortalActorComponent>();

            //actor.WalkingAudio = walkingAudio;
            actor.WalkingParticles = walkingParticles;
            actor.ReachZone = reachZone;

            actor.Size = request.CollisionBounds;

            actor.CurrentHealth = 100;
            actor.MaximumHealth = 100;

            actor.MovementSpeed = request.MovementSpeed;

            actor.SpriteAnimator = spriteAnimator;

            actor.OnDeath = request.OnDeath;

            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

            var healthBar = gameObject.AddComponent<ActorHealthBar>();

            healthBar.Actor = actor;

            gameObject.GetComponent<Transform>().Position = request.StartPosition;

            var hand = gameObject.AddComponent<WieldedItem>();

            return gameObject;
        }
    }
}
