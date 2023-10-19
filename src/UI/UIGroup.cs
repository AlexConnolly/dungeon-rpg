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

        private Color BackgroundColor = new Color(255, 224, 163);
        private Color BorderColor = new Color(214, 170, 82);

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

            Vector2 offset = new Vector2(this.Settings.Position.X, this.Settings.Position.Y);

            foreach(var element in _elements)
            {
                element.Draw(offset, spriteBatch);
            }
        }

        public void Text(TextElement options)
        {
            _elements.Add(options);
        }
    }
}
