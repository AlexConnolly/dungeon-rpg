using LDG.UI;

namespace LDG.Scenes
{
    public class DemoScene : Scene
    {
        public override void Initialize()
        {
            var loading = AddGameObject();

            var group = loading.AddComponent<UIGroup>();
            
            group.Text(new TextElement(new Microsoft.Xna.Framework.Rectangle(0, 0, 200, 40))
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
