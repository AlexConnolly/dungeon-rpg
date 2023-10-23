using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public Spritesheet(Dictionary<string, SpriteFrame> frames)
        {
            Frames = frames;
        }

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

        public static Spritesheet FromSheet(Texture2D texture, Point size)
        {
            var frames = SpriteFrame.GetFramesFromSheet(texture, size.ToVector2());

            Dictionary<string, SpriteFrame> keyedFrames = new Dictionary<string, SpriteFrame>();

            for(int x = 0; x < frames.Count; x++)
            {
                keyedFrames.Add(x.ToString(), frames[x]);
            }

            return new Spritesheet(keyedFrames);
        }

        public static Spritesheet FromAnimatedSheet(Texture2D texture, bool isYDirection, int leftIndex, int rightIndex, int upIndex, int downIndex, Point size)
        {
            var getFramesFromSheet = SpriteFrame.GetRowsFromSheet(texture, size.ToVector2(), isYDirection);

            Dictionary<string, SpriteFrame> frames = new Dictionary<string, SpriteFrame>();

            for(int x = 0; x < getFramesFromSheet[leftIndex].Count; x++)
            {
                frames.Add("LEFT_" + x, getFramesFromSheet[leftIndex][x]);
            }

            for (int x = 0; x < getFramesFromSheet[rightIndex].Count; x++)
            {
                frames.Add("RIGHT_" + x, getFramesFromSheet[rightIndex][x]);
            }

            for (int x = 0; x < getFramesFromSheet[upIndex].Count; x++)
            {
                frames.Add("UP_" + x, getFramesFromSheet[upIndex][x]);
            }

            for (int x = 0; x < getFramesFromSheet[downIndex].Count; x++)
            {
                frames.Add("DOWN_" + x, getFramesFromSheet[downIndex][x]);
            }

            return new Spritesheet(frames);
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
            _sheets.Add("character_chicken", Spritesheet.FromAnimatedSheet(content.Load<Texture2D>("Graphics/Sprites/Characters/chicken"), false, 3, 1, 0, 2, new Point(32, 32)));
            _sheets.Add("character_george", Spritesheet.FromAnimatedSheet(content.Load<Texture2D>("Graphics/Sprites/Characters/george"), true, 1, 3, 2, 0, new Point(48, 48)));
        }
        
        public static Spritesheet GetSheetByName(string name)
        {
            if (_sheets.ContainsKey(name))
                return _sheets[name];

            return null;
        }
    }
}
