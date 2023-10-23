using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDG.Components.HUD
{
    public class HealthBar : GameComponent
    {
        public HealthBar(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point size = new Point(340, 30);

            int x = (Screen.Resolution.X / 2) - (size.X / 2);
            int y = 10;

            using(var group = UIGroup.BeginGroup(new UIGroupSettings()
            {
                Position = new Rectangle(new Point(x, y), size)
            }))
            {
                group.Square(Point.Zero + new Point(4, 4), size - new Point(8, 8), Color.DarkRed, Color.IndianRed, 2);
            }
        }
    }
}
