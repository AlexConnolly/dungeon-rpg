using LDG.Sprite;

namespace Client.Items
{
    public abstract class Item
    {
        public abstract void Use();

        public abstract SpriteFrame SpriteFrame { get; }

        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
    }
}
