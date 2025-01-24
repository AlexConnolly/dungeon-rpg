using Homestead.World;
using LDG;
using LDG.Audio;
using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Items
{
    internal abstract class PlaceableItem : BaseItem
    {
        public abstract Func<GameObject, WorldObject> OnPlace { get; }

        public override bool Action(Player player, WorldManager world)
        {
            // Get object infront 
            var objectInfront = player.GetObjectInfront();

            if(objectInfront == null)
            {
                var newObject = world.AddGameObject();

                world.AddWorldObjectAtWorldPosition(player.GetPositionInfront(), OnPlace(newObject));

                player.Inventory.RemoveActiveItem();

                return true;
            }

            return false;
        }
    }
}
