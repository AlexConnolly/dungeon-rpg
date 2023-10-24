using LDG.UI;
using Microsoft.Xna.Framework;

namespace LDG.Scenes
{
    public class DemoScene : Scene
    {
        public override Color ClearColor => Color.DarkSlateBlue;

        public override void Initialize()
        {
            var loading = AddGameObject();

            var group = loading.AddComponent<UIGroup>();
            
            group.Text(new TextElement(new Rectangle((Screen.Resolution.X / 2 - (100)), (Screen.Resolution.Y / 2 - (20)), 200, 40))
            {
                Font = UIManager.Style.ButtonFont,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Text = "Loading..."
            });

            group.Settings.ShowBox = false;
        }
    }
}
