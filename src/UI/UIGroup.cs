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
    public class UIGroup : GameComponent
    {
        public UIGroupSettings Settings { get; set; } = new UIGroupSettings() { Position = new Rectangle(10, 10, 200, 40), ShowBox = true };

        private List<UIElement> _elements = new List<UIElement>();

        public UIGroup()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(Settings.ShowBox)
                spriteBatch.DrawSquare(this.Settings.Position, UIManager.Style.BackgroundColor, UIManager.Style.BorderColor, 4);

            foreach (var element in _elements)
            {
                element.Draw(spriteBatch, this);
            }
        }

        public override void Update(TimeFrame time)
        {
            foreach(var element in _elements)
            {
                element.Update(time);
            }
        }

        private void AddElement(UIElement element)
        {
            element.Group = this;
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
            AddElement(new SpriteElement(destination)
            {
                Frame = frame
            });
        }

        public void Square(Point location, Point size, Color background, Color border, int borderSize = 0)
        {
            AddElement(new SquareElement(new Rectangle(location, size))
            {
                Color = background,
                Border = border,
                BorderSize = borderSize
            });
        }
    }
}
