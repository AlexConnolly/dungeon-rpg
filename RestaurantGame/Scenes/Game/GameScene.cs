using LDG;
using Microsoft.Xna.Framework;

namespace RestaurantGame.Scenes.Game
{
    internal class GameScene : Scene
    {
        public override Color ClearColor => Color.LawnGreen;

        public override void Initialize()
        {
            var npc = AddGameObject().AddComponent<Npc>();
        }
    }
}
