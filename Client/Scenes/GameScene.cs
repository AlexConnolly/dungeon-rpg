using Client.Factories;
using LDG;
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
            // Create tilemap
            UIFactory.CreateGameUI(this);
        }
    }
}
