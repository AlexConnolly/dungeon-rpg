using Homestead.Items;
using LDG;
using LDG.Components.Audio;
using LDG.Components.Collision;
using LDG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Homestead.World
{
    internal class WorldItem : LDG.GameComponent
    {
        public IItem Item { get; set; }

        private SpriteFrame _backgroundFrame { get; set; }

        private AudioSource _audioSource;

        private BoxTrigger _trigger;

        private Player _player;

        private AudioSource _pickupSource;


        public override void Initialize()
        {
            _backgroundFrame = SpriteSheetManager.GetSheetByName("Icons").GetByKey("2");

            _player = GameObject.Scene.GetAllComponentsOfType<Player>().First();

            _trigger = AddComponent<BoxTrigger>();

            _trigger.Bounds = new Rectangle(0, 0, 24, 24);

            _pickupSource = AddComponent<AudioSource>();

            _pickupSource.Sound = Sounds.ItemPickup.Effect;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = Camera.WorldPositionToCameraPoint(this.Transform.Position).ToVector2();

            _backgroundFrame.Draw(spriteBatch, position, Color.White);

            Item.Icon.Draw(spriteBatch, position, Color.White, (Item.Icon.Size * 1).ToPoint());
        }

        public override void Update(TimeFrame time)
        {
            if (_trigger.IntersectingObjects.Contains(_player.GameObject))
            {
                _pickupSource.Start();

                _player.Inventory.AddItem(this.Item);

                GameObject.Scene.RemoveObject(this.GameObject);
            }
        }
    }
}
