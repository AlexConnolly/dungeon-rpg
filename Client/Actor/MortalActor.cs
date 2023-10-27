using LDG;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Actor
{
    /// <summary>
    /// Although this sounds extremely scary it is simply to allow for buffs, debuffs and health
    /// </summary>
    /// 

    public class MortalActorStats
    {
        public float MovementSpeed { get; set; }
        public float Health { get; set; }
    }

    public class ActiveEffect
    {
        public required float Time { get; set; }
        public required MortalActorEffect Effect { get; set; }
    }

    public class MortalActor : ActorComponent
    {
        private List<ActiveEffect> _debuffs = new List<ActiveEffect>();
        private List<ActiveEffect> _buffs = new List<ActiveEffect>();

        private MortalActorStats BaseStats { 
            get
            {
                return new MortalActorStats()
                {
                    MovementSpeed = this.MovementSpeed,
                    Health = 100
                };
            }
        } 

        public void AddDebuff(ActiveEffect effect)
        {
            this._debuffs.Add(effect);
        }

        public void AddBuff(ActiveEffect effect)
        {
            this._buffs.Add(effect);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(TimeFrame time)
        {

            var debuffedStats = new MortalActorStats()
            {
                Health = this.BaseStats.Health,
                MovementSpeed = this.BaseStats.MovementSpeed
            };

            // Debuffs
            foreach(var debuff in this._debuffs)
            {
                debuff.Time -= time.Delta;
                debuffedStats = debuff.Effect.Update(time, debuffedStats, this.BaseStats);
            }

            this._debuffs.Where(x => x.Time <= 0).ToList().ForEach(x => this._debuffs.Remove(x));

            // Buffs
            foreach (var buff in this._buffs)
            {
                buff.Time -= time.Delta;
                debuffedStats = buff.Effect.Update(time, debuffedStats, this.BaseStats);
            }

            this._buffs.Where(x => x.Time <= 0).ToList().ForEach(x => this._buffs.Remove(x));

            // Apply debuffed stats


            // Do not forget to update the base actor
            base.Update(time);
        }
    }
}
