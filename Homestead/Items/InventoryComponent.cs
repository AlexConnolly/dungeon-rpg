using Homestead.Abilities.Woodcutting.Items;
using Homestead.World;
using LDG;
using LDG.Audio;
using LDG.Components.Audio;
using LDG.UI;
using Microsoft.Xna.Framework;
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

        private List<IItem> _items = new List<IItem>();

        private int _activeIndex = 0;

        private UIGroup _uiGroup;

        private List<ButtonElement> _buttons = new List<ButtonElement>();

        private AudioSource _audioSource;

        private WorldManager _worldManager;
        private Player _player;

        private AudioSource _clickSource;

        public IItem GetActiveItem()
        {
            if (_items.Count <= _activeIndex)
                return null;

            return _items[_activeIndex];
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

        public void AddItem(IItem item)
        {
            _items.Add(item);
        }

        public T AddItem<T>() where T : IItem
        {
            var instance = (IItem)Activator.CreateInstance(typeof(T));

            _items.Add(instance);

            return (T)instance;
        }

        public bool ActionItemInHand()
        {
            // If our should always be greater than our active index
            if (_items.Count <= _activeIndex)
                return false;

            var currentItem = _items[_activeIndex];

            if(currentItem != null)
            {
                if (currentItem.Action(_player, _worldManager))
                {
                    if(currentItem.Sound != null)
                    {
                        var audioClipConfig = new AudioClipConfig()
                        {
                            Clip = currentItem.Sound,
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
                    _buttons[_activeIndex].IsActive = false;

                    _activeIndex = actualIndex;

                    _buttons[_activeIndex].IsActive = true;

                    _clickSource.Start();
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
            for(int x = 0; x < _items.Count; x++)
            {
                if (x == 9)
                    break;

                _buttons[x].Image = new ButtonImage() { Image = _items[x].Icon, Size = new Vector2(32, 32) };
            }
        }
    }
}
