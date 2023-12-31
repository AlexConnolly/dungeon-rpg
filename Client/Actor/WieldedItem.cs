﻿using LDG;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Actor
{
    public class WieldedItem : LDG.GameComponent
    {
        public Items.Item Item { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Item == null)
                return;

            Item.SpriteFrame.Draw(spriteBatch, Camera.WorldPositionToCameraPoint(Transform.Position).ToVector2(), Color.White, new Point(32, 32), 1);
        }
    }
}
