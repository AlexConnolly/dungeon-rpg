using LDG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Actor
{
    public abstract class MortalActorEffect
    {
        public abstract MortalActorStats Update(TimeFrame time, MortalActorStats current, MortalActorStats initial);
    }
}
