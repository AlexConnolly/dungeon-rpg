using Homestead.Items;
using LDG.Sprite;

namespace Homestead.Abilities.Crafting.Items
{
    internal class Nails : BaseItem
    {
        public override string Name => "Nails";

        public override string Description => "For making things stick together.";

        public override SpriteFrame Icon => Icons.Nails;
    }
}
