using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LDG.UI
{
    public abstract class WindowElement
    {
        public abstract Action OnClosed { get; }
        public abstract string Title { get; }
        public abstract Point Size { get; }
        public UIGroup Group { get; }

        public WindowElement()
        {
            var windowSize = Screen.Resolution;

            // Offset the window size to center in the screen 
            var windowPosition = new Point((windowSize.X - Size.X) / 2, (windowSize.Y - Size.Y) / 2);

            Group = new UIGroup()
            {
                Settings = new UIGroupSettings()
                {
                    Position = new Rectangle(windowPosition, Size),
                    ShowBox = true
                }
            };
        }

        public abstract void AddElements(UIGroup group);
    }
}
