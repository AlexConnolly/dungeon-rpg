using Homestead.Items;
using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Abilities.Crafting.Items
{
    internal class WoodenWall : BaseItem
    {
        public override string Name => "Wooden Wall";

        public override string Description => "A sturdy wall made of wood.";

        public override SpriteFrame Icon => Icons.WoodenWall;
    }
}
