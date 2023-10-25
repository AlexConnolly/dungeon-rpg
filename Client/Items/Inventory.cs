using Client.Items.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Items
{
    public static class Inventory
    {
        public static List<Item> Items { get; set; } = new List<Item>()
        {
            new StoneSword()
        };
    }
}
