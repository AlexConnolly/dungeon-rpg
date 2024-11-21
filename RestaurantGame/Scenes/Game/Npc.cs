using LDG;
using LDG.Components.Actor;
using LDG.Components.Sprite;
using LDG.Sprite;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantGame.Scenes.Game
{
    public class Npc : GameComponent
    {
        private ActorComponent _actor;

        public override void Initialize()
        {
            _actor = GameObject.AddComponent<ActorComponent>();

            _actor.SpriteAnimator = GameObject.AddComponent<SpriteMovementAnimator>();

            //_actor.SpriteAnimator.Sheet = Spritesheet.FromAnimatedSheet()
        }

        public override void Load(ContentManager contentManager)
        {

        }

        public override void Update(TimeFrame time)
        {
            _actor.IsMoving = true;
            _actor.Direction = Direction.Up;
        }
    }
}
