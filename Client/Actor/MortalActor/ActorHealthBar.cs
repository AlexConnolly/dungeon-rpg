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

        private float showTimeRemaining = 0;
        private float previousHealth = -1;

        private float timeToHide = 6f;

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
            float currentHealth = this.Actor.CurrentHealth;

            if(previousHealth != -1 && currentHealth != previousHealth)
            {
                // Set timer
                if(showTimeRemaining <= 0)
                {
                    showTimeRemaining = timeToHide;
                }
            }

            if(showTimeRemaining > 0)
            {
                showTimeRemaining -= time.Delta;

                this.group.Enabled = true;
            } else
            {
                this.group.Enabled = false;
            }

            this.group.Settings.Position = new Rectangle(Camera.WorldPositionToCameraPoint(this.Transform.Position + new Vector2(-(this.Actor.Size.X / 2) - 30, -this.Actor.Size.Y - 10)), this.size.ToPoint());

            int width = (int)((this.Actor.CurrentHealth / this.Actor.MaximumHealth) * 100);

            this.square.Position = new Rectangle(this.square.Position.Location, new Point(width - 8, this.square.Position.Size.Y));

            previousHealth = currentHealth;
        }
    }
}
