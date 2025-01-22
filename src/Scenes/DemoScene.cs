using LDG.UI;
using Microsoft.Xna.Framework;
using System;

namespace LDG.Scenes
{
    public class DemoScene : Scene
    {
        public override Color ClearColor => Color.DarkSlateGray;

        public override void Initialize()
        {
            var loading = AddGameObject();

            var group = new UIGroup();
            
            group.Text(new TextElement(new Rectangle((Screen.Resolution.X / 2 - (100)), (Screen.Resolution.Y / 2 - (20)), 200, 40))
            {
                Font = UIManager.Style.ButtonFont,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Text = "Loading..."
            });

            group.Button(new ButtonElement(new Rectangle((Screen.Resolution.X / 2 - (100)), (Screen.Resolution.Y / 2) + 40, 200, 40))
            {
                Text = "Click me",
                OnClick = () =>
                {
                    Console.WriteLine("Test");
                }
            });

            group.Settings.ShowBox = false;
        }
    }
}
