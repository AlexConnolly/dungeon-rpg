using Homestead.Abilities.Woodcutting.Items;
using Homestead.World;
using LDG;
using LDG.Audio;
using LDG.Components.Audio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Items
{
    internal class ItemSpawnerComponent : GameComponent
    {
        public IItem Item { get; set; }
        public int Amount { get; set; } = 1;
        public Point Area { get; set; } = new Point(50, 50);
        public float Delay { get; set; } = 1.0f;

        public float Offset { get; set; } = 0.2f;

        public AudioClip SpawnNoise { get; set; } = Sounds.ItemPickup;

        private AudioSource _audioSource;

        private int amountSpawned = 0;

        public override void Initialize()
        {
            _audioSource = AddComponent<AudioSource>();

            _audioSource.Sound = SpawnNoise.Effect;
        }

        public override void Update(TimeFrame time)
        {
            Delay -= time.Delta;

            if(Delay <= 0)
            {
                SpawnItem();

                amountSpawned++;

                if(amountSpawned == Amount)
                {
                    KillObject();
                } else
                {
                    Delay = Offset;
                }
            }
        }

        private void SpawnItem()
        {
            var itemObject = AddGameObject();

            var random = new System.Random();

            float randomX = (float)(random.NextDouble() * 100 - (Area.X));
            float randomY = (float)(random.NextDouble() * 100 - (Area.Y));

            itemObject.Transform.Position = this.Transform.Position + new Microsoft.Xna.Framework.Vector2(randomX, randomY);

            var worldItem = itemObject.AddComponent<WorldItem>();

            worldItem.Item = Item;

            _audioSource.Start();
        }

        private void KillObject()
        {
            GameObject.Scene.RemoveObject(this.GameObject);
        }
    }

    public static class SpawnerHelpers
    {
        public static void AddSpawner(this GameObject gameObject, IItem item, int amount, float delay, float offset)
        {
            var newObject = gameObject.Scene.AddGameObject();
            newObject.Transform.Position = gameObject.Transform.Position;

            var spawner = newObject.AddComponent<ItemSpawnerComponent>();

            spawner.Item = item;
            spawner.Amount = amount;
            spawner.Delay = delay;
            spawner.Offset = offset;
        }
    }
}
