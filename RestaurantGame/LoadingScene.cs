using LDG;
using LDG.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantGame
{
    internal class LoadingScene : Scene
    {
        public override Color ClearColor => Color.Orange;

        private TextElement textElement = null;

        private string[] randomText = new string[] { "Now with more burgers!", "My bbbbb bbbbutton is bbbbroken", "More mayo is gooooooood for you", "Deep fry everything", "Hello world! (in 4k)"};

        private float nextTime = 3.0f;

        public override void Initialize()
        {
            var textObject = AddGameObject();

            var uiGroup = textObject.AddComponent<UIGroup>();

            uiGroup.Settings.ShowBox = false;

            uiGroup.Text(new TextElement(new Rectangle((Screen.Resolution.X / 2) - 125, (Screen.Resolution.Y / 2) - 100, 250, 100))
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Text = "Loading...",
                Font = UIManager.Style.ButtonFont
            });

            textElement = uiGroup.Text(new TextElement(new Rectangle((Screen.Resolution.X / 2) - 125, (Screen.Resolution.Y / 2) - 50, 250, 100))
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Text = randomText[Random.Shared.Next(0, randomText.Length - 1)],
                Font = UIManager.Style.ButtonFont,
                Color = Color.OrangeRed
            });
        }

        public override void Update(TimeFrame time)
        {
            nextTime -= time.Delta;

            if(nextTime <= 0)
            {
                textElement.Text = randomText[Random.Shared.Next(0, randomText.Length)];
                nextTime = 3.0f;
            }
        }
    }
}
