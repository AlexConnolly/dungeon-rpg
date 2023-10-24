using Client.Components.HUD;
using LDG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Factories
{
    public class UIFactory
    {
        public static GameObject CreateGameUI(Scene scene)
        {
            var obj = scene.AddGameObject();

            var itemBar = obj.AddComponent<ItemBar>();
            var healthBar = obj.AddComponent<HealthBar>();

            return obj;
        }
    }
}
