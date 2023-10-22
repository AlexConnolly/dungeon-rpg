using LDG.Extensions;
using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LDG.Components.HUD
{
    public class ItemBar : GameComponent
    {
        public ItemBar(GameObject gameObject) : base(gameObject)
        {
        }

        private int CurrentIndex = 0;

        private int scrollValue = Mouse.GetState().ScrollWheelValue;

        public override void Update(TimeFrame time)
        {
            int newScroll = Mouse.GetState().ScrollWheelValue;

            if(newScroll < scrollValue)
            {
                CurrentIndex++;
            }

            if (newScroll > scrollValue)
            {
                CurrentIndex--;
            }

            if (CurrentIndex < 0)
            {
                CurrentIndex = 8;
            } 

            if(CurrentIndex > 8)
            {
                CurrentIndex = 0;
            }

            scrollValue = newScroll;
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
                    group.Button(new ButtonElement(group, new Rectangle(new Point(10 + (x * 50), 10), new Point(40, 40)))
                    {
                        Text = "",
                        ForceHover = x == CurrentIndex
                    });

                    if(x == CurrentIndex)
                    {
                        group.Square(new Point(10 + (x * 50), 10), new Point(40, 40), Color.Transparent, Color.Red, 4);
                    }
                }
            }

            // Get player component
            var actor = GetComponent<Actor>();

            Vector2 position = Vector2.Zero;

            switch (actor.Direction)
            {
                case Direction.Up:
                    position = new Vector2(0, -1);
                    break;

                case Direction.Down:
                    position = new Vector2(0, 1);
                    break;

                case Direction.Left:
                    position = new Vector2(-1, 0);
                    break;

                case Direction.Right:
                    position = new Vector2(1, 0);
                    break;
            }

            using (var group = UIGroup.BeginGroup(new UIGroupSettings()
            {
                Position = new Rectangle(LDG.Camera.WorldPositionToCameraPoint(this.Transform.Position) - new Point(24, 24), new Point(48, 48)),
                ShowBox = false
            }))
            {

            }
        }
    }
}
