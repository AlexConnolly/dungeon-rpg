using LDG.Sprite;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Tile
{
    public class TilemapPointer : GameComponent
    {
        public SpriteFrame Image { get; set; }

        public Tilemap Tilemap { get; set; }

        public override void Initialize()
        {
            if (Tilemap == null || Image == null)
                return;

            var mouseWorldPosition = LDG.Camera.CameraPointToWorldPosition(Mouse.GetState().Position);
        }
    }
}
