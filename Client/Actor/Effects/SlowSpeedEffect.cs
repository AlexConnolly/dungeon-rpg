using LDG;
using LDG.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Actor.Effects
{
    public class SlowSpeedEffect : MortalActorEffect
    {
        public override SpriteFrame Frame { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float Percentage = 50f;

        public override MortalActorStats Update(TimeFrame time, MortalActorStats current, MortalActorStats initial)
        {
            return new MortalActorStats()
            {
                Health = current.Health,
                MovementSpeed = (initial.MovementSpeed - ((initial.MovementSpeed) * (Percentage / 100)))
            };
        }
    }
}
