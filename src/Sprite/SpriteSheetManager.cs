using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Sprite
{
    public class Spritesheet
    {
        private Dictionary<string, SpriteFrame> Frames { get; set; }

        public SpriteFrame GetByKey(string key)
        {
            if (Frames.ContainsKey(key))
                return Frames[key];

            return null;
        }

        public List<SpriteFrame> GetByStartsWith(string startsWith)
        {
            var returns = new List<SpriteFrame>();

            foreach(var key in Frames.Keys)
            {
                if(key.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase))
                {
                    returns.Add(Frames[key]);
                }
            }

            return returns;
        }
    }

    public class SpritesheetItem
    {
        public string Key { get; set; }
        public Point Location { get; set; }
    }

    public class SpritesheetDefinition
    {
        public Point SpriteSize { get; set; }

        public List<SpritesheetItem> Items { get; set; } = new List<SpritesheetItem>();

        /// <summary>
        /// The key for the content load for texture2d. (eg. /graphics/images/spritesheet)
        /// </summary>
        public string TextureKey { get; set; }
    }

    public static class SpriteSheetManager
    {
        private static Dictionary<string, Spritesheet> _sheets = new Dictionary<string, Spritesheet>();

        public static void Load(ContentManager content)
        {
            // Scan folder for spritesheets (.json format)

            // Load sheets (load sheet .json and the relevant texture through content)

            // ???
        }
        
        public static Spritesheet GetSheetByName(string name)
        {
            if (_sheets.ContainsKey(name))
                return _sheets[name];

            return null;
        }
    }
}
