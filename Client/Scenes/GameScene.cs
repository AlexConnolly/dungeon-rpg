using Client.Factories;
using LDG;
using LDG.Components;
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

            // Create chicken
            for(int x = 0; x < 30; x++)
            {
                int bounds = 500;

                var character = CharacterFactory.CreateCharacter(this, new CreateCharacterRequest()
                {
                    CollisionBounds = new Microsoft.Xna.Framework.Vector2(40, 20),
                    MovementFPS = 10,
                    MovementSheet = SpriteSheetManager.GetSheetByName("characters_chicken"),
                    MovementSpeed = 30,
                    StartPosition = new Vector2(Random.Shared.Next(-bounds, bounds), Random.Shared.Next(-bounds, bounds))
                });

                character.GetComponent<Actor>().IsMoving = true;
            }
            

            // Create UI
            UIFactory.CreateGameUI(this);
        }
    }
}
