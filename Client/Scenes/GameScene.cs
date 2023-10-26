using Client.Components.ActorComponents;
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

            for(int x = 0; x < 1; x++)
            {
                var chicken = CharacterFactory.CreateCharacter(this, new CreateCharacterRequest()
                {
                    CollisionBounds = new Microsoft.Xna.Framework.Vector2(40, 20),
                    MovementFPS = 10,
                    MovementSheet = SpriteSheetManager.GetSheetByName("characters_chicken"),
                    MovementSpeed = 35,
                    StartPosition = new Microsoft.Xna.Framework.Vector2(Random.Shared.Next(-100, 100), Random.Shared.Next(-100, 100))
                });

                chicken.GetComponent<Actor>().IsMoving = true;
            }

            // Create UI
            UIFactory.CreateGameUI(this);
        }
    }
}
