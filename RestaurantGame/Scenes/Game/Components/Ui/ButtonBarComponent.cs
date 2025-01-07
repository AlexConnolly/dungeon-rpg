using LDG;
using LDG.Sprite;
using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameComponent = LDG.GameComponent;

namespace RestaurantGame.Scenes.Game.Components.Ui
{
    internal class ButtonBarComponent : GameComponent
    {
        private UIGroup _group;

        public override void Initialize()
        {
        }

        public override void Load(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            _group = GameObject.AddComponent<UIGroup>();

            int width = 180;
            int height = 50;

            int floating = 30;

            int startX = (Screen.Resolution.X / 2) - (width / 2);
            int startY = (Screen.Resolution.Y) - (height + floating);

            _group.Settings = new UIGroupSettings()
            {
                Position = new Microsoft.Xna.Framework.Rectangle(startX, startY, width, height),
                ShowBox = true
            };

            var iconSheet = Spritesheet.FromSheet(Texture2D.FromFile(graphicsDevice, "Scenes/Game/Artwork/Icons.png"), new Point(32, 32));

            var icon1 = _group.Button(new ButtonElement(new Microsoft.Xna.Framework.Rectangle(6, 6, 38, 38))
            {
                Text = "",
                Image = new ButtonImage()
                {
                    Image = iconSheet.GetByKey("0"),
                    Size = new Vector2(38, 38)
                }
            });

            var icon2 = _group.Button(new ButtonElement(new Microsoft.Xna.Framework.Rectangle(48, 6, 38, 38))
            {
                Text = "",
                Image = new ButtonImage()
                {
                    Image = iconSheet.GetByKey("1"),
                    Size = new Vector2(38, 38)
                }
            });

            var icon3 = _group.Button(new ButtonElement(new Microsoft.Xna.Framework.Rectangle(90, 6, 38, 38))
            {
                Text = "",
                Image = new ButtonImage()
                {
                    Image = iconSheet.GetByKey("2"),
                    Size = new Vector2(38, 38)
                }
            });

            var icon4 = _group.Button(new ButtonElement(new Microsoft.Xna.Framework.Rectangle(132, 6, 38, 38))
            {
                Text = "",
                Image = new ButtonImage()
                {
                    Image = iconSheet.GetByKey("3"),
                    Size = new Vector2(38, 38)
                }
            });
        }
    }
}
