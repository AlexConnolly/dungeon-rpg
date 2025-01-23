using Homestead.World;
using LDG.Audio;
using LDG.Sprite;

namespace Homestead.Items
{
    internal abstract class BaseItem : IItem
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract SpriteFrame Icon { get; }

        public AudioClip Sound => null;

        public virtual bool Action(Player player, WorldManager world)
        {
            // Do nothing because a base item inherently does nothing when actioned
            return false;
        }
    }
}
