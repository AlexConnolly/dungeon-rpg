using Homestead.Abilities.Crafting.Windows;
using Homestead.Abilities.Woodcutting.Items;
using Homestead.Items;
using Homestead.World;
using LDG.Sprite;
using LDG.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Abilities.Crafting
{
    internal class CraftingBench : WorldObject
    {
        public override string InteractionName => "Craft";

        public override void Initialize()
        {
            Sprite = SpriteSheetManager.GetSheetByName("WorldObjects").GetByKey("1");
        }

        public override bool Interact(Player player, WorldManager world)
        {
            UIManager.SetWindow(new CraftingWindow());

            return true;
        }
    }
}
