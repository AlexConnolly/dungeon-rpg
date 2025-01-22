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
    internal abstract class BaseItem : IItem
    {
        public abstract string Name { get; }

        public abstract SpriteFrame Icon { get; }

        public AudioClip Sound => null;

        public bool Action(Player player, WorldManager world)
        {
            // Do nothing because a base item inherently does nothing when actioned
            return false;
        }
    }
}
