using Homestead.Abilities.Crafting.Items;
using Homestead.Abilities.Woodcutting.Items;
using Homestead.Items;

namespace Homestead.Abilities.Crafting.Recipe
{
    internal class PlanksRecipe : IRecipe
    {

        public IItem Output => new Planks();

        public List<IItem> Input => new List<IItem>()
        {
            new Logs()
        };
    }
}
