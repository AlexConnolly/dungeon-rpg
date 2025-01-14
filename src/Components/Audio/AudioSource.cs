using LDG.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace LDG.Components.Audio
{
    public class AudioSource : GameComponent
    {
        public SoundEffectInstance Sound { get; set; }

        public AudioSource()
        {

        }

        public bool IsPlaying()
        {
            return Sound != null && Sound.State == SoundState.Playing;
        }

        public void Loop()
        {
            this.Start(true);
        }

        public void Start(bool loop = false)
        {
            if(Sound != null)
            {
                this.Stop();
            }

            Sound.IsLooped = loop;
            Sound.Play();
        }

        public void Stop()
        {
            if(Sound != null)
            {
                Sound.Stop();
            }
        }

        public override void Update(TimeFrame time)
        {
            if(this.Sound != null)
            {
                const float MaximumListenRange = 250f;

                var distance = Math.Abs(Vector2.Distance(this.Transform.Position, LDG.Camera.Position));

                if (distance > MaximumListenRange)
                {
                    this.Sound.Volume = 0.1f;
                }
                else
                {
                    // Calculate volume 
                    float rangeLeft = MaximumListenRange - distance;

                    float percentage = rangeLeft / MaximumListenRange;

                    // Set volume 
                    this.Sound.Volume = LDGMathHelpers.LogFade(0.1f, 1, percentage);
                }
            }
        }
    }
}
