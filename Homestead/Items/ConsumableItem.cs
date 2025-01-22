using Homestead.World;
using LDG.Audio;
using LDG.Sprite;

namespace Homestead.Items
{
    public abstract class ConsumableItem : IItem
    {
        public abstract string Name { get; }

        public abstract SpriteFrame Icon { get; }

        public abstract  AudioClip Sound { get; }

        public abstract bool Consume(Player player, WorldManager world);

        public bool Action(Player player, WorldManager world)
        {
            if(Consume(player, world))
            {
                // Remove item from inventory
                player.Inventory.RemoveActiveItem();

                return true;
            }

            return false;
        }
    }
}
