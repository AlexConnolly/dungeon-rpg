using Homestead.Abilities.Crafting.Items;
using Homestead.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Abilities.Crafting.Recipe
{
    internal class WoodenWallRecipe : IRecipe
    {
        public List<IItem> Input => new List<IItem>()
        {
            new Planks(),
            new Planks(),
            new Planks()
        };

        public IItem Output => new WoodenWall();
    }
}
