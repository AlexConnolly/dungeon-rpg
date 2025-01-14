using Homestead.World;
using LDG.Audio;
using LDG.Sprite;

namespace Homestead.Items
{
    public interface IItem
    {
        public bool Action(Player player, WorldManager world);
        public bool ConsumeOnAction { get; }
        public string Name { get; }
        public SpriteFrame Icon { get; }

        public AudioClipConfig ActionSound { get; }
    }
}
