using LDG;
using LDG.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Actor.MortalActor
{
    public class ActorHealthBar : ActorComponent
    {
        private UIGroup group;

        public override void Initialize()
        {
            this.group = GameObject.AddComponent<UIGroup>();

            this.group.Settings = new UIGroupSettings()
            {
                Position = new Microsoft.Xna.Framework.Rectangle(this.Transform.Position.ToPoint(), new Point(100, 20)),
                ShowBox = true
            };
        }

        public override void Update(TimeFrame time)
        {
            this.group.Settings.Position = new Rectangle(this.Transform.Position.ToPoint(), new Point(100, 20));
        }
    }
}
