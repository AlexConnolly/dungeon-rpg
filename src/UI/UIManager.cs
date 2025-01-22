using Microsoft.Xna.Framework;
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
    public static class UIManager
    {
        public static SpriteBatch CurrentSpriteBatch
        {
            get; private set;
        }

        private static List<UIGroup> _groups = new List<UIGroup>();

        public static UIStyle Style { get; set; }

        private static WindowElement _window;

        public static void Load(SpriteBatch spriteBatch, ContentManager Content)
        {
            UIManager.CurrentSpriteBatch = spriteBatch;

            // Load sprite sheets
            var defaultFont = new FontConfig()
            {
                Font = Content.Load<SpriteFont>("Fonts/Large"),
                Shadow = new FontShadowConfig()
                {
                    Color = Color.Black
                }
            };

            UIManager.Style = new UIStyle()
            {
                BackgroundColor = new Color(255, 224, 163),
                BorderColor = new Color(61, 52, 22),
                BorderColorActive = new Color(173, 62, 75),
                
                ButtonFont = defaultFont,
                HeaderFont = defaultFont,
                TextFont = defaultFont
            };
        }

        public static void RegisterGroup(UIGroup group)
        {
            _groups.Add(group);
        }

        public static void RemoveGroup(UIGroup group)
        {
            _groups.Remove(group);
        }

        private static void ClearGroups()
        {
            _groups = new List<UIGroup>();
        }

        public static void Update(TimeFrame frame)
        {
            foreach (var element in _groups)
            {
                if (element != _window?.Group)
                    element.Update(frame);
            }

            if (_window != null)
            {
                _window.Group.Update(frame);
            }
        }

        public static void Draw()
        {
            foreach(var group in UIManager._groups)
            {
                if(group != _window?.Group)
                    group.Draw(UIManager.CurrentSpriteBatch);
            }

            if(_window != null)
            {
                _window.Group.Draw(UIManager.CurrentSpriteBatch);
            }
        }

        public static void SetWindow(WindowElement element)
        {
            if(_window != null)
            {
                if(_window.OnClosed != null)
                {
                    _window.OnClosed();
                    _groups.Remove(_window.Group);
                }
            }

            if(element != null)
                element.AddElements(element.Group);

            _window = element;
        }
    }
}
