using LDG.Components;
using LDG.Sprite;

namespace Client.Items
{
    public abstract class Item
    {
        public abstract void Use(Actor consumer);

        public abstract SpriteFrame SpriteFrame { get; }

        public abstract string Name { get; set; }
        public abstract string Description { get; set; }

        public abstract void EnterHand(Actor consumer);
        public abstract void LeaveHand(Actor consumer);
    }
}
