﻿using LDG;
using LDG.Audio;
using LDG.Components.Sprite;
using LDG.Sprite;

namespace Homestead.World
{
    public abstract class WorldObject : GameComponent
    {
        private SpriteRenderer _spriteRenderer;

        public SpriteFrame Sprite { 
            set
            {
                if (_spriteRenderer == null)
                    _spriteRenderer = AddComponent<SpriteRenderer>();

                _spriteRenderer.Frame = value;
            }
        }

        public virtual AudioClip InteractionSound { get; }

        public virtual bool Interact(Player player, WorldManager world)
        {
            return false;
        }

        public abstract string InteractionName { get; }
    }
}
