using Homestead.Abilities.Crafting.Recipe;
using Homestead.Items;
using Homestead.World;
using LDG.Sprite;
using LDG.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homestead.Abilities.Crafting.Windows
{
    internal class CraftingWindow : WindowElement
    {
        public override Action OnClosed => () =>
        {

        };

        public override string Title => "Crafting Bench";

        public override Point Size => new Point(600, 400);

        public override SpriteFrame Icon => SpriteSheetManager.GetSheetByName("WorldObjects").GetByKey("1");

        private IRecipe _currentRecipe;
        private List<IRecipe> _recipes = new List<IRecipe>();

        public CraftingWindow()
        {
            _recipes = new List<IRecipe>()
            {
                new PlanksRecipe(),
                new WoodenWallRecipe()
            };
        }

        private List<ButtonElement> _buttons = new List<ButtonElement>();

        private void SetCurrentRecipe(IRecipe recipe)
        {
            _currentRecipe = recipe;

            for(int x = 0; x < 9; x++)
            {
                // Make sure that new recipe has item as index
                if(x >= _currentRecipe.Input.Count)
                {
                    _buttons[x].Image = null;
                    continue;
                }

                var item = _currentRecipe.Input[x];

                _buttons[x].Image = new ButtonImage()
                {
                    Image = item.Icon,
                    Size = new Vector2(70, 70)
                };
            }

        }

        public override void AddElements(UIGroup group)
        {
            _currentRecipe = _recipes[0];

            var listItems = new List<ListItem>();

            foreach(var recipe in _recipes)
            {
                listItems.Add(new ListItem()
                {
                    Title = recipe.Output.Name,
                    Text = recipe.Output.Description,
                    Identifier = recipe
                });
            }

            var list = group.List(new ListElement(new Microsoft.Xna.Framework.Rectangle(10, 70, 190, 320), listItems));

            list.OnItemClicked = (item) =>
            {
                SetCurrentRecipe(item.Identifier as IRecipe);
            };

            // A 3x3 grid of squares
            int size = 70;
            int spacing = 20;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int xLocation = (x * size) + (x * spacing);
                    int yLocation = (y * size) + (y * spacing);

                    // button over the top of the square
                    var button = group.Button(new ButtonElement(new Microsoft.Xna.Framework.Rectangle(220 + xLocation, 70 + yLocation, size, size))
                    {
                        OnClick = () =>
                        {
                        }
                    });

                    _buttons.Add(button);
                }
            }

            // Craft all button below the squares
            group.Button(new ButtonElement(new Rectangle(220, 350, 180, 40))
            {
                Text = "Craft All",
                OnClick = () =>
                {
                    var player = Player.GetInstance();

                    while(player.Inventory.HasItems(_currentRecipe.Input.ToArray()))
                    {
                        player.Inventory.RemoveItems(_currentRecipe.Input.ToArray());
                        player.Inventory.AddItem(_currentRecipe.Output);
                    }
                }
            });

            // Craft one button
            group.Button(new ButtonElement(new Rectangle(410, 350, 180, 40))
            {
                Text = "Craft One",
                OnClick = () =>
                {
                    var player = Player.GetInstance();

                    if (player.Inventory.HasItems(_currentRecipe.Input.ToArray()))
                    {
                        player.Inventory.RemoveItems(_currentRecipe.Input.ToArray());

                        player.Inventory.AddItem(_currentRecipe.Output);

                        player.PlaySound(Sounds.OhYeah);
                    }
                }
            });

            SetCurrentRecipe(_recipes[0]);
        }
    }
}
