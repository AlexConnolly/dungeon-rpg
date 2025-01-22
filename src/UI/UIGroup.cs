using LDG.Drawing;
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
    public class UIGroup
    {
        public UIGroupSettings Settings { get; set; } = new UIGroupSettings() { Position = new Rectangle(10, 10, 200, 40), ShowBox = true };

        private List<UIElement> _elements = new List<UIElement>();

        public UIGroup()
        {
            UIManager.RegisterGroup(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Settings.ShowBox)
                spriteBatch.DrawSquare(this.Settings.Position, UIManager.Style.BackgroundColor, UIManager.Style.BorderColor, 4);

            foreach (var element in _elements)
            {
                element.Draw(spriteBatch, this);
            }
        }

        public void Update(TimeFrame time)
        {
            foreach(var element in _elements)
            {
                element.Update(time);
            }
        }

        private UIElement AddElement(UIElement element)
        {
            element.Group = this;
            _elements.Add(element);
            element.Initialize();

            return element;
        }

        public TextElement Text(TextElement element)
        {
            return (TextElement)AddElement(element);
        }

        public ButtonElement Button(ButtonElement element)
        {
            return (ButtonElement)AddElement(element);
        }

        public SpriteElement Image(SpriteFrame frame, Rectangle destination)
        {
            return (SpriteElement)AddElement(new SpriteElement(destination)
            {
                Frame = frame
            });
        }

        public SquareElement Square(Point location, Point size, Color background, Color border, int borderSize = 0)
        {
            var element = new SquareElement(new Rectangle(location, size))
            {
                Color = background,
                Border = border,
                BorderSize = borderSize
            };

            return (SquareElement)AddElement(element);
        }
    }
}
