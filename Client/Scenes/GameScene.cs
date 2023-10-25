using Client.Factories;
using LDG;
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
            // Create UI
            UIFactory.CreateGameUI(this);

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
            CharacterFactory.CreateCharacter(this, new CreateCharacterRequest()
            {
                CollisionBounds = new Microsoft.Xna.Framework.Vector2(40, 20),
                MovementFPS = 10,
                MovementSheet = SpriteSheetManager.GetSheetByName("characters_chicken"),
                MovementSpeed = 75,
                StartPosition = Vector2.Zero
            });

        }
    }
}
