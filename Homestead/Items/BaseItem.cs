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
        public abstract bool ConsumeOnAction { get; }

        public abstract string Name { get; }

        public abstract SpriteFrame Icon { get; }

        public abstract AudioClip Sound { get; }

        public AudioClipConfig ActionSound => new AudioClipConfig() { Clip = Sound, Pitch = new LDG.Range(0.65f, 1) };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="world"></param>
        /// <returns>Whether the item was consummed</returns>
        public abstract bool Action(Player player, WorldManager world);
    }
}
