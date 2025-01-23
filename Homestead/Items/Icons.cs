using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homestead.Items
{
    public static class Icons
    {
        public static SpriteFrame Axe { get; } = SpriteSheetManager.GetSheetByName("Icons").GetByKey("0");
        public static SpriteFrame Logs { get; } = SpriteSheetManager.GetSheetByName("Icons").GetByKey("1");
        public static SpriteFrame TreeSeed { get; } = SpriteSheetManager.GetSheetByName("Icons").GetByKey("3");
        public static SpriteFrame Planks { get; } = SpriteSheetManager.GetSheetByName("Icons").GetByKey("4");
        public static SpriteFrame WoodenWall { get; } = SpriteSheetManager.GetSheetByName("Icons").GetByKey("5");

    }
}
