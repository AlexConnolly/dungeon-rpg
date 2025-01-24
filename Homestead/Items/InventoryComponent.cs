using Homestead.Abilities.Woodcutting.Items;
using Homestead.World;
using LDG;
using LDG.Audio;
using LDG.Components.Audio;
using LDG.Input;
using LDG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homestead.Items
{
    internal class InventoryComponent : LDG.GameComponent
    {
        private readonly int _width = 410;
        private readonly int _height = 50;

        private IItem[] _items = new IItem[9];

        private int _activeIndex = 0;

        private UIGroup _uiGroup;

        private List<ButtonElement> _buttons = new List<ButtonElement>();

        private AudioSource _audioSource;

        private WorldManager _worldManager;
        private Player _player;

        private AudioSource _clickSource;

        public IItem GetActiveItem()
        {
            return _items[_activeIndex];
        }

        public bool HasItems(params IItem[] items)
        {
            // Create a copy of the current items to avoid modifying the original list
            var availableItems = new List<IItem>(_items);

            foreach (var item in items)
            {
                var itemType = item.GetType();

                bool foundItem = false;

                for (int x = 0; x < availableItems.Count; x++)
                {
                    if(availableItems[x] == null)
                    {
                        continue;
                    }

                    if (_items[x].GetType() == itemType)
                    {
                        foundItem = true;
                        availableItems.RemoveAt(x);
                        break;
                    }
                }

                if (!foundItem)
                    return false;
            }

            // All items matched
            return true;
        }

        public void RemoveItems(params IItem[] items)
        {
            foreach (var item in items)
            {
                var itemType = item.GetType();

                for(int x = 0; x < _items.Length; x++)
                {
                    if(_items[x] == null)
                    {
                        continue;
                    }

                    if (_items[x].GetType() == itemType)
                    {
                        _items[x] = null;
                        break;
                    }
                }
            }
        }

        public void RemoveActiveItem()
        {
            if (_items[_activeIndex] != null)
            {
                _items[_activeIndex] = null;
            }
        }

        public bool HasActiveItem()
        {
            return GetActiveItem() != null;
        }

        public IEnumerable<IItem> GetItems()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }

        public bool AddItem(IItem item)
        {
            // Find empty slot in _items
            for(int i = 0; i < 9; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;

                    Player.GetInstance().PlaySound(Sounds.ItemPickup);

                    return true;
                }
            }

            return false;
        }

        public T AddItem<T>() where T : IItem
        {
            var instance = (IItem)Activator.CreateInstance(typeof(T));
            
            AddItem(instance);

            return (T)instance;
        }

        public bool ActionItemInHand()
        {
            // If our should always be greater than our active index
            var activeItem = GetActiveItem();

            if (activeItem == null)
                return false;

            if(activeItem != null)
            {
                if (activeItem.Action(_player, _worldManager))
                {
                    if(activeItem.Sound != null)
                    {
                        var audioClipConfig = new AudioClipConfig()
                        {
                            Clip = activeItem.Sound,
                            Pitch = new LDG.Range(-0.1f, 0.1f)
                        };

                        _audioSource.Sound = audioClipConfig.ToSoundEffect();

                        _audioSource.Start();
                    }

                    return true;
                }
            }

            return false;
        }

        private void SetActiveIndex(int index)
        {
            // Make sure that we dont overflow
            if (index < 0)
                index = 8;

            if (index > 8)
                index = 0;

            _buttons[_activeIndex].IsActive = false;

            _activeIndex = index;

            _buttons[_activeIndex].IsActive = true;

            // Only play sound if there is an item
            if (_items[index] != null)
                _clickSource.Start();
        }

        public override void Initialize()
        {
            _audioSource = AddComponent<AudioSource>();
            _clickSource = AddComponent<AudioSource>();

            _clickSource.Sound = Sounds.ItemEquip.Effect;

            _player = GameObject.Scene.GetAllComponentsOfType<Player>().First();
            _worldManager = GameObject.Scene.GetAllComponentsOfType<WorldManager>().First();

            GameObject.DrawPriority = -1;

            IsUi = true;

            _uiGroup = new UIGroup();

            _uiGroup.Settings = new UIGroupSettings()
            {
                Position = new Microsoft.Xna.Framework.Rectangle()
                {
                    X = (Screen.Resolution.X / 2) - (_width / 2),
                    Y = Screen.Resolution.Y - (_height + 20),
                    Width = _width,
                    Height = _height
                }
            };

            for(int x = 0; x < 9; x++)
            {
                int actualIndex = x;

                int xLocation = (x * 45) + 5;

                var button = _uiGroup.Button(new ButtonElement(new Rectangle(xLocation, 5, 40, 40), ""));

                button.OnClick = () =>
                {
                    SetActiveIndex(actualIndex);
                };

                if(actualIndex == 0)
                {
                    button.IsActive = true;
                }

                // TODO - add active item hover

                _buttons.Add(button);
            }

        }

        public override void Update(TimeFrame time)
        {
            for(int x = 0; x < 9; x++)
            {
                if (x == 9)
                    break;

                // Set null if we don't have an item
                if(_items[x] == null)
                {
                    _buttons[x].Image = null;
                    continue;
                }

                _buttons[x].Image = new ButtonImage() { Image = _items[x].Icon, Size = new Vector2(32, 32) };
            }

            switch(MouseHelper.GetScrollDirection())
            {
                case MouseHelper.ScrollDirection.Up:
                    SetActiveIndex(_activeIndex - 1);

                    break;

                case MouseHelper.ScrollDirection.Down:
                    SetActiveIndex(_activeIndex + 1);

                    break;
            }
        }
    }
}
