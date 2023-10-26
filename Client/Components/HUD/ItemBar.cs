using LDG.Components.Collision;
using LDG.Components.Tile;
using LDG.Extensions;
using LDG.Sprite;
using LDG.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LDG;
using Microsoft.Xna.Framework;
using LDG.Components;
using LDG.Input;

namespace Client.Components.HUD
{
    public class ItemBar : LDG.GameComponent
    {
        public ItemBar()
        {
        }

        private int CurrentIndex = -1;

        private int scrollValue = Mouse.GetState().ScrollWheelValue;

        private UIGroup group;

        private SquareElement selectionSquare;

        private List<ButtonElement> buttons = new List<ButtonElement>();

        private LDG.Components.Actor playerActor;

        public override void Initialize()
        {
            this.group = this.GameObject.AddComponent<UIGroup>();

            this.playerActor = this.GameObject.Scene.GetGameObjectWithTag("Player").GetComponent<LDG.Components.Actor>();

            Vector2 size = new Vector2(460, 60);

            this.group.Settings = new UIGroupSettings()
            {
                Position = new Rectangle((int)(Screen.Resolution.X / 2) - (int)(size.X / 2), (int)Screen.Resolution.Y - (int)(size.Y) - 10, (int)size.X, (int)size.Y)
            };

            for (int x = 0; x < 9; x++)
            {
                Items.Item item = null;

                if(Items.Inventory.Items.Count >= x + 1)
                {
                    item = Items.Inventory.Items[x];
                }

                buttons.Add(group.Button(new ButtonElement(new Rectangle(new Point(10 + (x * 50), 10), new Point(40, 40)))
                {
                    Text = "",
                    Image = new ButtonImage()
                    {
                        Size = new Vector2(24, 24),
                        Image = item != null ? item.SpriteFrame : null
                    },
                    OnClick = () =>
                    {

                    }
                }));
            }

            selectionSquare = group.Square(new Point(10 + (CurrentIndex * 50), 10), new Point(40, 40), Color.Transparent, Color.Red, 4);
        }
        public override void Update(TimeFrame time)
        {
            for (int x = 0; x < 9; x++)
            {
                Items.Item item = null;

                if (Items.Inventory.Items.Count >= x + 1)
                {
                    item = Items.Inventory.Items[x];

                    buttons[x].Image = new ButtonImage()
                    {
                        Size = new Vector2(24, 24),
                        Image = item != null ? item.SpriteFrame : null
                    };
                }

            }
            
            int newScroll = Mouse.GetState().ScrollWheelValue;

            int startValue = CurrentIndex;

            if(CurrentIndex == -1)
            {
                CurrentIndex = 0;
            }

            if(newScroll < scrollValue)
            {
                CurrentIndex++;
            }
            if (newScroll > scrollValue)
            {
                CurrentIndex--;
            }
            if (CurrentIndex < 0)
            {
                CurrentIndex = 8;
            } 

            if(CurrentIndex > 8)
            {
                CurrentIndex = 0;
            }

            scrollValue = newScroll;

            // Set selection square position
            if(startValue != CurrentIndex)
            {
                selectionSquare.Position = new Rectangle(new Point(10 + (CurrentIndex * 50), 10), selectionSquare.Position.Size);

                // Leave hand for one item enter hand for the other 
                if(startValue != -1)
                {
                    // Check if we actually had an item
                    if(startValue + 1 <= Items.Inventory.Items.Count)
                        Items.Inventory.Items[startValue].LeaveHand(playerActor);
                }

                if(CurrentIndex + 1 <= Items.Inventory.Items.Count)
                    Items.Inventory.Items[CurrentIndex].EnterHand(playerActor);
            }


            if(KeyboardHelper.WasKeyPressed(Keys.E))
            {
                if (Client.Items.Inventory.Items.Count > this.CurrentIndex)
                {
                    var item = Items.Inventory.Items[this.CurrentIndex];

                    if (item != null)
                    {
                        item.Use(this.playerActor);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            return;

            // Get player component
            var actor = GetComponent<LDG.Components.Actor>();

            // Get tilemap
            var tilemap = GameObject.Scene.GetAllComponentsOfType<Tilemap>()[0];
            var collider = GetComponent<BoxCollider>();

            Vector2 facingDirection = Vector2.Zero;

            switch (actor.Direction)
            {
                case Direction.Up:
                    facingDirection = new Vector2(0, -1);
                    break;

                case Direction.Down:
                    facingDirection = new Vector2(0, 1);
                    break;

                case Direction.Left:
                    facingDirection = new Vector2(-1, 0);
                    break;

                case Direction.Right:
                    facingDirection = new Vector2(1, 0);
                    break;
            }

            Vector2 tilePosition = tilemap.WorldPositionToTileStart(this.Transform.Position + (collider.Bounds * (facingDirection) + (tilemap.TileSize.ToVector2() * facingDirection)));

            //using (var group = UIGroup.BeginGroup(new UIGroupSettings()
            //{
            //    Position = new Rectangle(LDG.Camera.WorldPositionToCameraPoint(tilePosition), tilemap.TileSize),
            //    ShowBox = false
            //}))
            //{
            //    group.Square(Point.Zero, group.Settings.Position.Size, Color.Blue.SetOpacity(0.15f), Color.CadetBlue.SetOpacity(0.3f), 2);
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.E))
            //{
            //    tilemap.SetTileAtLocation(1, tilemap.WorldPositionToTilePosition(tilePosition), SpriteSheetManager.GetSheetByName("tiles_world").GetByKey("0"));
            //}
        }
    }
}
