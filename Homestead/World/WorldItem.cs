using Homestead.Items;
using LDG;
using LDG.Components.Audio;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Homestead.World
{
    internal class WorldItem : LDG.GameComponent
    {
        public BaseItem Item { get; set; }

        private SpriteFrame _backgroundFrame { get; set; }

        private AudioSource _audioSource;


        public override void Initialize()
        {
            _backgroundFrame = SpriteSheetManager.GetSheetByName("Icons").GetByKey("2");
            _audioSource = AddComponent<AudioSource>();

            _audioSource.Sound = Sounds.ItemDrop.Effect;

            _audioSource.Start();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = Camera.WorldPositionToCameraPoint(this.Transform.Position).ToVector2();

            _backgroundFrame.Draw(spriteBatch, position, Color.White);

            Item.Icon.Draw(spriteBatch, position, Color.White, (Item.Icon.Size * 1).ToPoint());
        }
    }
}
