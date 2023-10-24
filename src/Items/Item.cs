using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Items
{
    public abstract class Item
    {
        public abstract void Use();

        public SpriteFrame SpriteFrame { get; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
