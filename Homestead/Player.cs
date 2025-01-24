using Homestead.Abilities.Woodcutting.Items;
using Homestead.Items;
using Homestead.World;
using LDG;
using LDG.Audio;
using LDG.Components.Actor;
using LDG.Components.Audio;
using LDG.Components.Player;
using LDG.Input;
using LDG.UI;
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
        private AudioSource _walkingAudio;
        internal InventoryComponent Inventory { get; private set; }

        public static Player GetInstance()
        {
            return SceneManager.CurrentScene.GetAllComponentsOfType<Player>().First();
        }


        public override void Initialize()
        {
            _worldManager = GameObject.Scene.GetAllComponentsOfType<WorldManager>().FirstOrDefault();
            _actor = GameObject.Scene.GetAllComponentsOfType<ActorComponent>().FirstOrDefault();
            _audioSource = GameObject.AddComponent<AudioSource>();
            _walkingAudio = AddComponent<AudioSource>();

            _walkingAudio.Sound = Sounds.Walking.Effect;

            Inventory = AddComponent<InventoryComponent>();

            _actor.Size = new Microsoft.Xna.Framework.Vector2(25, 20);
            _actor.CollisionOffset = new Microsoft.Xna.Framework.Vector2(0, 10);

            _actor.WalkingAudio = _walkingAudio;
        }

        public void PlaySound(AudioClip clip)
        {
            _audioSource.Sound = clip.Effect;
            _audioSource.Start();
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
            return _worldManager.GetWorldObjectAtWorldPosition(GetPositionInfront());
        }

        public Microsoft.Xna.Framework.Vector2 GetPositionInfront()
        {
            var relativePosition = GetRelativeFacingDirection();

            return (Camera.Position + (_actor.Size / 2)) + new Microsoft.Xna.Framework.Vector2(relativePosition.X * _worldManager.ChunkResolution, relativePosition.Y * _worldManager.ChunkResolution);
        }

        private bool addedAxe = false;

        public override void Update(TimeFrame time)
        {
            if(!addedAxe)
            {
                Inventory.AddItem<Axe>();
                addedAxe = true;
            }

            if(KeyboardHelper.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                var usedItem = Inventory.ActionItemInHand();

                if (!usedItem)
                {
                    // See if we can interact with the world item instead
                    var worldObject = GetObjectInfront();

                    if (worldObject != null)
                    {
                        var didInterract = worldObject.Interact(this, _worldManager);

                        if(didInterract)
                        {
                            if(worldObject.InteractionSound != null)
                            {
                                _audioSource.Sound = worldObject.InteractionSound.Effect;
                                _audioSource.Start();
                            }
                        }
                    }
                }
            }

            if(KeyboardHelper.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                // Spawn object infront of player 
                var relativePosition = GetRelativeFacingDirection();

                var position = GetPositionInfront();

                GameObject.AddSpawner(Inventory.GetActiveItem(), 1, 0.5f, 0);

                Inventory.RemoveActiveItem();
            }

            if(_actor.IsMoving)
            {
                UIManager.SetWindow(null);
            }
        }

        public override void DrawDebug(SpriteBatch spriteBatch)
        {

        }
    }
}
