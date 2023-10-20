using LDG.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.UI
{
    public class UIGroup : IDisposable
    {
        public readonly UIGroupSettings Settings;

        public Color BackgroundColor = new Color(255, 224, 163);
        public Color BorderColor = new Color(214, 170, 82);

        private List<UIElement> _elements = new List<UIElement>();

        public UIGroup()
        {
            throw new Exception("Do not create a UIGroup directly. Call BeginGroup.");
        }

        private UIGroup(UIGroupSettings settings)
        {
            this.Settings = settings;
        }

        public static UIGroup BeginGroup(UIGroupSettings settings)
        {
            var newGroup = new UIGroup(settings);

            UIManager.RegisterGroup(newGroup);

            return newGroup;
        }

        public void Dispose()
        {

        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawSquare(this.Settings.Position, BackgroundColor, BorderColor, 4);

            foreach (var element in _elements)
            {
                element.Draw(spriteBatch, this);
            }
        }

        private void AddElement(UIElement element)
        {
            _elements.Add(element);
            element.Initialize();
        }

        public void Text(TextElement element)
        {
            AddElement(element);
        }

        public void Button(ButtonElement element)
        {
            AddElement(element);
        }
    }
}
