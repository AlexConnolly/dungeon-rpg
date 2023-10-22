using LDG.Extensions;
using LDG.Sprite;
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
            if(Settings.ShowBox)
                spriteBatch.DrawSquare(this.Settings.Position, UIManager.Style.BackgroundColor, UIManager.Style.BorderColor, 4);

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

        public void Image(SpriteFrame frame, Rectangle destination)
        {
            AddElement(new SpriteElement(this, destination)
            {
                Frame = frame
            });
        }

        public void Square(Point location, Point size, Color background, Color border, int borderSize = 0)
        {
            AddElement(new SquareElement(this, new Rectangle(location, size))
            {
                Color = background,
                Border = border,
                BorderSize = borderSize
            });
        }
    }
}
