using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Items.Items
{
    public class StoneSword : Item
    {
        public override string Name { get; set; } = "Stone Sword";
        public override string Description { get; set; } = "A sword for an ill-equipped warrior";

        public override SpriteFrame SpriteFrame
        {
            get
            {
                return SpriteSheetManager.GetSheetByName("items").GetByKey("104");
            }
        }

        public override void Use()
        {

        }
    }
}
