using Homestead.Abilities.Woodcutting.Items;
using Homestead.World;
using LDG;
using LDG.Components.Actor;
using LDG.Components.Audio;
using LDG.Components.Player;
using LDG.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Homestead
{
    public class Player : LDG.GameComponent
    {
        private WorldManager _worldManager;
        private ActorComponent _actor;
        private AudioSource _audioSource;

        public override void Initialize()
        {
            _worldManager = GameObject.Scene.GetAllComponentsOfType<WorldManager>().FirstOrDefault();
            _actor = GameObject.Scene.GetAllComponentsOfType<ActorComponent>().FirstOrDefault();
            _audioSource = GameObject.AddComponent<AudioSource>();

            _actor.Size = new Microsoft.Xna.Framework.Vector2(32, 32);
        }

        private Point GetRelativeFacingDirection()
        {
            switch(_actor.Direction)
            {
                case Direction.Left:
                    return new Point(-1, 0);

                case Direction.Right:
                    return new Point(1, 0);

                case Direction.Up:
                    return new Point(0, -1);

                case Direction.Down:
                    return new Point(0, 1);
            }

            return new Point(0, 0);
        }

        public WorldObject GetObjectInfront()
        {
            return _worldManager.GetWorldObjectAtWorldPosition(Camera.Position + (_actor.Size / 2), GetRelativeFacingDirection());
        }

        public override void Update(TimeFrame time)
        {
            if(KeyboardHelper.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                var axe = new Axe();

                if (axe.Action(this, _worldManager))
                {
                    _audioSource.Sound = axe.ActionSound.ToSoundEffect();

                    _audioSource.Start();
                }
            }
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {

        }
    }
}
