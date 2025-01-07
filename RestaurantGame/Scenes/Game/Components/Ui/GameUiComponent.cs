using LDG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantGame.Scenes.Game.Components.Ui
{
    internal class GameUiComponent : GameComponent
    {
        public override void Initialize()
        {
            GameObject.AddComponent<ButtonBarComponent>();
        }
    }
}
