﻿using LDG;
using LDG.Components.Character;
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

            character.Tag = "Player";

            return character;
        }
    }
}