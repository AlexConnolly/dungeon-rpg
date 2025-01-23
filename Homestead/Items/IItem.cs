using Homestead.World;
using LDG.Audio;
using LDG.Sprite;

namespace Homestead.Items
{
    public interface IItem
    {
        public string Name { get; }
        public string Description { get; }
        public SpriteFrame Icon { get; }

        public AudioClip Sound { get; }

        public bool Action(Player player, WorldManager world);
    }
}
