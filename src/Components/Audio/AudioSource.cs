﻿using LDG.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Components.Audio
{
    public class AudioSource : GameComponent
    {
        private SoundEffectInstance instance;

        public SoundEffect Sound { get; set; }

        public AudioSource()
        {

        }

        public bool IsPlaying()
        {
            return instance != null && instance.State == SoundState.Playing;
        }

        public void Loop()
        {
            this.Start(true);
        }

        public void Start(bool loop = false)
        {
            if(instance != null)
            {
                this.Stop();
            }

            instance = Sound.CreateInstance();
            instance.IsLooped = loop;
            instance.Play();
        }

        public void Stop()
        {
            if(instance != null)
            {
                instance.Stop();
                instance = null;
            }
        }

        public override void Update(TimeFrame time)
        {
            if(this.instance != null)
            {
                const float MaximumListenRange = 250f;

                var distance = Math.Abs(Vector2.Distance(this.Transform.Position, LDG.Camera.Position));

                if (distance > MaximumListenRange)
                {
                    this.instance.Volume = 0.1f;
                }
                else
                {
                    // Calculate volume 
                    float rangeLeft = MaximumListenRange - distance;

                    float percentage = rangeLeft / MaximumListenRange;

                    // Set volume 
                    this.instance.Volume = LDGMathHelpers.LogFade(0.1f, 1, percentage);
                }
            }
        }
    }
}
