using Homestead.World;
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
        public override bool Action(Player player, WorldManager world)
        {
            // Get object infront 
            var objectInfront = player.GetObjectInfront();

            if(objectInfront == null)
            {
                world.
            }
        }
    }
}
