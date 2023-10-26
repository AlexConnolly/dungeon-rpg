using Client.Components.ActorComponents;
using LDG;
using LDG.Components;
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
                return SpriteSheetManager.GetSheetByName("items").GetByKey("105");
            }
        }

        public override void EnterHand(Actor consumer)
        {
            consumer.GameObject.GetComponent<WieldedItem>().Item = this;
        }

        public override void LeaveHand(Actor consumer)
        {
            consumer.GameObject.GetComponent<WieldedItem>().Item = null;
        }

        public override void Use(Actor consumer)
        {
            foreach(var other in consumer.ReachZone.IntersectingObjects)
            {
                if(other != consumer.GameObject) {
                    consumer.GameObject.Scene.RemoveObject(other);
                }
            }
        }
    }
}
