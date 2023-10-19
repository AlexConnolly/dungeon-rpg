using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.UI
{
    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        Top,
        Middle,
        Bottom
    }

    public abstract class UIElement
    {
        public abstract void Draw(Vector2 offset, SpriteBatch spriteBatch);
        public abstract Vector2 ContentDimensions();

        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;

        public abstract Rectangle Position { get; set; }


        private int GetXOffset(int contentWidth)
        {
            switch(HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    return 0;

                case HorizontalAlignment.Center:
                    return (Position.Width / 2) - (contentWidth / 2);

                case HorizontalAlignment.Right:
                    return (Position.Width - contentWidth);
            }

            return 0;
        }

        private int GetYOffset(int contentHeight)
        {
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    return 0;

                case VerticalAlignment.Middle:
                    return (Position.Height / 2) - (contentHeight / 2);

                case VerticalAlignment.Bottom:
                    return (Position.Height - contentHeight);
            }

            return 0;
        }

        public Vector2 CalculateAlignmentOffset()
        {
            Vector2 contentSize = ContentDimensions();

            int x = int.Parse(contentSize.X.ToString());
            int y = int.Parse(contentSize.Y.ToString());

            return new Vector2(GetXOffset(x), GetYOffset(y));
        }
    }
}
