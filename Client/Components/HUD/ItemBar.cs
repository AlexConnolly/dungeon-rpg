﻿using LDG.Components.Collision;
using LDG.Components.Tile;
using LDG.Extensions;
using LDG.Sprite;
using LDG.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LDG;
using Microsoft.Xna.Framework;
using LDG.Components;

namespace Client.Components.HUD
{
    public class ItemBar : LDG.GameComponent
    {
        public ItemBar()
        {
        }

        private int CurrentIndex = 0;

        private int scrollValue = Mouse.GetState().ScrollWheelValue;

        private UIGroup group;

        private SquareElement selectionSquare;

        public override void Initialize()
        {
            this.group = this.GameObject.AddComponent<UIGroup>();

            Vector2 size = new Vector2(460, 60);

            this.group.Settings = new UIGroupSettings()
            {
                Position = new Rectangle((int)(Screen.Resolution.X / 2) - (int)(size.X / 2), (int)Screen.Resolution.Y - (int)(size.Y) - 10, (int)size.X, (int)size.Y)
            };

            for (int x = 0; x < 9; x++)
            {
                group.Button(new ButtonElement(new Rectangle(new Point(10 + (x * 50), 10), new Point(40, 40)))
                {
                    Text = "",
                    Image = new ButtonImage()
                    {
                        Size = new Vector2(24, 24),
                        Image = SpriteSheetManager.GetSheetByName("tiles_world").GetByKey("0")
                    }
                });
            }

            selectionSquare = group.Square(new Point(10 + (CurrentIndex * 50), 10), new Point(40, 40), Color.Transparent, Color.Red, 4);
        }
        public override void Update(TimeFrame time)
        {
            int newScroll = Mouse.GetState().ScrollWheelValue;

            int startValue = CurrentIndex;

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
                selectionSquare.Position = new Rectangle(new Point(10 + (CurrentIndex * 50), 10), selectionSquare.Position.Size);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            return;

            // Get player component
            var actor = GetComponent<Actor>();

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

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                tilemap.SetTileAtLocation(1, tilemap.WorldPositionToTilePosition(tilePosition), SpriteSheetManager.GetSheetByName("tiles_world").GetByKey("0"));
            }
        }
    }
}