using LDG;
using Microsoft.Xna.Framework;

namespace RestaurantGame.Scenes.Game
{
    internal class GameScene : Scene
    {
        public override Color ClearColor => Color.LawnGreen;

        public override void Initialize()
        {
            for(int x = 0; x < 100; x++)
            {
                var npc2 = AddGameObject().AddComponent<Npc>();

                npc2.Transform.Position = new Vector2(x * 15, npc2.Transform.Position.Y - (x * 15));
            }
        }
    }
}
