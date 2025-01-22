using Homestead.World;
using LDG.Audio;
using LDG.Sprite;

namespace Homestead.Items
{
    public abstract class UsableItem : IItem
    {
        public abstract string Name { get; }

        public abstract SpriteFrame Icon { get; }

        public abstract bool Use(WorldObject target);

        public abstract AudioClip Sound { get; }

        public bool Action(Player player, WorldManager world)
        {
            var objectInfront = player.GetObjectInfront();

            return Use(objectInfront);
        }
    }
}
