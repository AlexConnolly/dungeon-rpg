using Homestead.Items;
using Homestead.World;
using LDG.Audio;
using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Abilities.Woodcutting.Items
{
    internal class Logs : BaseItem
    {
        public override bool ConsumeOnAction => false;

        public override string Name => "Logs";

        public override SpriteFrame Icon => Icons.Logs;

        public override AudioClip Sound => null;

        public override bool Action(Player player, WorldManager world)
        {
            return false;
        }
    }
}
