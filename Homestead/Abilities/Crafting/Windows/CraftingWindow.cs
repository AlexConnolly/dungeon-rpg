using LDG.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Abilities.Crafting.Windows
{
    internal class CraftingWindow : WindowElement
    {
        public override Action OnClosed => () =>
        {

        };

        public override string Title => "Crafting";

        public override Point Size => new Point(600, 400);

        public override void AddElements(UIGroup group)
        {
            group.Button(new ButtonElement(new Rectangle(10, 10, 140, 40), "Test button"));
        }
    }
}
