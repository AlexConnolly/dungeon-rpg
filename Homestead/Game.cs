using Homestead.World;
using LDG;
using LDG.Components.Tile;
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
        }
    }
}
