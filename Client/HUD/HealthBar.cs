using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Client.HUD
{
    public class HealthBar : LDG.GameComponent
    {
        public HealthBar()
        {

        }

        private SquareElement healthBar;
        private SquareElement manaBar;

        public override void Initialize()
        {

            Point size = new Point(280, 20);

            int x = 10;
            int y = 10;

            var healthGroup = GameObject.AddComponent<UIGroup>();

            healthGroup.Settings.Position = new Rectangle(new Point(x, y), size);

            healthBar = healthGroup.Square(Point.Zero + new Point(4, 4), size - new Point(8, 8), Color.DarkRed, Color.IndianRed, 2);

            var manaGroup = GameObject.AddComponent<UIGroup>();

            manaGroup.Settings.Position = new Rectangle(new Point(x, y + size.Y + 10), size);

            manaBar = manaGroup.Square(Point.Zero + new Point(4, 4), size - new Point(8, 8), Color.DarkBlue, Color.CornflowerBlue, 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
