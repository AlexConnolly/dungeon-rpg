using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.World
{
    internal class DestructableWorldObject : WorldObject
    {
        public override string InteractionName => "Break";

        public int HitsToBreak = 3;

        private int hitsSoFar = 0;

        public override bool Interact(Player player, WorldManager world)
        {
            hitsSoFar++;

            if(hitsSoFar >= HitsToBreak)
            {
                // TODO: Remove item

                return true;
            }

            return false;
        }
    }
}
