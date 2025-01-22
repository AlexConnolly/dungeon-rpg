using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LDG.Components.Engine
{
    public class FpsComponent : GameComponent
    {
        private UI.TextElement textElement;

        public override void Initialize()
        {
            var uiGroup = new UIGroup();

            textElement = uiGroup.Text(new TextElement(new Rectangle(10, 10, 250, 100))
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Text = "FPS component",
                Font = UIManager.Style.ButtonFont
            });
        }

        public override void Update(TimeFrame time)
        {
            this.textElement.Text = (1.0f / time.Delta).ToString() + " FPS";
        }
    }
}
