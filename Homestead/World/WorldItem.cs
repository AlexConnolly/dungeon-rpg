using Homestead.Items;
using LDG;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Homestead.World
{
    internal class WorldItem : LDG.GameComponent
    {
        public BaseItem Item { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Item.Icon.Draw(spriteBatch, Camera.WorldPositionToCameraPoint(this.Transform.Position).ToVector2(), Color.White, (Item.Icon.Size * 1).ToPoint());
        }
    }
}
