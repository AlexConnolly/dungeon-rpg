using LDG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components
{
    public class Inventory : GameComponent
    {
        public Inventory(GameObject gameObject)
        {

        }

        public List<Item> Items { get; set; }
    }
}
