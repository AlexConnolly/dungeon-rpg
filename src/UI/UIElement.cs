using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.UI
{
    public abstract class UIElement
    {
        public abstract void Draw(Vector2 offset, SpriteBatch spriteBatch);
    }
}
