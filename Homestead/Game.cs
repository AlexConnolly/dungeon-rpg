using Homestead.World;
using LDG;
using LDG.Components.Actor;
using LDG.Components.Camera;
using LDG.Components.Player;
using LDG.Components.Sprite;
using LDG.Components.Tile;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead
{
    internal class Game : Scene
    {
        private WorldRenderer _worldRenderer;

        public override void Initialize()
        {
            _worldRenderer = AddGameObject().AddComponent<WorldRenderer>();

            // Preload sprites
            SpriteSheetManager.AddToCache("Icons", Spritesheet.FromSheet(Texture2D.FromFile(Graphics, "Media/Spritesheets/Icons.png"), new Point(24, 24)));
            SpriteSheetManager.AddToCache("WorldObjects", Spritesheet.FromSheet(Texture2D.FromFile(Graphics, "Media/Spritesheets/WorldObjects.png"), new Point(32, 32)));

            // Create the player
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            var playerObject = AddGameObject();

            var actor = playerObject.AddComponent<ActorComponent>();

            actor.MovementSpeed = 60;

            playerObject.AddComponent<PlayerController>();
            playerObject.AddComponent<MainCameraFollow>();

            var spriteRenderer = playerObject.AddComponent<SpriteRenderer>();

            var animator = playerObject.AddComponent<SpriteMovementAnimator>();

            actor.SpriteAnimator = animator;

            actor.SpriteAnimator.FramesPerSecond = 10;

            actor.SpriteAnimator.Sheet = SpriteSheetManager.GetSheetByName("characters_george");

            playerObject.AddComponent<Player>();
        }
    }
}
