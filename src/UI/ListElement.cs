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
    public class ListElement : UIElement
    {
        public ListElement(Rectangle position, List<ListItem> items) : base(position)
        {
            Items = items;
        }

        private List<ListItem> Items { get; }

        public Action<ListItem> OnItemClicked { get; set; }

        public override Vector2 ContentDimensions()
        {
            return new Vector2(this.Position.Width, this.Position.Height);
        }

        public override void Initialize()
        {
            Group.Square(new Point(this.Position.X, this.Position.Y), new Point(this.Position.Width, this.Position.Height), UIManager.Style.BackgroundColor, UIManager.Style.BorderColor, 4);

            int y = 10;
            int ySize = 50;

            foreach(var item in Items)
            {
                // Draw a square for each item, incremeneting the y by the size and 10 pixels
                var square = Group.Square(new Point(this.Position.X + 10, this.Position.Y + y), new Point(this.Position.Width - 20, ySize), UIManager.Style.BackgroundColorActive, UIManager.Style.BorderColor, 2);

                square.OnClick = () =>
                {
                    if(OnItemClicked != null)
                        OnItemClicked(item);
                };

                // Add title text
                Group.Text(new TextElement(new Rectangle(this.Position.X + 20, this.Position.Y + y + 5, this.Position.Width - 40, 20))
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Middle,
                    Text = item.Title,
                    Font = UIManager.Style.HeaderFont
                });

                // Add smaller Text
                Group.Text(new TextElement(new Rectangle(this.Position.X + 20, this.Position.Y + y + 25, this.Position.Width - 40, 20))
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Middle,
                    Text = item.Text,
                    Font = UIManager.Style.TextFont
                });

                y += ySize + 10;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, UIGroup group)
        {

        }
    }

    public class ListItem
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public SpriteFrame Icon { get; set; }

        public object Identifier { get; set; }
    }
}
