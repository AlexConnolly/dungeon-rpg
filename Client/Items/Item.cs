using LDG.Sprite;

namespace Client.Items
{
    public abstract class Item
    {
        public abstract void Use();

        public SpriteFrame SpriteFrame { get; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
