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
    }
}
