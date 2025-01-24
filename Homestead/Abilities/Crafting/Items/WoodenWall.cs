using Homestead.Items;
using Homestead.World;
using LDG;
using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Abilities.Crafting.Items
{
    internal class WoodenWall : PlaceableItem
    {
        public override string Name => "Wooden Wall";

        public override string Description => "A sturdy wall made of wood.";

        public override SpriteFrame Icon => WorldSprites.WoodenWall;

        public override Func<GameObject, WorldObject> OnPlace => (gameObject) =>
        {
            var worldObject = gameObject.AddComponent<DestructableWorldObject>();

            worldObject.Sprite = WorldSprites.WoodenWall;

            return worldObject;
        };
    }
}
