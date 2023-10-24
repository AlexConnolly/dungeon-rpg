using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDG.Components.HUD
{
    public class HealthBar : GameComponent
    {
        public HealthBar()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point size = new Point(280, 20);

            int x = 10;
            int y = 10;

            using(var group = UIGroup.BeginGroup(new UIGroupSettings()
            {
                Position = new Rectangle(new Point(x, y), size)
            }))
            {
                group.Square(Point.Zero + new Point(4, 4), size - new Point(8, 8), Color.DarkRed, Color.IndianRed, 2);
            }

            using (var group = UIGroup.BeginGroup(new UIGroupSettings()
            {
                Position = new Rectangle(new Point(x, y + (size.Y + 10)), size)
            }))
            {
                group.Square(Point.Zero + new Point(4, 4), size - new Point(8, 8), Color.DarkBlue, Color.CornflowerBlue, 2);
            }
        }
    }
}
