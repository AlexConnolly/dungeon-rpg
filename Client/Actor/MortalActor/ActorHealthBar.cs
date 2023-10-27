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
    public class ActorHealthBar : LDG.GameComponent
    {
        private UIGroup group;

        public MortalActorComponent Actor { get; set; }

        private Vector2 size = new Vector2(100, 10);

        private SquareElement square;

        public override void Initialize()
        {
            this.group = GameObject.AddComponent<UIGroup>();

            this.group.Settings = new UIGroupSettings()
            {
                Position = new Microsoft.Xna.Framework.Rectangle(this.Transform.Position.ToPoint(), new Point(100, 20)),
                ShowBox = true
            };

            this.square = this.group.Square(new Point(4, 4), this.size.ToPoint() - new Vector2(8 ,8).ToPoint(), Color.Green, Color.Green, 0);
        }

        public override void Update(TimeFrame time)
        {
            this.group.Settings.Position = new Rectangle(Camera.WorldPositionToCameraPoint(this.Transform.Position + new Vector2(-this.Actor.Size.X, -this.Actor.Size.Y)), this.size.ToPoint());

            this.square.Position = new Rectangle(this.square.Position.Location, new Point((int)(this.Actor.CurrentHealth / this.Actor.MaximumHealth) * 100, this.square.Position.Size.Y));
        }
    }
}
