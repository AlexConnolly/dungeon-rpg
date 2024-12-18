﻿using Client.Actor;
using Client.Items.Items;
using LDG;
using LDG.Components.Actor;
using LDG.Components.Camera;
using LDG.Components.Collision;
using LDG.Components.Particles;
using LDG.Components.Player;
using LDG.Particles.MovementStrategies;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

            character.AddComponent<PlayerController>();
            character.AddComponent<MainCameraFollow>();

            new StoneSword().EnterHand((ActorComponent)character.GetComponent<ActorComponent>());

            character.Tag = "Player";

            return character;
        }
    }
}
