using Client.Actor;
using LDG.Components;
using LDG.Sprite;

namespace Client.Items
{
    public abstract class Item
    {
        public abstract void Use(ActorComponent consumer);

        public abstract SpriteFrame SpriteFrame { get; }

        public abstract string Name { get; set; }
        public abstract string Description { get; set; }

        public abstract void EnterHand(ActorComponent consumer);
        public abstract void LeaveHand(ActorComponent consumer);
    }
}
