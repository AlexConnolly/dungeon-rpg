using LDG;
using LDG.Components.Actor;
using LDG.Components.Sprite;
using LDG.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private Texture2D _texture;

        public override void Initialize()
        {
            _actor = GameObject.AddComponent<ActorComponent>();
            _actor.SpriteAnimator = GameObject.AddComponent<SpriteMovementAnimator>();
            var renderer = _actor.GameObject.AddComponent<SpriteRenderer>();

            _actor.SpriteAnimator.Sheet = Spritesheet.FromAnimatedSheet(_texture, false, 0, 0, 0, 1, new Microsoft.Xna.Framework.Point(24, 24));
        }

        public override void Load(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            _texture = Texture2D.FromFile(graphicsDevice, "Scenes/Game/Artwork/Npc.png");
        }

        public override void Update(TimeFrame time)
        {
            _actor.IsMoving = true;
            _actor.Direction = Direction.Up;
        }
    }
}
