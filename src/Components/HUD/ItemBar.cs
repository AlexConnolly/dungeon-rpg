using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LDG.Components.HUD
{
    public class ItemBar : GameComponent
    {
        public ItemBar(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 size = new Vector2(460, 60);


            using (var group = UIGroup.BeginGroup(new UIGroupSettings()
            {
                Position = new Rectangle((int)(Screen.Resolution.X / 2) - (int)(size.X / 2), (int)Screen.Resolution.Y - (int)(size.Y) - 10, (int)size.X, (int)size.Y)
            }))
            {
               for(int x = 0; x < 9; x++)
                {
                    using (var child = UIGroup.BeginGroup(new UIGroupSettings()
                    {
                        Position = new Rectangle(group.Settings.Position.Location + new Point(10 + (x * 50), 10), new Point(40, 40))
                    }))
                    {

                    }
                }
            }
        }
    }
}
