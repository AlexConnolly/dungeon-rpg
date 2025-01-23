using Homestead.Items;
using LDG.Sprite;

namespace Homestead.Abilities.Woodcutting.Items
{
    internal class TreeSeed : BaseItem
    {
        public override string Name => "Tree Seed";

        public override SpriteFrame Icon => Icons.TreeSeed;

        public override string Description => "When planted, grows a tree";
    }
}
