using Homestead.Items;

namespace Homestead.Abilities.Crafting
{
    internal interface IBlueprint
    {
        string Name { get; }
        IEnumerable<KeyValuePair<IItem, int>> InputItems { get; }
        IEnumerable<KeyValuePair<IItem, int>> OutputItems { get; }
    }
}
