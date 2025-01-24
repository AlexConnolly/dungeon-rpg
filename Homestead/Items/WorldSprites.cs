using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Items
{
    public static class WorldSprites
    {
        public static SpriteFrame WoodenWall { get; } = SpriteSheetManager.GetSheetByName("WorldObjects").GetByKey("2");
    }
}
