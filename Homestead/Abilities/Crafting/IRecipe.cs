using Homestead.Items;

namespace Homestead.Abilities.Crafting
{
    public interface IRecipe
    {
        public List<IItem> Input { get; }
        public IItem Output { get; }
    }
}
