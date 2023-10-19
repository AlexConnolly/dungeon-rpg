using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LDG.UI
{
    internal static class UIManager
    {
        public static SpriteBatch CurrentSpriteBatch
        {
            get; private set;
        }

        public static SpriteFont LargeFont
        {
            get; private set;
        }

        private static List<UIGroup> _groups = new List<UIGroup>();

        public static void Load(SpriteBatch spriteBatch, ContentManager Content)
        {
            UIManager.CurrentSpriteBatch = spriteBatch;

            // Load sprite sheets
            LargeFont = Content.Load<SpriteFont>("Fonts/Large");

            // Do anything else?
        }

        internal static void RegisterGroup(UIGroup group)
        {
            _groups.Add(group);
        }

        private static void ClearGroups()
        {
            _groups = new List<UIGroup>();
        }

        public static void Update(TimeFrame frame)
        {
            UIManager.ClearGroups();
        }

        public static void Draw()
        {
            foreach(var group in UIManager._groups)
            {
                group.Draw(UIManager.CurrentSpriteBatch);
            }
        }
    }
}
