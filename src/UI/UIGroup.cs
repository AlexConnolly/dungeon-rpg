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
        private readonly UIGroupSettings _settings;

        private Color BackgroundColor = new Color(255, 224, 163);
        private Color BorderColor = new Color(214, 170, 82);

        public UIGroup()
        {
            throw new Exception("Do not create a UIGroup directly. Call BeginGroup.");
        }

        private UIGroup(UIGroupSettings settings)
        {
            this._settings = settings;
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
            spriteBatch.DrawSquare(this._settings.Position, BackgroundColor, BorderColor, 4);

        }
    }
}
