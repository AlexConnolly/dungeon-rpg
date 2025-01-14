using Homestead.Items;
using Homestead.World;
using LDG.Audio;
using LDG.Sprite;

namespace Homestead.Abilities.Woodcutting.Items
{
    internal class Axe : BaseItem
    {
        public override bool ConsumeOnAction => false;

        public override string Name => "Makeshift Axe";

        public override SpriteFrame Icon => Icons.Axe;

        public override AudioClip Sound => Sounds.Axe;

        public override bool Action(Player player, WorldManager world)
        {
            var objectInfront = player.GetObjectInfront();

            if (objectInfront == null)
            {
                return false;
            }

            if (objectInfront is TreeComponent)
            {
                var tree = objectInfront as TreeComponent;

                tree.Hit();

                return true;
            }

            return false;
        }
    }
}
