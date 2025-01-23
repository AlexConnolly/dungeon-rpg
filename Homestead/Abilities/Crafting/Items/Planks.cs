using Homestead.Items;
using LDG.Sprite;

namespace Homestead.Abilities.Crafting.Items
{
    internal class Planks : BaseItem
    {
        public override string Name => "Planks";

        public override string Description => "Flat, wooden boards. Great for building things.";

        public override SpriteFrame Icon => Icons.Planks;
    }
}
