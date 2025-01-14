using LDG;
using LDG.Components.Sprite;
using LDG.Sprite;

namespace Homestead.World
{
    public abstract class WorldObject : GameComponent
    {
        private SpriteRenderer _spriteRenderer;

        public override void Initialize()
        {
            _spriteRenderer = GameObject.AddComponent<SpriteRenderer>();
            _spriteRenderer.Frame = this.Sprite;
        }

        public abstract SpriteFrame Sprite { get; }
    }
}
